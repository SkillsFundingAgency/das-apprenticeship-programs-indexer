using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using OfficeOpenXml;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Extensions;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Infrastructure;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class AssessmentOrgsXlsxService : IGetAssessmentOrgsData
    {
        private readonly IAssessmentOrgsExcelPackageService _assessmentOrgsExcelPackageService;
        private readonly IWebClient _webClient;
        protected readonly IAppServiceSettings _appServiceSettings;
        private readonly ILog _log;

        public AssessmentOrgsXlsxService(IAssessmentOrgsExcelPackageService assessmentOrgsExcelPackageService, IWebClient webClient, IAppServiceSettings appServiceSettings, ILog log)
        {
            _assessmentOrgsExcelPackageService = assessmentOrgsExcelPackageService;
            _webClient = webClient;
            _appServiceSettings = appServiceSettings;
            _log = log;
        }

        public AssessmentOrganisationsDTO GetAssessmentOrganisationsData()
        {
            IDictionary<string, object> extras = new Dictionary<string, object>();
            extras.Add("DependencyLogEntry.Url", _appServiceSettings.VstsAssessmentOrgsUrl);
            

            try
            {
                _log.Debug("Downloading ROAAO", new Dictionary<string, object> {{"Url", _appServiceSettings.VstsAssessmentOrgsUrl}});
                IEnumerable<Organisation> assessmentOrgs;
                IEnumerable<StandardOrganisationsData> standardOrganisationsData;

                using (var stream = GetFileStream())
                using (var package = new ExcelPackage(stream))
                {
                    assessmentOrgs = _assessmentOrgsExcelPackageService.GetAssessmentOrganisations(package).ToList();
                    standardOrganisationsData = _assessmentOrgsExcelPackageService.GetStandardOrganisationsData(package).ToList();

                    foreach (var org in assessmentOrgs)
                    {
                        var standard = standardOrganisationsData.FirstOrDefault(x => x.EpaOrganisationIdentifier == org.EpaOrganisationIdentifier);
                        org.Phone = standard?.Phone;
                        org.Email = standard?.Email;
                    }

                    foreach (var data in standardOrganisationsData)
                    {
                        var organisation = assessmentOrgs.FirstOrDefault(x => x.EpaOrganisationIdentifier == data.EpaOrganisationIdentifier);
                        data.Address = organisation?.Address;
                        data.EpaOrganisation = organisation?.EpaOrganisation;
                        data.WebsiteLink = organisation?.WebsiteLink;
                        data.OrganisationType = organisation?.OrganisationType;
                    }
                }

                return new AssessmentOrganisationsDTO
                {
                    Organisations = FilterOrganisations(assessmentOrgs),
                    StandardOrganisationsData = FilterStandardOrganisations(standardOrganisationsData)
                };
            }
            catch (WebException wex)
            {
                var response = (HttpWebResponse)wex.Response;
                if (response != null)
                {
                    extras.Add("DependencyLogEntry.ResponseCode", response.StatusCode);
                }

                if (response?.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _log.Error(wex, "Your VSTS credentials were unauthorised", extras);
                }
                else
                {
                    _log.Error(wex, "Problem downloading ROATP from VSTS", extras);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Problem downloading ROATP from VSTS", extras);
            }

            return null;
        }

        public virtual Stream GetFileStream()
        {
            return new MemoryStream(_webClient.DownloadData(new Uri(_appServiceSettings.VstsAssessmentOrgsUrl)));
        }

        private List<Organisation> FilterOrganisations(IEnumerable<Organisation> organisationsData)
        {
            return organisationsData.Where(organisation => organisation.EpaOrganisationIdentifier != string.Empty && organisation.EpaOrganisation != string.Empty).DistinctBy(x => x.EpaOrganisationIdentifier).ToList();
        }

        private List<StandardOrganisationsData> FilterStandardOrganisations(IEnumerable<StandardOrganisationsData> standardOrganisationsData)
        {
            return
                standardOrganisationsData.Where(
                    organisationsData =>
                        organisationsData.EpaOrganisationIdentifier != string.Empty && organisationsData.StandardCode != string.Empty && organisationsData.EffectiveFrom != DateTime.MaxValue).ToList();
        }
    }
}
