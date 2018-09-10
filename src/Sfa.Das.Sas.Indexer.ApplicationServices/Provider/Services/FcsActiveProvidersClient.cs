using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;

    public class FcsActiveProvidersClient : IAsyncRequestHandler<FcsProviderRequest, FcsProviderResult>
    {
        private readonly ILog _logger;
        private readonly IMetaDataHelper _metaDataHelper;

        public FcsActiveProvidersClient(IMetaDataHelper metaDataHelper, ILog logger)
        {
            _metaDataHelper = metaDataHelper;
            _logger = logger;
        }

        public async Task<FcsProviderResult> Handle(FcsProviderRequest message)
        {
            var records = _metaDataHelper.GetFcsData();
            _logger.Debug($"Retrieved {records.Count} providers on the FCS list", new Dictionary<string, object> { { "TotalCount", records.Count } });
            return new FcsProviderResult { Providers = records.Select(x => x.UkPrn) };
        }
    }
}