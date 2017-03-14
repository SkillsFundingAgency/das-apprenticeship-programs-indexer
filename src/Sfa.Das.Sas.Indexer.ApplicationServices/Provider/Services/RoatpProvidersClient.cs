namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;

    public class RoatpProvidersClient : IAsyncRequestHandler<RoatpProviderRequest, List<RoatpProviderResult>>
    {
        private readonly ILog _logger;

        private readonly IVstsClient _vstsClient;
        private readonly IGetRoatpProviders _getRoatpProviders;

        public RoatpProvidersClient(IVstsClient vstsClient, IGetRoatpProviders getRoatpProviders, ILog logger)
        {
            _vstsClient = vstsClient;
            _getRoatpProviders = getRoatpProviders;
            _logger = logger;
        }

        public async Task<List<RoatpProviderResult>> Handle(RoatpProviderRequest message)
        {
            _logger.Debug("Starting to retreive RoATP providers");
            var records = _getRoatpProviders.GetRoatpData();
            _logger.Debug($"Retrieved {records.Count} providers on the FCS list");
            return records;
        }
    }
}