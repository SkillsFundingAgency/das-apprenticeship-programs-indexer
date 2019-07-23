using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{


    public class RoatpClient : IAsyncRequestHandler<RoatpRequest, List<RoatpProviderResult>>
    {
        private readonly ILog _logger;
        private readonly IRoatpApiClient _apiClient;
        private readonly IRoatpMapper _mapper;
        private readonly ProviderType[] _validProviderTypes = { ProviderType.MainProvider, ProviderType.EmployerProvider };

        public RoatpClient(IRoatpApiClient apiClient, ILog logger, IRoatpMapper mapper)
        {
            _apiClient = apiClient;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<RoatpProviderResult>> Handle(RoatpRequest message)
        {
            var roatpSummaries = await _apiClient.GetRoatpSummary();

            // MFCMFC
            // add logging
            var records = _mapper.Map(roatpSummaries);

            _logger.Debug($"Retrieved {records.Count} providers on the ROATP list", new Dictionary<string, object> { { "TotalCount", records.Count } });
            var filtered = records.Where(x => _validProviderTypes.Contains(x.ProviderType) && x.IsDateValid()).ToList();
            _logger.Debug($"Filtered out Supporting providers on ROATP", new Dictionary<string, object> { { "TotalCount", filtered.Count } });
            return filtered;

        }
    }
}
