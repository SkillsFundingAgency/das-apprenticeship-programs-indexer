using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
        private readonly IAppServiceSettings _appServiceSettings;

        public AssessmentOrgsXlsxService(IAssessmentOrgsExcelPackageService assessmentOrgsExcelPackageService, IWebClient webClient, IAppServiceSettings appServiceSettings)
        {
            _assessmentOrgsExcelPackageService = assessmentOrgsExcelPackageService;
            _webClient = webClient;
            _appServiceSettings = appServiceSettings;
        }

        public AssessmentOrganisationsDTO GetAssessmentOrganisationsData()
        {
            IEnumerable<Organisation> assessmentOrgs;
            IEnumerable<StandardOrganisationsData> standardOrganisationsData;

                if (!string.IsNullOrEmpty(_appServiceSettings.GitUsername))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_appServiceSettings.GitUsername}:{_appServiceSettings.GitPassword}"));
                    _webClient.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
                }

                var filePath = Path.GetTempFileName();
                _webClient.DownloadFile(new Uri(_appServiceSettings.VstsAssessmentOrgsUrl), filePath);

                using (var package = _assessmentOrgsExcelPackageService.GetExcelPackageFromFilePath(filePath))
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

        private List<Organisation> FilterOrganisations(IEnumerable<Organisation> organisationsData)
        {
            return organisationsData.Where(organisation => organisation.EpaOrganisationIdentifier != string.Empty && organisation.EpaOrganisation != string.Empty).DistinctBy(x => x.EpaOrganisationIdentifier).ToList();
        }

        private List<StandardOrganisationsData> FilterStandardOrganisations(IEnumerable<StandardOrganisationsData> standardOrganisationsData)
        {
            return
                standardOrganisationsData.Where(
                    organisationsData =>
                        organisationsData.EpaOrganisationIdentifier != string.Empty && organisationsData.StandardCode != string.Empty && organisationsData.EffectiveFrom != default(DateTime)).ToList();
        }
    }
}
