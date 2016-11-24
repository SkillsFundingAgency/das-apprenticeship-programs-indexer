using System.Collections.Generic;
using System.Linq;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Mapping;
using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers
{
    public class UkrlpClient : IUkrlpClient
    {
        private readonly ILog _logger;

        public UkrlpClient(ILog logger)
        {
            _logger = logger;
        }

        public IEnumerable<Provider> RetrieveAllProviders(ProviderQueryStructure providerQueryStructure)
        {
            var client = new ProviderQueryPortTypeClient();

            var response = client.retrieveAllProvidersAsync(providerQueryStructure);
            _logger.Debug($"Retreived {response.Result.ProviderQueryResponse.MatchingProviderRecords.Length} Providers from UKRLP");
            var mapper = new UkrlpProviderResponseMapper();
            return response.Result.ProviderQueryResponse.MatchingProviderRecords.Select(mapper.MapFromUkrlpProviderRecord);
        }
    }
}
