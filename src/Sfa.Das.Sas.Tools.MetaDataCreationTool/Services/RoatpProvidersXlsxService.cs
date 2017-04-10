using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using OfficeOpenXml;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Extensions;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.Services
{
    public class RoatpProvidersXlsxService : IGetRoatpProviders
    {
        private const int UkprnPosition = 1;
        private const int OrganisationNamePosition = 2;
        private const int ProviderTypePosition = 3;
        private const int ContractedForNonLeviedEmployersPosition = 4;
        private const int ParentCompanyGuaranteePosition = 5;
        private const int NewOrganisationWithoutFinancialTrackRecordPosition = 6;
        private const int StartDatePosition = 7;
        private const int EndDatePosition = 8;

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

            var response = roatpProviders.Where(roatpProviderResult => roatpProviderResult.Ukprn != string.Empty && roatpProviderResult.OrganisationName != string.Empty).DistinctBy(x => x.Ukprn).ToList();
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
                    Ukprn = roatpWorkSheet.Cells[i, UkprnPosition].Value != null ? roatpWorkSheet.Cells[i, UkprnPosition].Value.ToString() : string.Empty,
                    OrganisationName = roatpWorkSheet.Cells[i, OrganisationNamePosition].Value != null ? roatpWorkSheet.Cells[i, OrganisationNamePosition].Value.ToString() : string.Empty,
                    ProviderType = GetProviderType(roatpWorkSheet.Cells[i, ProviderTypePosition]),
                    ContractedForNonLeviedEmployers = GetBooleanValue(roatpWorkSheet.Cells[i, ContractedForNonLeviedEmployersPosition]),
                    ParentCompanyGuarantee = GetBooleanValue(roatpWorkSheet.Cells[i, ParentCompanyGuaranteePosition]),
                    NewOrganisationWithoutFinancialTrackRecord = GetBooleanValue(roatpWorkSheet.Cells[i, NewOrganisationWithoutFinancialTrackRecordPosition]),
                    StartDate = GetDateTimeValue(roatpWorkSheet.Cells[i, StartDatePosition]),
                    EndDate = GetDateTimeValue(roatpWorkSheet.Cells[i, EndDatePosition]),
                };

                roatpProviders.Add(roatpData);
            }
        }

        private static DateTime? GetDateTimeValue(ExcelRange excelRange)
        {
            var value = excelRange.Value?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Contains("/"))
                {
                    return DateTime.Parse(value);
                }

                return DateTime.FromOADate(double.Parse(excelRange.Value.ToString()));
            }

            return null;
        }

        private static bool GetBooleanValue(ExcelRange excelRange)
        {
            if (excelRange.Value != null)
            {
                return excelRange.Value.ToString().ToUpper() == "Y";
            }

            return false;
        }

        private static ProviderType GetProviderType(ExcelRange excelRange)
        {
            if (excelRange.Value != null)
            {
                switch (excelRange.Value.ToString().ToLower())
                {
                    case "main provider":
                        return ProviderType.MainProvider;
                    case "supporting provider":
                        return ProviderType.SupportingProvider;
                    case "employer provider":
                        return ProviderType.EmployerProvider;
                    default:
                        return ProviderType.Unknown;
                }
            }

            return ProviderType.Unknown;
        }
    }
}
