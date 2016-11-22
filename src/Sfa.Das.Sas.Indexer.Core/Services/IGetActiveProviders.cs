using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.Core.Services
{
    public interface IGetActiveProviders
    {
        Task<IEnumerable<int>> GetActiveProviders();
    }
}