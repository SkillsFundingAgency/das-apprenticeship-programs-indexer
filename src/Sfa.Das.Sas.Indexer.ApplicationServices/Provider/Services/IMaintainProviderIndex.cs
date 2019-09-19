using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IMaintainProviderIndex : IMaintainSearchIndexes
    {
        void IndexApiProviders(string indexName, ICollection<Core.Models.Provider.Provider> entries);

        void IndexStandards(string indexName, ICollection<Core.Models.Provider.Provider> entries);

        void IndexFrameworks(string indexName, ICollection<Core.Models.Provider.Provider> indexEntries);

        void LogResponse(BulkResponse[] elementIndexResult, string documentType);
    }
}