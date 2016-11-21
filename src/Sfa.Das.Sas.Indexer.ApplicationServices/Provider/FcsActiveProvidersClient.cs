using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Infrastructure;
using Sfa.Das.Sas.Indexer.ApplicationServices.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Settings;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider
{
    public class FcsActiveProvidersClient : IGetActiveProviders
    {
        private readonly IAppServiceSettings _appServiceSettings;

        private readonly IConvertFromCsv _convertFromCsv;

        private readonly IVstsClient _vstsClient;

        public FcsActiveProvidersClient(IVstsClient vstsClient, IAppServiceSettings appServiceSettings, IConvertFromCsv convertFromCsv)
        {
            _vstsClient = vstsClient;
            _appServiceSettings = appServiceSettings;
            _convertFromCsv = convertFromCsv;
        }

        public async Task<IEnumerable<int>> GetActiveProviders()
        {
            var loadProvidersFromVsts = await _vstsClient.GetFileContentAsync($"fcs/{_appServiceSettings.EnvironmentName}/fcs-active.csv");
            var records = _convertFromCsv.CsvTo<ActiveProviderCsvRecord>(loadProvidersFromVsts);
            return records.Select(x => x.UkPrn);
        }
    }
}