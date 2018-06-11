using Sfa.Das.Sas.Indexer.Core.Shared.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;
    using Sfa.Das.Sas.Indexer.Core.Services;

    public sealed class ApprenticeshipIndexer : IGenericIndexerHelper<IMaintainApprenticeshipIndex>
    {
        private readonly IIndexSettings<IMaintainApprenticeshipIndex> _settings;
        private readonly IMediator _mediator;
        private readonly IMaintainApprenticeshipIndex _searchIndexMaintainer;
        private readonly ILog _log;

        public ApprenticeshipIndexer(
            IIndexSettings<IMaintainApprenticeshipIndex> settings,
            IMediator mediator,
            IMaintainApprenticeshipIndex searchIndexMaintainer,
            ILog log)
        {
            _settings = settings;
            _mediator = mediator;
            _searchIndexMaintainer = searchIndexMaintainer;
            _log = log;
        }

        public async Task<IndexerResult> IndexEntries(string indexName)
        {
            var standardMetadata = await LoadStandardMetaData();
            var frameworkMetaDataResults = _mediator.Send(new FrameworkMetaDataRequest());

            var totalAmountDocuments = GetTotalAmountDocumentsToBeIndexed(standardMetadata, frameworkMetaDataResults);

            IndexStandards(indexName, standardMetadata);
            IndexFrameworks(indexName, frameworkMetaDataResults);

            return new IndexerResult
            {
                IsSuccessful = IsIndexCorrectlyCreated(indexName, totalAmountDocuments),
                TotalCount = totalAmountDocuments
            };
        }

        private int GetTotalAmountDocumentsToBeIndexed(ICollection<StandardMetaData> standardMetadata, FrameworkMetaDataResult frameworkMetaDataResults)
        {
            return standardMetadata.Count + frameworkMetaDataResults.Frameworks.Count();
        }

        public bool CreateIndex(string indexName)
        {
            // If it already exists and is empty, let's delete it.
            if (_searchIndexMaintainer.IndexExists(indexName))
            {
                _log.Warn("Index already exists, deleting and creating a new one");

                _searchIndexMaintainer.DeleteIndex(indexName);
            }

            // create index
            _searchIndexMaintainer.CreateIndex(indexName);

            return _searchIndexMaintainer.IndexExists(indexName);
        }

        public bool IsIndexCorrectlyCreated(string indexName, int totalAmountDocuments)
        {
            return _searchIndexMaintainer.IndexIsCompletedAndContainsDocuments(indexName, totalAmountDocuments);
        }

        public void ChangeUnderlyingIndexForAlias(string newIndexName)
        {
            if (!_searchIndexMaintainer.AliasExists(_settings.IndexesAlias))
            {
                _log.Warn("Alias doesn't exists, creating a new one...");

                _searchIndexMaintainer.CreateIndexAlias(_settings.IndexesAlias, newIndexName);
            }

            _searchIndexMaintainer.SwapAliasIndex(_settings.IndexesAlias, newIndexName);
        }

        public bool DeleteOldIndexes(DateTime scheduledRefreshDateTime)
        {
            var today = IndexerHelper.GetIndexNameAndDateExtension(scheduledRefreshDateTime, _settings.IndexesAlias, "yyyy-MM-dd");
            var oneDayAgo = IndexerHelper.GetIndexNameAndDateExtension(scheduledRefreshDateTime.AddDays(-1), _settings.IndexesAlias, "yyyy-MM-dd");

            return _searchIndexMaintainer.DeleteIndexes(x =>
                !(x.StartsWith(today, StringComparison.InvariantCultureIgnoreCase) ||
                    x.StartsWith(oneDayAgo, StringComparison.InvariantCultureIgnoreCase)) &&
                x.StartsWith(_settings.IndexesAlias, StringComparison.InvariantCultureIgnoreCase));
        }

        private void IndexStandards(string indexName, ICollection<StandardMetaData> entries)
        {
            _log.Debug("Indexing " + entries.Count + " standards");

            _searchIndexMaintainer.IndexStandards(indexName, entries);
        }

        private void IndexFrameworks(string indexName, FrameworkMetaDataResult entries)
        {
            _log.Debug("Indexing " + entries.Frameworks.Count() + " frameworks");

            _searchIndexMaintainer.IndexFrameworks(indexName, entries.Frameworks);
        }

        private Task<ICollection<StandardMetaData>> LoadStandardMetaData()
        {
            var standardsMetaData = _mediator.Send(new StandardMetaDataRequest());
            return Task.FromResult<ICollection<StandardMetaData>>(standardsMetaData.Standards.ToList());
        }
    }
}