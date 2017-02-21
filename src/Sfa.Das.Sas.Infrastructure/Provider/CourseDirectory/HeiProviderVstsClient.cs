using System.Linq;
using MediatR;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Hei;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    public sealed class HeiProviderVstsClient : IRequestHandler<HeiProvidersRequest, HeiProvidersResult>
    {
        private readonly IConvertFromCsv _convertFromCsv;
        private readonly IVstsClient _vstsClient;
        private readonly IAppServiceSettings _appServiceSettings;

        public HeiProviderVstsClient(
            IConvertFromCsv convertFromCsv,
            IVstsClient vstsClient,
            IAppServiceSettings appServiceSettings)
        {
            _convertFromCsv = convertFromCsv;
            _vstsClient = vstsClient;
            _appServiceSettings = appServiceSettings;
        }

        public HeiProvidersResult Handle(HeiProvidersRequest message)
        {
            var records = _convertFromCsv.CsvTo<HeiProviderCsvRecord>(LoadHeiProvidersFromVsts());

            var providers = (from heiProviderCsvRecord in records where heiProviderCsvRecord.UkPrn != null && heiProviderCsvRecord.OrgType == "Higher Education Organisation" select heiProviderCsvRecord.UkPrn).Distinct().ToList();
            return new HeiProvidersResult { Providers = providers };
        }


        private string LoadHeiProvidersFromVsts()
        {
            return _vstsClient.GetFileContent($"hei/{_appServiceSettings.EnvironmentName}/hei.csv");
        }
    }
}