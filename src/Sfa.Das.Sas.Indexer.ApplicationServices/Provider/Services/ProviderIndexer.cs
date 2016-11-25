using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Extensions;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Services;
using CoreProvider = Sfa.Das.Sas.Indexer.Core.Models.Provider.Provider;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public sealed class ProviderIndexer : IGenericIndexerHelper<IMaintainProviderIndex>
    {
        private readonly IMaintainProviderIndex _searchIndexMaintainer;

        private readonly IProviderDataService _providerDataService;

        private readonly IIndexSettings<IMaintainProviderIndex> _settings;
        private readonly ICourseDirectoryProviderMapper _courseDirectoryProviderMapper;
        private readonly IUkrlpProviderMapper _ukrlpProviderMapper;
        private readonly ILogProvider _log;

        public ProviderIndexer(
            IIndexSettings<IMaintainProviderIndex> settings,
            ICourseDirectoryProviderMapper courseDirectoryProviderMapper,
            IUkrlpProviderMapper ukrlpProviderMapper,
            IMaintainProviderIndex searchIndexMaintainer,
            IProviderDataService providerDataService,
            ILogProvider log)
        {
            _settings = settings;
            _providerDataService = providerDataService;
            _courseDirectoryProviderMapper = courseDirectoryProviderMapper;
            _ukrlpProviderMapper = ukrlpProviderMapper;
            _searchIndexMaintainer = searchIndexMaintainer;
            _log = log;
        }

        public bool CreateIndex(string indexName)
        {
            var indexExists = _searchIndexMaintainer.IndexExists(indexName);

            // If it already exists and is empty, let's delete it.
            if (indexExists)
            {
                _log.Warn("Index already exists, deleting and creating a new one");

                _searchIndexMaintainer.DeleteIndex(indexName);
            }

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
                _log.Warn("Alias doesn't exist, creating a new one...");

                _searchIndexMaintainer.CreateIndexAlias(_settings.IndexesAlias, newIndexName);
            }
            else
            {
                _searchIndexMaintainer.SwapAliasIndex(_settings.IndexesAlias, newIndexName);
            }
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

        public async Task IndexEntries(string indexName)
        {
            _log.Debug("Loading data at provider index");
            var source = _providerDataService.LoadDatasetsAsync().Result;

            _log.Debug("Creating providers");
            var providers = CreateProviders(source).ToList();

            var providerSiteEntries = providers.Where(x => source.CourseDirectoryUkPrns.Contains(x.Ukprn)).ToList();

            var bulkStandardTasks = new List<Task<IBulkResponse>>();
            var bulkFrameworkTasks = new List<Task<IBulkResponse>>();
            var bulkProviderTasks = new List<Task<IBulkResponse>>();

            _log.Debug("Indexing " + providerSiteEntries.Count + " provider sites");
            bulkStandardTasks.AddRange(_searchIndexMaintainer.IndexStandards(indexName, providerSiteEntries));
            bulkFrameworkTasks.AddRange(_searchIndexMaintainer.IndexFrameworks(indexName, providerSiteEntries));

            _log.Debug("Indexing " + providers.Count + " providers");
            bulkProviderTasks.AddRange(_searchIndexMaintainer.IndexProviders(indexName, providers));

            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkStandardTasks), "StandardProvider");
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkFrameworkTasks), "FrameworkProvider");
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkProviderTasks), "ProviderDocument");
        }

        private IEnumerable<CoreProvider> CreateProviders(ProviderSourceDto source)
        {
            foreach (var ukprn in source.ActiveProviders)
            {
                var ukrlpProvider = source.UkrlpProviders.MatchingProviderRecords.FirstOrDefault(x => x.UnitedKingdomProviderReferenceNumber == ukprn.ToString());

                if (ukrlpProvider != null)
                {
                    CoreProvider provider;

                    if (source.CourseDirectoryUkPrns.Contains(ukprn))
                    {
                        var courseDirectoryProvider = source.CourseDirectoryProviders.First(x => x.Ukprn == ukprn);
                        provider = _courseDirectoryProviderMapper.Map(courseDirectoryProvider);
                    }
                    else
                    {
                        provider = _ukrlpProviderMapper.Map(ukrlpProvider);
                    }

                    provider.LegalName = ukrlpProvider.ProviderName;
                    provider.Addresses = ukrlpProvider.ProviderContact.Select(_ukrlpProviderMapper.MapAddress);

                    var byProvidersFiltered = source.AchievementRateProviders.Where(bp => bp.Ukprn == provider.Ukprn);

                    provider.IsEmployerProvider = source.EmployerProviders.Contains(provider.Ukprn.ToString());
                    provider.IsHigherEducationInstitute = source.HeiProviders.Contains(provider.Ukprn.ToString());

                    provider.Frameworks.ForEach(m => _providerDataService.UpdateFramework(m, source.Frameworks, byProvidersFiltered, source.AchievementRateNationals));
                    provider.Standards.ForEach(m => _providerDataService.UpdateStandard(m, source.Standards, byProvidersFiltered, source.AchievementRateNationals));

                    _providerDataService.SetLearnerSatisfactionRate(source.LearnerSatisfactionRates, provider);
                    _providerDataService.SetEmployerSatisfactionRate(source.EmployerSatisfactionRates, provider);

                    yield return provider;
                }
            }
        }
    }
}