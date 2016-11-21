using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Services
{
    public interface IMaintainProviderIndex : IMaintainSearchIndexes
    {
        Task IndexEntries(string indexName, ICollection<Core.Models.Provider.Provider> entries);
        List<Task<IBulkResponse>> IndexFrameworks(string indexName, ICollection<Core.Models.Provider.Provider> indexEntries);
        List<Task<IBulkResponse>> IndexProviders(string indexName, ICollection<Core.Models.Provider.Provider> indexEntries);
        List<Task<IBulkResponse>> IndexStandards(string indexName, IEnumerable<Core.Models.Provider.Provider> indexEntries);
        void LogResponse(IBulkResponse[] elementIndexResult, string documentType);
    }
}