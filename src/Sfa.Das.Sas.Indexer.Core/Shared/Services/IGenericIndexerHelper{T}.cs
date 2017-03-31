using System;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.Core.Services
{
    public interface IGenericIndexerHelper<T>
    {
        Task<bool> IndexEntries(string indexName);

        bool DeleteOldIndexes(DateTime scheduledRefreshDateTime);

        bool CreateIndex(string indexName);

        void ChangeUnderlyingIndexForAlias(string newIndexName);
    }
}