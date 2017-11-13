using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.AzureWorkerRole.DependencyResolution;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Net;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

namespace Esfa.Roaao.Xslx.IntegrationTests
{
    [TestClass]
    public class RoaaoExcelTests
    {
        private AssessmentOrganisationsDTO results;

        private IGetAssessmentOrgsData prodsut;

        [TestInitialize]
        public void Init()
        {
            // Arrange
            var container = IoC.Initialize();
            var sut = container.GetInstance<IGetAssessmentOrgsData>();

            // Act
            results = sut.GetAssessmentOrganisationsData();
            using (var prodContainer = container.GetNestedContainer())
            {
                prodContainer.Configure(_ => { _.For<IAppServiceSettings>().Use<ProdAppSettings>(); });
                prodsut = prodContainer.GetInstance<IGetAssessmentOrgsData>();
            }
        }

        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void ShouldNotDeleteExistingOrganisationStandardAssociation()
        {
            List<string> errors = new List<string>();
            var prodresults = prodsut.GetAssessmentOrganisationsData();
            var prodAssesmentOrgs = prodresults.StandardOrganisationsData.Select(x => new List<string> { x.EpaOrganisationIdentifier,x.StandardCode }).ToList();
            var localAssesmentOrgs = results.StandardOrganisationsData.Select(x => new List<string> { x.EpaOrganisationIdentifier, x.StandardCode }).ToList();

            var removedOrganisationsAssociationWithStandards = prodAssesmentOrgs.Select(x=>$"{x[0]}-{x[1]}").ToList()
                                                               .Except(localAssesmentOrgs.Select(y => $"{y[0]}-{y[1]}").ToList())
                                                               .Select(x => new List<string> { x.Split('-')[0], x.Split('-')[1] })
                                                               .ToList();

            foreach(var removed in removedOrganisationsAssociationWithStandards)
            {
                var effectiveFrom = prodresults.StandardOrganisationsData.Single(x => x.EpaOrganisationIdentifier == removed[0] && x.StandardCode == removed[1]).EffectiveFrom;
                var epaorganisation = results.Organisations.SingleOrDefault(x => x.EpaOrganisationIdentifier == removed[0]);
                if (epaorganisation != null && effectiveFrom <= DateTime.Now)
                {
                    errors.Add($"'{epaorganisation.EpaOrganisation}({removed[0]})' - Standard Code {removed[1]} since {effectiveFrom.ToString("dd/MM/yyyy")}");
                }
            }

            Assert.AreEqual(0, errors.Count, $"Following details are removed from 'Register - Standards' worksheet {Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
        }


        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void ShouldNotDeleteExistingOrganisation()
        {
            var prodAssesmentOrgs = prodsut.GetAssessmentOrganisationsData().Organisations.Select(x=>x.EpaOrganisationIdentifier).ToList();
            var localAssesmentOrgs = results.Organisations.Select(x => x.EpaOrganisationIdentifier).ToList();
            
            var removedOrganisations = prodAssesmentOrgs.Except(localAssesmentOrgs);

            Assert.AreEqual(0, removedOrganisations.Count(), $"Following details are removed from 'Register - Organisations' worksheet  {Environment.NewLine}{string.Join(Environment.NewLine, removedOrganisations)}");
        }

        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void EpaShouldExist()
        {
            List<string> errors = new List<string>();
            foreach (var standardOrganisationsData in results.StandardOrganisationsData)
            {
                if (results.Organisations.Any(x => x.EpaOrganisationIdentifier == standardOrganisationsData.EpaOrganisationIdentifier) == false)
                {
                    errors.Add(standardOrganisationsData.EpaOrganisationIdentifier);
                }
            }
            Assert.AreEqual(0, errors.Count, $"Couldn't find the Assessment Org for {Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
        }

        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void ShouldNotHaveTrailingOrLeadingSpaceInOrganisationType()
        {
            List<string> errors = new List<string>();

            errors.AddRange(results.Organisations.Where(x => string.Equals(x.OrganisationType.Trim(), x.OrganisationType) == false).Select(y => $"{y.EpaOrganisationIdentifier} - '{y.OrganisationType}'"));
                
            Assert.AreEqual(0, errors.Count, $"Following assessment Org has space in organisation type {Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
        }

        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void ShouldHaveAValidEffectiveFromDateForStandardPeriods()
        {
            List<string> errors = new List<string>();
            foreach (var data in results.StandardOrganisationsData)
            {
                if (data.EffectiveFrom == default(DateTime) || data.EffectiveFrom == DateTime.MaxValue || data.EffectiveFrom == DateTime.MinValue)
                {
                    errors.Add($"EPA {data.EpaOrganisationIdentifier} has an invalid effective from date for Standard {data.StandardCode}");
                }
            }
            Assert.IsTrue(errors.Count == 0, string.Join(Environment.NewLine, errors));
        }

        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void ShouldNotHaveOverlappingPeriods()
        {
            List<string> errors = new List<string>();
            foreach (var organisationIdentifier in results.Organisations.GroupBy(x => x.EpaOrganisationIdentifier).Select(id => id.Key))
            {
                var epadata = results.StandardOrganisationsData.Where(x => x.EpaOrganisationIdentifier == organisationIdentifier);
                var standardCodes = epadata.GroupBy(y => y.StandardCode).Where(g => g.Count() > 1).Select(z => z.Key);
                foreach (var standardCode in standardCodes)
                {
                    var periods = epadata.Where(x => x.StandardCode == standardCode).OrderBy(y => y.EffectiveFrom);
                    for (int i = 0; i < periods.Count() - 1; i++)
                    {
                        var from = periods.ElementAt(i + 1).EffectiveFrom;
                        var to = periods.ElementAt(i).EffectiveTo;

                        if (from < to)
                        {
                            errors.Add($"{organisationIdentifier} assessment for Standard {standardCode} has effective to as {to} but next assessment starts from {from}");
                        }
                    }
                }
            }
            Assert.IsTrue(errors.Count == 0, string.Join(Environment.NewLine, errors));
        }

        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void ShouldNotHaveDuplicateCurrentPeriods()
        {
            var errors = new List<string>();
            foreach (var epaidentifier in results.Organisations.Select(x => x.EpaOrganisationIdentifier).Distinct())
            {
                var epaStandards = results.StandardOrganisationsData.Where(x => x.EpaOrganisationIdentifier == epaidentifier);

                //Get list of Standards which has >1 rows 
                var standards = epaStandards.GroupBy(x => x.StandardCode).Where(y => y.Count() > 1).Select(z => z.Key);
                foreach (var standard in standards)
                {
                    //Get list of Standards covered by an EPA which has more than 1 current periods
                    var currentperiods = epaStandards
                                         .Where(x => x.StandardCode == standard)
                                         .Where(z => (z.EffectiveFrom.Date <= DateTime.Now.Date && (z.EffectiveTo.HasValue == false || z.EffectiveTo.Value.Date >= DateTime.Now.Date)));
                                  
                    if (currentperiods.Count() > 1)
                    {
                        errors.Add($"{epaidentifier} assessment for Standard {standard} has duplicate current periods.");
                    }
                }
            }
            Assert.IsTrue(errors.Count == 0, string.Join(Environment.NewLine, errors));
        }

        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void ShouldNotHaveInvalidPeriods()
        {
            var errors = new List<string>();
            foreach (var epaidentifier in results.Organisations.Select(x => x.EpaOrganisationIdentifier).Distinct())
            {
                var epaStandards = results.StandardOrganisationsData.Where(x => x.EpaOrganisationIdentifier == epaidentifier);

                //Get list of Standards which has >1 rows 
                var standards = epaStandards.GroupBy(x => x.StandardCode).Where(y => y.Count() > 1).Select(z => z.Key);
                foreach (var standard in standards)
                {
                    //Get list of Standards covered by an EPA which has invalid periods
                    var invalidperiods = epaStandards
                                         .Where(x => x.StandardCode == standard)
                                         .Where(z => z.EffectiveTo.HasValue == false || z.EffectiveTo.Value.Date > DateTime.Now);

                    if (invalidperiods.Count() > 1)
                    {
                        errors.Add($"{epaidentifier} assessment for Standard {standard} has some invalid periods.");
                    }
                }
            }
            Assert.IsTrue(errors.Count == 0, string.Join(Environment.NewLine, errors));
        }

        [TestMethod]
        [TestCategory("RoAAo Excel Tests")]
        public void ShouldNotHaveBrokenWebsiteLink()
        {
            var errors = new List<string>();
            foreach (var epa in results.Organisations.Select(x => new string[] { x.EpaOrganisationIdentifier, x.WebsiteLink }))
            {
                var website = epa[1];
                var websiteCheck = CheckWebsiteLink(website);
                if (!string.IsNullOrEmpty(website) && !websiteCheck.Key)
                {
                   errors.Add($"{epa[0]} EPA Org has a broken website link {epa[1]}, no of attempts {websiteCheck.Value}");
                }
            }
            Assert.IsTrue(errors.Count == 0, string.Join(Environment.NewLine, errors));
        }

        private KeyValuePair<bool, int> CheckWebsiteLink(string website)
        {
            var absolutePath = website.StartsWith("http") ? website : $"http://{website}";
            int noofattempts = 0;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    for (var i = 0; i <= 2; i++)
                    {
                        noofattempts = i + 1;
                        var request = new HttpRequestMessage(HttpMethod.Head, new Uri(absolutePath));
                        request.Headers.Add("Accept", "text/html");
                        request.Headers.Add("Cache-Control", "no-cache");
                        request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                        using (var response = httpClient.SendAsync(request))
                        {
                            var result = response.Result;
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                return new KeyValuePair<bool, int>(true, noofattempts);
                            }
                        }
                    }
                    return new KeyValuePair<bool, int>(false, noofattempts);
                }
            }
            catch(Exception)
            {
                return new KeyValuePair<bool, int>(false, noofattempts);
            }
        }
    }    
}
