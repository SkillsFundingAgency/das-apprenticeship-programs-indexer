using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Core.Services
{
    public interface IGetActiveProviders
    {
        Task<FcsProviderResult> GetActiveProviders();
    }
}