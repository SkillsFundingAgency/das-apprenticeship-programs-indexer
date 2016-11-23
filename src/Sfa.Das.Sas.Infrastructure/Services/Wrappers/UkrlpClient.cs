using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.Infrastructure.Mapping;
using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers
{
    public class UkrlpClient : IUkrlpClient
    {
        public IEnumerable<Provider> RetrieveAllProviders(ProviderQueryStructure providerQueryStructure)
        {
            var client = new ProviderQueryPortTypeClient();

            var response = client.retrieveAllProvidersAsync(providerQueryStructure);
            var mapper = new UkrlpProviderWrapperMapper();
            return response.Result.ProviderQueryResponse.MatchingProviderRecords.Select(mapper.MapFromUkrlpProviderRecord);
        }
    }
}
