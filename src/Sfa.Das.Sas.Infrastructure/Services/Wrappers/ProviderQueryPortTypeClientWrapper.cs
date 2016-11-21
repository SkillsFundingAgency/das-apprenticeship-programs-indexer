using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.Infrastructure.Uklrp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers
{
    public class ProviderQueryPortTypeClientWrapper : IProviderQueryPortTypeClientWrapper
    {
        public Task<response> RetrieveAllProvidersAsync(ProviderQueryStructure providerQueryStructure)
        {
            var client = new ProviderQueryPortTypeClient();
            return client.retrieveAllProvidersAsync(providerQueryStructure);
        }
    }
}
