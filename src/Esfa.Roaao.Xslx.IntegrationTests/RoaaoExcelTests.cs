using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.AzureWorkerRole.DependencyResolution;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using System.Collections.Generic;
using System;

namespace Esfa.Roaao.Xslx.IntegrationTests
{
    [TestClass]
    public class RoaaoExcelTests
    {
        private AssessmentOrganisationsDTO results;

        [TestInitialize]
        public void Init()
        {
            // Arrange
            var container = IoC.Initialize();
            var sut = container.GetInstance<IGetAssessmentOrgsData>();

            // Act
            results = sut.GetAssessmentOrganisationsData();
        }


        [TestMethod]
        public void EpaShouldExist()
        {
            foreach (var standardOrganisationsData in results.StandardOrganisationsData)
            {
                Assert.IsTrue(results.Organisations.Any(x => x.EpaOrganisationIdentifier == standardOrganisationsData.EpaOrganisationIdentifier), $"Couldn't find the Assessment Org for {standardOrganisationsData.EpaOrganisationIdentifier}");
            }
        }

        [TestMethod]
        public void ShouldHaveAValidEffectiveFromDateForStandardPeriods()
        {
            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
            foreach (var data in results.StandardOrganisationsData)
            {
                if (data.EffectiveFrom == default(DateTime))
                {
                    errors.Add(new KeyValuePair<string, string>(data.StandardCode, data.EpaOrganisationIdentifier));
                }
            }
            Assert.IsTrue(errors.Count == 0, string.Join(Environment.NewLine, errors.Select(x => $"EPA {x.Value} has an invalid effective from date for Standard {x.Key} ")));
        }

        [TestMethod]
        public void ShouldNotHaveOverlappingPeriods()
        {
            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
            foreach (var organisationsData in results.Organisations.OrderBy(x => x.EpaOrganisationIdentifier))
            {
                var epadata = results.StandardOrganisationsData.Where(x => x.EpaOrganisationIdentifier == organisationsData.EpaOrganisationIdentifier);
                var standardCodes = epadata.GroupBy(y => y.StandardCode).Where(g => g.Count() > 1).Select(z => z.Key);
                foreach (var standardCode in standardCodes)
                {
                    var epaorg = organisationsData.EpaOrganisationIdentifier;
                    var periods = epadata.Where(x => x.StandardCode == standardCode).OrderBy(y => y.EffectiveFrom);
                    for (int i = 0; i < periods.Count() - 1; i++)
                    {
                        var from = periods.ElementAt(i + 1).EffectiveFrom;
                        var to = periods.ElementAt(i).EffectiveTo;

                        if (from < to)
                        {
                            Console.WriteLine($"{epaorg} assessment for Standard {standardCode} has effective to as {to} but next assessment starts from {from}");
                            errors.Add(new KeyValuePair<string, string>(standardCode, epaorg));
                        }
                    }
                }
            }
            Assert.IsTrue(errors.Count == 0, string.Join(Environment.NewLine, errors.Select(x => $"EPA {x.Value} has overlapping period for Standard {x.Key} ")));
        }
    }
}
