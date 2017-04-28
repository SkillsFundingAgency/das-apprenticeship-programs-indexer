using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;

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
        private readonly IGetRoatpProviders _getRoatpProviders;
        private readonly ProviderType[] _validProviderTypes = { ProviderType.MainProvider, ProviderType.EmployerProvider };


        public RoatpProvidersClient(IGetRoatpProviders getRoatpProviders, ILog logger)
        {
            _getRoatpProviders = getRoatpProviders;
            _logger = logger;
        }

        public async Task<List<RoatpProviderResult>> Handle(RoatpProviderRequest message)
        {
            _logger.Debug("Starting to retreive RoATP providers");
            var records = _getRoatpProviders.GetRoatpData();
            _logger.Debug($"Retrieved {records.Count} providers on the ROATP list", new Dictionary<string, object> { { "TotalCount", records.Count } });
            var filtered = records.Where(x => _validProviderTypes.Contains(x.ProviderType) && x.IsDateValid()).ToList();
            _logger.Debug($"Filtered out Supporting providers on ROATP", new Dictionary<string, object> { { "TotalCount", filtered.Count } });
            return filtered;
        }
    }
}