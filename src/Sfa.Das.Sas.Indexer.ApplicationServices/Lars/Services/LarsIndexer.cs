namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Apprenticeship.Models.Standard;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Core.Services;
    using Sfa.Das.Sas.Indexer.Core.Shared.Models;

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

        public async Task<IndexerResult> IndexEntries(string indexName)
        {
            _log.Debug("Retrieving Lars data");
            var larsData = _metaDataHelper.GetAllApprenticeshipLarsMetaData();

            var totalAmountDocuments = GetTotalAmountDocumentsToBeIndexed(larsData);

            _log.Debug("Indexing Lars data into index");
            IndexStandards(indexName, larsData.Standards);
            IndexFrameworks(indexName, larsData.Frameworks);
            IndexFundingMetadata(indexName, larsData.FundingMetaData);
            IndexFrameworkAimMetaData(indexName, larsData.FrameworkAimMetaData);
            IndexLearningDeliveryMetaData(indexName, larsData.LearningDeliveryMetaData);
            IndexApprenticeshipComponentTypeMetaData(indexName, larsData.ApprenticeshipComponentTypeMetaData);
            IndexApprenticeshipFundingDetails(indexName, larsData.ApprenticeshipFunding);
            _log.Debug("Completed indexing Lars data");

            return new IndexerResult
            {
                IsSuccessful = IsIndexCorrectlyCreated(indexName, totalAmountDocuments),
                TotalCount = totalAmountDocuments
            };
        }

        private int GetTotalAmountDocumentsToBeIndexed(LarsData larsData)
        {
            return larsData.Standards.Count() +
                larsData.Frameworks.Count() +
                larsData.FundingMetaData.Count() +
                larsData.FrameworkAimMetaData.Count() +
                larsData.LearningDeliveryMetaData.Count() +
                larsData.ApprenticeshipComponentTypeMetaData.Count() +
                larsData.ApprenticeshipFunding.Count();
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

        private void IndexStandards(string indexName, IEnumerable<LarsStandard> standards)
        {
            try
            {
                _log.Debug("Indexing " + standards.Count() + " standards into Lars index");

                _searchIndexMaintainer.IndexStandards(indexName, standards);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS Standards");
            }
        }

        private void IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> frameworks)
        {
            try
            {
                _log.Debug("Indexing " + frameworks.Count() + " frameworks into Lars index");

                _searchIndexMaintainer.IndexFrameworks(indexName, frameworks);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS Frameworks");
            }
        }

        private void IndexFundingMetadata(string indexName, IEnumerable<FundingMetaData> fundingMetaData)
        {
            try
            {
                _log.Debug("Indexing " + fundingMetaData.Count() + " fundingMetaData details into Lars index");

                _searchIndexMaintainer.IndexFundingMetadata(indexName, fundingMetaData);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS Funding Metadata");
            }
        }

        private void IndexFrameworkAimMetaData(string indexName, IEnumerable<FrameworkAimMetaData> larsDataFrameworkAimMetaData)
        {
            try
            {
                _log.Debug("Indexing " + larsDataFrameworkAimMetaData.Count() + " frameworkaim details into Lars index");

                _searchIndexMaintainer.IndexFrameworkAimMetaData(indexName, larsDataFrameworkAimMetaData);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS FrameworkAimMetadata");
            }
        }

        private void IndexApprenticeshipComponentTypeMetaData(string indexName, IEnumerable<ApprenticeshipComponentTypeMetaData> apprenticeshipComponentTypeMetaData)
        {
            try
            {
                _log.Debug("Indexing " + apprenticeshipComponentTypeMetaData.Count() + " apprenticeship component type details into Lars index");

                _searchIndexMaintainer.IndexApprenticeshipComponentTypeMetaData(indexName, apprenticeshipComponentTypeMetaData);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS ApprenticeshipComponentType");
            }
        }

        private void IndexLearningDeliveryMetaData(string indexName, IEnumerable<LearningDeliveryMetaData> learningDeliveryMetaData)
        {
            try
            {
                _log.Debug("Indexing " + learningDeliveryMetaData.Count() + " learning delivery metadata details into Lars index");

                _searchIndexMaintainer.IndexLearningDeliveryMetaData(indexName, learningDeliveryMetaData);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS LearningDeliveryMetaData");
            }
        }

        private void IndexApprenticeshipFundingDetails(string indexName, IEnumerable<ApprenticeshipFundingMetaData> larsDataApprenticeshipFunding)
        {
            try
            {
                _log.Debug("Indexing " + larsDataApprenticeshipFunding.Count() + " apprenticeship funding metadata details");

                _searchIndexMaintainer.IndexApprenticeshipFundingDetails(indexName, larsDataApprenticeshipFunding);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing LARS ApprenticeshipFundingDetails");
            }
        }

        private int GetTotalAmountDocumentsToBeIndexed(LarsData larsData)
        {
            return larsData.Standards.Count() +
                   larsData.Frameworks.Count() +
                   larsData.FundingMetaData.Count() +
                   larsData.FrameworkAimMetaData.Count() +
                   larsData.LearningDeliveryMetaData.Count() +
                   larsData.ApprenticeshipComponentTypeMetaData.Count() +
                   larsData.ApprenticeshipFunding.Count();
        }
    }
}