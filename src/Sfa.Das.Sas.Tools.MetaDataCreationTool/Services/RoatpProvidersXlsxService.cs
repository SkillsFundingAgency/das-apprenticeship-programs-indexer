using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using OfficeOpenXml;
using SFA.DAS.NLog.Logger;
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
        private readonly ILog _log;

        public RoatpProvidersXlsxService(IAppServiceSettings appServiceSettings, ILog log)
        {
            _appServiceSettings = appServiceSettings;
            _log = log;
        }

        public List<RoatpProviderResult> GetRoatpData()
        {
            var roatpProviders = new List<RoatpProviderResult>();
            IDictionary<string, object> extras = new Dictionary<string, object>();
            extras.Add("DependencyLogEntry.Url", _appServiceSettings.VstsRoatpUrl);

            using (var client = new WebClient())
            {
                if (!string.IsNullOrEmpty(_appServiceSettings.GitUsername))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_appServiceSettings.GitUsername}:{_appServiceSettings.GitPassword}"));
                    client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
                }

                try
                {
                    _log.Debug("Downloading ROATP", new Dictionary<string, object> { { "Url", _appServiceSettings.VstsRoatpUrl } });

                    using (var stream = new MemoryStream(client.DownloadData(new Uri(_appServiceSettings.VstsRoatpUrl))))
                    using (var package = new ExcelPackage(stream))
                    {

                        GetRoatp(package, roatpProviders);
                    }

                    return roatpProviders.Where(roatpProviderResult => roatpProviderResult.Ukprn != string.Empty && roatpProviderResult.OrganisationName != string.Empty).DistinctBy(x => x.Ukprn).ToList();
                }
                catch (WebException wex)
                {
                    var response = (HttpWebResponse) wex.Response;
                    if (response != null)
                    {
                        extras.Add("DependencyLogEntry.ResponseCode", response.StatusCode);
                    }

                    _log.Error(wex, "Problem downloading ROATP from VSTS", extras);
                }
                catch (Exception ex)
                {
                    _log.Error(ex, "Problem downloading ROATP from VSTS", extras);
                }
            }

            return null;
        }

        public ProviderType GetProviderType(object providerType, string ukprn)
        {
            if (providerType != null)
            {
                var type = providerType.ToString().ToLower().Trim();

                if (type == "main provider")
                {
                    return ProviderType.MainProvider;
                }

                if (type == "supporting provider")
                {
                    return ProviderType.SupportingProvider;
                }

                if (type == "employer provider")
                {
                    return ProviderType.EmployerProvider;
                }
            }

            _log.Warn($"Couldn't find the provider type \"{providerType}\"", new Dictionary<string, object> { { "UKPRN", ukprn } });
            return ProviderType.Unknown;
        }

        private void GetRoatp(ExcelPackage package, List<RoatpProviderResult> roatpProviders)
        {
            var roatpWorkSheet = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "RoATP");
            if (roatpWorkSheet == null) return;

            for (var i = roatpWorkSheet.Dimension.Start.Row + 1; i <= roatpWorkSheet.Dimension.End.Row; i++)
            {
                var ukprn = roatpWorkSheet.Cells[i, UkprnPosition].Value != null ? roatpWorkSheet.Cells[i, UkprnPosition].Value.ToString() : string.Empty;
                var roatpData = new RoatpProviderResult
                {
                    Ukprn = ukprn,
                    OrganisationName = roatpWorkSheet.Cells[i, OrganisationNamePosition].Value != null ? roatpWorkSheet.Cells[i, OrganisationNamePosition].Value.ToString() : string.Empty,
                    ProviderType = GetProviderType(roatpWorkSheet.Cells[i, ProviderTypePosition].Value, ukprn),
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
                if (value.Contains("/") || value.Contains("-"))
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
    }
}
