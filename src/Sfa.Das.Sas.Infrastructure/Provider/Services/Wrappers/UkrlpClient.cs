namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Services.Wrappers
{
    using System.Collections.Generic;
    using System.Linq;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Mapping;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

    public class UkrlpClient : IUkrlpClient
    {
        private readonly ILog _logger;
        private readonly IInfrastructureSettings _settings;

        public UkrlpClient(ILog logger, IInfrastructureSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public IEnumerable<ApplicationServices.Provider.Models.UkRlp.Provider> RetrieveAllProviders(ProviderQueryStructure providerQueryStructure)
        {
            using (var client = new ProviderQueryPortTypeClient("ProviderQueryPort", _settings.UkrlpServiceEndpointUrl))
            {
                var response = client.retrieveAllProviders(providerQueryStructure);
                _logger.Debug($"Retrieved {response.MatchingProviderRecords?.Length} Providers from UKRLP");
                var mapper = new UkrlpProviderResponseMapper();
                return response.MatchingProviderRecords?.Select(mapper.MapFromUkrlpProviderRecord);
            }
        }
    }
}
