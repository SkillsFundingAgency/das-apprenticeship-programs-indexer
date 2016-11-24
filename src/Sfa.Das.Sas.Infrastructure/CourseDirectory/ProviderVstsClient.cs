using System.Collections.Generic;
using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.EmployerProvider;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Hei;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    public sealed class ProviderVstsClient : IGetApprenticeshipProviders
    {
        private readonly IConvertFromCsv _convertFromCsv;
        private readonly IVstsClient _vstsClient;
        private readonly IAppServiceSettings _appServiceSettings;
        private readonly ILog _logger;

        public ProviderVstsClient(IConvertFromCsv _convertFromCsv,
            IVstsClient _vstsClient,
            IAppServiceSettings _appServiceSettings, ILog logger)
        {
            this._convertFromCsv = _convertFromCsv;
            this._vstsClient = _vstsClient;
            this._appServiceSettings = _appServiceSettings;
            _logger = logger;
        }

        public ICollection<string> GetEmployerProviders()
        {
            var records = _convertFromCsv.CsvTo<EmployerProviderCsvRecord>(LoadEmployerProvidersFromVsts());
            _logger.Debug($"Retreived {records.Count} Employer providers");

            return records.Select(employerProviderCsvRecord => employerProviderCsvRecord.UkPrn.ToString()).ToList();
        }

        private string LoadEmployerProvidersFromVsts()
        {
            return _vstsClient.GetFileContent($"employerProviders/{_appServiceSettings.EnvironmentName}/employerProviders.csv");
        }

        public ICollection<string> GetHeiProviders()
        {
            var records = _convertFromCsv.CsvTo<HeiProviderCsvRecord>(LoadHeiProvidersFromVsts());

            return (from heiProviderCsvRecord in records where heiProviderCsvRecord.UkPrn != null && heiProviderCsvRecord.OrgType == "Higher Education Organisation" select heiProviderCsvRecord.UkPrn).Distinct().ToList();
        }

        private string LoadHeiProvidersFromVsts()
        {
            return _vstsClient.GetFileContent($"hei/{_appServiceSettings.EnvironmentName}/hei.csv");
        }
    }
}