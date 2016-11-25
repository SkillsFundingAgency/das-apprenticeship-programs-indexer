﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    public sealed class ApprenticeshipIndexer : IGenericIndexerHelper<IMaintainApprenticeshipIndex>
    {
        private readonly IIndexSettings<IMaintainApprenticeshipIndex> _settings;
        private readonly IMaintainApprenticeshipIndex _searchIndexMaintainer;
        private readonly IMetaDataHelper _metaDataHelper;
        private readonly ILogApprenticeships _log;

        public ApprenticeshipIndexer(
            IIndexSettings<IMaintainApprenticeshipIndex> settings,
            IMaintainApprenticeshipIndex searchIndexMaintainer,
            IMetaDataHelper metaDataHelper,
            ILogApprenticeships log)
        {
            _settings = settings;
            _searchIndexMaintainer = searchIndexMaintainer;
            _metaDataHelper = metaDataHelper;
            _log = log;
        }

        public async Task IndexEntries(string indexName)
        {
            _metaDataHelper.UpdateMetadataRepository();

            await IndexStandards(indexName).ConfigureAwait(false);
            await IndexFrameworks(indexName).ConfigureAwait(false);
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

        public bool IsIndexCorrectlyCreated(string indexName)
        {
            return _searchIndexMaintainer.IndexContainsDocuments(indexName);
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

        private async Task IndexStandards(string indexName)
        {
            var entries = await LoadStandardMetaData();

            _log.Debug("Indexing " + entries.Count + " standards");

            await _searchIndexMaintainer.IndexStandards(indexName, entries).ConfigureAwait(false);
        }

        private async Task IndexFrameworks(string indexName)
        {
            try
            {
                var entries = _metaDataHelper.GetAllFrameworkMetaData();

                _log.Debug("Indexing " + entries.Count() + " frameworks");

                await _searchIndexMaintainer.IndexFrameworks(indexName, entries).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing Frameworks");
            }
        }

        private Task<ICollection<StandardMetaData>> LoadStandardMetaData()
        {
            var standardsMetaData = _metaDataHelper.GetAllStandardsMetaData();
            return Task.FromResult<ICollection<StandardMetaData>>(standardsMetaData.ToList());
        }
    }
}