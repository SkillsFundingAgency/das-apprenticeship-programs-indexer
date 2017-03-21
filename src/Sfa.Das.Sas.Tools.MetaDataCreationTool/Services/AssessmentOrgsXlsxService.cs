using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using OfficeOpenXml;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Extensions;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class AssessmentOrgsXlsxService : IGetAssessmentOrgsData
    {
        private readonly IAppServiceSettings _appServiceSettings;
        private readonly string organisationsWorksheetName = "Register - Organisations";
        private readonly string standardsWorkSheetName = "Register - Standards";

        public AssessmentOrgsXlsxService(IAppServiceSettings appServiceSettings)
        {
            _appServiceSettings = appServiceSettings;
        }

        public AssessmentOrganisationsDTO GetAssessmentOrganisationsData()
        {
            IEnumerable<Organisation> assessmentOrgs;
            IEnumerable<StandardOrganisationsData> standardOrganisationsData;

            using (var client = new WebClient())
            {
                if (!string.IsNullOrEmpty(_appServiceSettings.GitUsername))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_appServiceSettings.GitUsername}:{_appServiceSettings.GitPassword}"));
                    client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
                }

                var filePath = Path.GetTempFileName();
                client.DownloadFile(new Uri(_appServiceSettings.VstsAssessmentOrgsUrl), filePath);

                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    assessmentOrgs = GetAssessmentOrganisations(package).ToList();
                    standardOrganisationsData = GetStandardOrganisationsData(package).ToList();

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

        private IEnumerable<StandardOrganisationsData> GetStandardOrganisationsData(ExcelPackage package)
        {
            var standardOrganisationWorkSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == standardsWorkSheetName);
            if (standardOrganisationWorkSheet != null)
            {
                for (var i = standardOrganisationWorkSheet.Dimension.Start.Row + 1; i <= standardOrganisationWorkSheet.Dimension.End.Row; i++)
                {
                    yield return new StandardOrganisationsData
                    {
                        EpaOrganisationIdentifier = standardOrganisationWorkSheet.Cells[i, 1].Value != null ? standardOrganisationWorkSheet.Cells[i, 1].Value.ToString() : string.Empty,
                        StandardCode = standardOrganisationWorkSheet.Cells[i, 3].Value != null ? standardOrganisationWorkSheet.Cells[i, 3].Value.ToString() : string.Empty,
                        EffectiveFrom = standardOrganisationWorkSheet.Cells[i, 5].Value != null ? Convert.ToDateTime(standardOrganisationWorkSheet.Cells[i, 5].Value.ToString()) : default(DateTime),
                        Phone = standardOrganisationWorkSheet.Cells[i, 8].Value != null ? standardOrganisationWorkSheet.Cells[i, 8].Value.ToString() : string.Empty,
                        Email = standardOrganisationWorkSheet.Cells[i, 9].Value != null ? standardOrganisationWorkSheet.Cells[i, 9].Value.ToString() : string.Empty
                    };
                }
            }
        }

        private IEnumerable<Organisation> GetAssessmentOrganisations(ExcelPackage package)
        {
            var organisationsWorkSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == organisationsWorksheetName);

            if (organisationsWorkSheet != null)
            {
                for (var i = organisationsWorkSheet.Dimension.Start.Row + 1; i <= organisationsWorkSheet.Dimension.End.Row; i++)
                {
                    yield return new Organisation
                    {
                        EpaOrganisationIdentifier = organisationsWorkSheet.Cells[i, 1].Value != null ? organisationsWorkSheet.Cells[i, 1].Value.ToString() : string.Empty,
                        EpaOrganisation = organisationsWorkSheet.Cells[i, 2].Value != null ? organisationsWorkSheet.Cells[i, 2].Value.ToString() : string.Empty,
                        OrganisationType = organisationsWorkSheet.Cells[i, 3].Value != null ? organisationsWorkSheet.Cells[i, 3].Value.ToString() : string.Empty,
                        WebsiteLink = organisationsWorkSheet.Cells[i, 4].Value != null ? organisationsWorkSheet.Cells[i, 4].Value.ToString() : string.Empty,
                        Address = new Address
                        {
                            Primary = organisationsWorkSheet.Cells[i, 5].Value != null ? organisationsWorkSheet.Cells[i, 5].Value.ToString() : string.Empty,
                            Secondary = organisationsWorkSheet.Cells[i, 6].Value != null ? organisationsWorkSheet.Cells[i, 6].Value.ToString() : string.Empty,
                            Street = organisationsWorkSheet.Cells[i, 7].Value != null ? organisationsWorkSheet.Cells[i, 7].Value.ToString() : string.Empty,
                            Town = organisationsWorkSheet.Cells[i, 8].Value != null ? organisationsWorkSheet.Cells[i, 8].Value.ToString() : string.Empty,
                            Postcode = organisationsWorkSheet.Cells[i, 9].Value != null ? organisationsWorkSheet.Cells[i, 9].Value.ToString() : string.Empty
                        }
                    };
                }
            }
        }
    }
}
