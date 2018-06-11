using System;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.Core.Shared.Models;

namespace Sfa.Das.Sas.Indexer.Core.Services
{
    public interface IGenericIndexerHelper<T>
    {
        Task<IndexerResult> IndexEntries(string indexName);

        bool DeleteOldIndexes(DateTime scheduledRefreshDateTime);

        bool CreateIndex(string indexName);

        void ChangeUnderlyingIndexForAlias(string newIndexName);
    }
}