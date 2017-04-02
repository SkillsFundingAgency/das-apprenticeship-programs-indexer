using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Services;

    public sealed class AssessmentOrgsIndexer : IGenericIndexerHelper<IMaintainAssessmentOrgsIndex>
    {
        private readonly IIndexSettings<IMaintainAssessmentOrgsIndex> _settings;
        private readonly IMaintainAssessmentOrgsIndex _assessmentOrgsIndexMaintainer;
        private readonly IMetaDataHelper _metaDataHelper;
        private readonly ILog _log;

        public AssessmentOrgsIndexer(
            IIndexSettings<IMaintainAssessmentOrgsIndex> settings,
            IMaintainAssessmentOrgsIndex assessmentOrgsIndexMaintainer,
            IMetaDataHelper metaDataHelper,
            ILog log)
        {
            _settings = settings;
            _assessmentOrgsIndexMaintainer = assessmentOrgsIndexMaintainer;
            _metaDataHelper = metaDataHelper;
            _log = log;
        }

        public async Task IndexEntries(string indexName)
        {
            _log.Debug("Retrieving Assessment Orgs data");
            var assessmentOrgsData = _metaDataHelper.GetAssessmentOrganisationsData();

            _log.Debug("Indexing Assessment Orgs data into index");
            await IndexOrganisations(indexName, assessmentOrgsData.Organisations).ConfigureAwait(false);
            await IndexStandardOrganisationsData(indexName, assessmentOrgsData.StandardOrganisationsData).ConfigureAwait(false);
            _log.Debug("Completed indexing Assessment Orgs data");
        }

        private async Task IndexStandardOrganisationsData(string indexName, List<StandardOrganisationsData> standardOrganisationsData)
        {
            try
            {
                _log.Debug("Indexing " + standardOrganisationsData.Count + " standard organisations data into Assessment Organisations index");

                await _assessmentOrgsIndexMaintainer.IndexStandardOrganisationsData(indexName, standardOrganisationsData).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing Standard Organisations data");
            }
        }

        private async Task IndexOrganisations(string indexName, List<Organisation> organisations)
        {
            try
            {
                _log.Debug("Indexing " + organisations.Count + " organisations into Assessment Organisations index");

                await _assessmentOrgsIndexMaintainer.IndexOrganisations(indexName, organisations).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing Assessment Organisations");
            }
        }

        public bool CreateIndex(string indexName)
        {
            // If it already exists and is empty, let's delete it.
            if (_assessmentOrgsIndexMaintainer.IndexExists(indexName))
            {
                _log.Warn("Index already exists, deleting and creating a new one");

                _assessmentOrgsIndexMaintainer.DeleteIndex(indexName);
            }

            // create index
            _assessmentOrgsIndexMaintainer.CreateIndex(indexName);

            return _assessmentOrgsIndexMaintainer.IndexExists(indexName);
        }

        public bool IsIndexCorrectlyCreated(string indexName)
        {
            return _assessmentOrgsIndexMaintainer.IndexContainsDocuments(indexName);
        }

        public void ChangeUnderlyingIndexForAlias(string newIndexName)
        {
            if (!_assessmentOrgsIndexMaintainer.AliasExists(_settings.IndexesAlias))
            {
                _log.Warn("Alias doesn't exists, creating a new one...");

                _assessmentOrgsIndexMaintainer.CreateIndexAlias(_settings.IndexesAlias, newIndexName);
            }

            _assessmentOrgsIndexMaintainer.SwapAliasIndex(_settings.IndexesAlias, newIndexName);
        }

        public bool DeleteOldIndexes(DateTime scheduledRefreshDateTime)
        {
            var today = IndexerHelper.GetIndexNameAndDateExtension(scheduledRefreshDateTime, _settings.IndexesAlias, "yyyy-MM-dd");
            var oneDayAgo = IndexerHelper.GetIndexNameAndDateExtension(scheduledRefreshDateTime.AddDays(-1), _settings.IndexesAlias, "yyyy-MM-dd");

            return _assessmentOrgsIndexMaintainer.DeleteIndexes(x =>
                !(x.StartsWith(today, StringComparison.InvariantCultureIgnoreCase) ||
                  x.StartsWith(oneDayAgo, StringComparison.InvariantCultureIgnoreCase)) &&
                x.StartsWith(_settings.IndexesAlias, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}