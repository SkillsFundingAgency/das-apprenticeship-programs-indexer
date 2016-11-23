using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers
{
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Ukrlp;

    public interface IUkrlpClient
    {
        IEnumerable<Provider> RetrieveAllProviders(ProviderQueryStructure providerQueryStructure);
    }
}
