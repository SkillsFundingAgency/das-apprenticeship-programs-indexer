using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using OfficeOpenXml;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class RoatpProvidersXlsxService : IGetRoatpProviders
    {
        private readonly IAppServiceSettings _appServiceSettings;

        public RoatpProvidersXlsxService(IAppServiceSettings appServiceSettings)
        {
            _appServiceSettings = appServiceSettings;
        }

        public List<RoatpProviderResult> GetRoatpData()
        {
            var roatpProviders = new List<RoatpProviderResult>();

            using (var client = new WebClient())
            {
                if (!string.IsNullOrEmpty(_appServiceSettings.GitUsername))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_appServiceSettings.GitUsername}:{_appServiceSettings.GitPassword}"));
                    client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
                }

                var filePath = Path.GetTempFileName();
                client.DownloadFile(new Uri(_appServiceSettings.VstsRoatpUrl), filePath);

                using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                {
                    GetRoatp(package, roatpProviders);
                }
            }

            var response = roatpProviders.Where(roatpProviderResult => roatpProviderResult.Ukprn != string.Empty && roatpProviderResult.OrganisationName != string.Empty).ToList();
            return response;
        }

        private static void GetRoatp(ExcelPackage package, List<RoatpProviderResult> roatpProviders)
        {
            var roatpWorkSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "RoATP");
            if (roatpWorkSheet == null) return;

            for (var i = roatpWorkSheet.Dimension.Start.Row + 1; i <= roatpWorkSheet.Dimension.End.Row; i++)
            {
                var roatpData = new RoatpProviderResult
                {
                    Ukprn = roatpWorkSheet.Cells[i, 1].Value != null ? roatpWorkSheet.Cells[i, 1].Value.ToString() : string.Empty,
                    OrganisationName = roatpWorkSheet.Cells[i, 2].Value != null ? roatpWorkSheet.Cells[i, 2].Value.ToString() : string.Empty,
                    ProviderType = roatpWorkSheet.Cells[i, 3].Value != null ? roatpWorkSheet.Cells[i, 3].Value.ToString() : string.Empty,
                };

                if (roatpWorkSheet.Cells[i, 4].Value != null)
                {
                    roatpData.ContractedForNonLeviedEmployers = roatpWorkSheet.Cells[i, 4].Value.ToString().ToUpper() == "Y";
                }

                if (roatpWorkSheet.Cells[i, 5].Value != null)
                {
                    roatpData.ParentCompanyGuarantee = roatpWorkSheet.Cells[i, 5].Value.ToString().ToUpper() == "Y";
                }

                if (roatpWorkSheet.Cells[i, 6].Value != null)
                {
                    roatpData.NewOrganisationWithoutFinancialTrackRecord = roatpWorkSheet.Cells[i, 6].Value.ToString().ToUpper() == "Y";
                }

                try
                {
                    if (roatpWorkSheet.Cells[i, 7].Value != null)
                    {
                        roatpData.StartDate = roatpWorkSheet.Cells[i, 7].Value.ToString() != string.Empty ? DateTime.FromOADate(double.Parse(roatpWorkSheet.Cells[i, 7].Value.ToString())) : default(DateTime);
                    }

                    if (roatpWorkSheet.Cells[i, 8].Value != null)
                    {
                        roatpData.EndDate = roatpWorkSheet.Cells[i, 8].Value.ToString() != string.Empty ? DateTime.FromOADate(double.Parse(roatpWorkSheet.Cells[i, 8].Value.ToString())) : default(DateTime);
                    }
                }
                catch (Exception e)
                {
                    var message = e.Message;
                }

                roatpProviders.Add(roatpData);
            }
        }

        private static void GetStandardOrganisationsData(ExcelPackage package, List<StandardOrganisationsData> standardOrganisationsData)
        {
            var standardOrganisationWorkSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "Register - Standards");
            if (standardOrganisationWorkSheet == null) return;
            for (var i = standardOrganisationWorkSheet.Dimension.Start.Row + 1; i <= standardOrganisationWorkSheet.Dimension.End.Row; i++)
            {
                var standardOrganisationData = new StandardOrganisationsData
                {
                    EpaOrganisationIdentifier = standardOrganisationWorkSheet.Cells[i, 1].Value != null ? standardOrganisationWorkSheet.Cells[i, 1].Value.ToString() : string.Empty,
                    StandardCode = standardOrganisationWorkSheet.Cells[i, 3].Value != null ? standardOrganisationWorkSheet.Cells[i, 3].Value.ToString() : string.Empty,
                    EffectiveFrom = standardOrganisationWorkSheet.Cells[i, 5].Value != null ? Convert.ToDateTime(standardOrganisationWorkSheet.Cells[i, 5].Value.ToString()) : default(DateTime)
                };
                standardOrganisationsData.Add(standardOrganisationData);
            }
        }

        private static void GetAssessmentOrganisations(ExcelPackage package, List<Organisation> assessmentOrgs)
        {
            var organisationsWorkSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "Register - Organisations");

            if (organisationsWorkSheet == null) return;
            for (var i = organisationsWorkSheet.Dimension.Start.Row + 1; i <= organisationsWorkSheet.Dimension.End.Row; i++)
            {
                var organisation = new Organisation
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
                assessmentOrgs.Add(organisation);
            }
        }
    }
}
