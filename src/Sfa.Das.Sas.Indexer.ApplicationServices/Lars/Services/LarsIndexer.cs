namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Core.Services;

    public sealed class LarsIndexer : IGenericIndexerHelper<IMaintainLarsIndex>
    {
        private readonly IIndexSettings<IMaintainLarsIndex> _settings;
        private readonly IMaintainLarsIndex _searchIndexMaintainer;
        private readonly IMetaDataHelper _metaDataHelper;
        private readonly ILog _log;

        public LarsIndexer(
            IIndexSettings<IMaintainLarsIndex> settings,
            IMaintainLarsIndex searchIndexMaintainer,
            IMetaDataHelper metaDataHelper,
            ILog log)
        {
            _settings = settings;
            _searchIndexMaintainer = searchIndexMaintainer;
            _metaDataHelper = metaDataHelper;
            _log = log;
        }

        public async Task IndexEntries(string indexName)
        {
            // TODO: load LARS data
            _log.Debug("Retrieving Lars data");
            var larsData = _metaDataHelper.GetAllApprenticeshipLarsMetaData();

            // TODO: index LARS data
            _log.Debug("Indexing Lars data into index");
            await IndexStandards(indexName, larsData.Standards).ConfigureAwait(false);
            await IndexFrameworks(indexName, larsData.Frameworks).ConfigureAwait(false);
            await IndexFundingMetadata(indexName, larsData.FundingMetaData).ConfigureAwait(false);
            await IndexFrameworkAimMetaData(indexName, larsData.FrameworkAimMetaData).ConfigureAwait(false);
            await IndexLearningDeliveryMetaData(indexName, larsData.LearningDeliveryMetaData).ConfigureAwait(false);
            await IndexApprenticeshipComponentTypeMetaData(indexName, larsData.ApprenticeshipComponentTypeMetaData).ConfigureAwait(false);
            await IndexApprenticeshipFundingDetails(indexName, larsData.ApprenticeshipFunding).ConfigureAwait(false);
            _log.Debug("Completed indexing Lars data");
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

        private async Task IndexStandards(string indexName, IEnumerable<LarsStandard> standards)
        {
            try
            {
                _log.Debug("Indexing " + standards.Count() + " standards into Lars index");

                await _searchIndexMaintainer.IndexStandards(indexName, standards).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS Standards");
            }
        }

        private async Task IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> frameworks)
        {
            try
            {
                _log.Debug("Indexing " + frameworks.Count() + " frameworks into Lars index");

                await _searchIndexMaintainer.IndexFrameworks(indexName, frameworks).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS Frameworks");
            }
        }

        private async Task IndexFundingMetadata(string indexName, IEnumerable<FundingMetaData> fundingMetaData)
        {
            try
            {
                _log.Debug("Indexing " + fundingMetaData.Count() + " fundingMetaData details into Lars index");

                await _searchIndexMaintainer.IndexFundingMetadata(indexName, fundingMetaData).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS Funding Metadata");
            }
        }

        private async Task IndexFrameworkAimMetaData(string indexName, IEnumerable<FrameworkAimMetaData> larsDataFrameworkAimMetaData)
        {
            try
            {
                _log.Debug("Indexing " + larsDataFrameworkAimMetaData.Count() + " frameworkaim details into Lars index");

                await _searchIndexMaintainer.IndexFrameworkAimMetaData(indexName, larsDataFrameworkAimMetaData).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS FrameworkAimMetadata");
            }
        }

        private async Task IndexApprenticeshipComponentTypeMetaData(string indexName, IEnumerable<ApprenticeshipComponentTypeMetaData> apprenticeshipComponentTypeMetaData)
        {
            try
            {
                _log.Debug("Indexing " + apprenticeshipComponentTypeMetaData.Count() + " apprenticeship component type details into Lars index");

                await _searchIndexMaintainer.IndexApprenticeshipComponentTypeMetaData(indexName, apprenticeshipComponentTypeMetaData).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS ApprenticeshipComponentType");
            }
        }

        private async Task IndexLearningDeliveryMetaData(string indexName, IEnumerable<LearningDeliveryMetaData> learningDeliveryMetaData)
        {
            try
            {
                _log.Debug("Indexing " + learningDeliveryMetaData.Count() + " learning delivery metadata details into Lars index");

                await _searchIndexMaintainer.IndexLearningDeliveryMetaData(indexName, learningDeliveryMetaData).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS LearningDeliveryMetaData");
            }
        }

        private async Task IndexApprenticeshipFundingDetails(string indexName, IEnumerable<ApprenticeshipFunding> larsDataApprenticeshipFunding)
        {
            try
            {
                _log.Debug("Indexing " + larsDataApprenticeshipFunding.Count() + " learning delivery metadata details");

                await _searchIndexMaintainer.IndexApprenticeshipFundingDetails(indexName, larsDataApprenticeshipFunding).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS ApprenticeshipFundingDetails");
            }
        }
    }
}