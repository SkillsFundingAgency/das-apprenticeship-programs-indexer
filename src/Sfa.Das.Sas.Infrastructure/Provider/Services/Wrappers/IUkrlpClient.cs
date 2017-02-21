using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Services.Wrappers
{
    public interface IUkrlpClient
    {
        IEnumerable<ApplicationServices.Provider.Models.UkRlp.Provider> RetrieveAllProviders(ProviderQueryStructure providerQueryStructure);
    }
}
