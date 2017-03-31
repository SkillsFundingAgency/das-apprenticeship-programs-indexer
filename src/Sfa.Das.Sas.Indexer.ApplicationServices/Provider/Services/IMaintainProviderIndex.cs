using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IMaintainProviderIndex : IMaintainSearchIndexes
    {
        Task IndexProviders(string indexName, ICollection<Core.Models.Provider.Provider> entries);

        Task IndexApiProviders(string indexName, ICollection<Core.Models.Provider.Provider> entries);

        Task IndexStandards(string indexName, ICollection<Core.Models.Provider.Provider> entries);

        Task IndexFrameworks(string indexName, ICollection<Core.Models.Provider.Provider> indexEntries);

        void LogResponse(IBulkResponse[] elementIndexResult, string documentType);
    }
}