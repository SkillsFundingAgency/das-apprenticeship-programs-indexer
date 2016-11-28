using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Mapping;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers
{
    public class UkrlpClient : IUkrlpClient
    {
        private readonly ILog _logger;
        private readonly IInfrastructureSettings _settings;

        public UkrlpClient(ILog logger, IInfrastructureSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public IEnumerable<Provider> RetrieveAllProviders(ProviderQueryStructure providerQueryStructure)
        {
            _logger.Debug("Creating UKRLP client.");
            var client = new ProviderQueryPortTypeClient("ProviderQueryPort", _settings.UkrlpServiceEndpointUrl);
            _logger.Debug($"Address used for connecting to UKRLP: {client.ChannelFactory.Endpoint.Address}");

            var response = client.retrieveAllProvidersAsync(providerQueryStructure);
            _logger.Debug($"Retreived {response.Result.ProviderQueryResponse.MatchingProviderRecords.Length} Providers from UKRLP");
            var mapper = new UkrlpProviderResponseMapper();
            return response.Result.ProviderQueryResponse.MatchingProviderRecords.Select(mapper.MapFromUkrlpProviderRecord);
        }
    }
}
