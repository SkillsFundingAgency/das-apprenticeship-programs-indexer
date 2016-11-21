using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Standard;
using Sfa.Das.Sas.Indexer.Core.Extensions;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider
{
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;

    public sealed class ProviderIndexer : IGenericIndexerHelper<IMaintainProviderIndex>
    {
        private readonly IMaintainProviderIndex _searchIndexMaintainer;

        private readonly IProviderDataService _providerDataService;

        private readonly IIndexSettings<IMaintainProviderIndex> _settings;
        private readonly IGetApprenticeshipProviders _providerRepository;
        private readonly IGetCourseDirectoryProviders _courseDirectoryClient;
        private readonly ICourseDirectoryProviderMapper _courseDirectoryProviderMapper;
        private readonly IMetaDataHelper _metaDataHelper;
        private readonly IAchievementRatesProvider _achievementRatesProvider;
        private readonly ISatisfactionRatesProvider _satisfactionRatesProvider;
        private readonly IGetActiveProviders _activeProviderClient;

        private readonly ILog _log;

        public ProviderIndexer(
            IIndexSettings<IMaintainProviderIndex> settings,
            IGetApprenticeshipProviders providerRepository,
            IGetCourseDirectoryProviders courseDirectoryClient,
            ICourseDirectoryProviderMapper courseDirectoryProviderMapper,
            IMetaDataHelper metaDataHelper,
            IAchievementRatesProvider achievementRatesProvider,
            ISatisfactionRatesProvider satisfactionRatesProvider,
            IGetActiveProviders activeProviderClient,
            IMaintainProviderIndex searchIndexMaintainer,
            IProviderDataService providerDataService,
            ILog log)
        {
            _settings = settings;
            _providerRepository = providerRepository;
            _courseDirectoryClient = courseDirectoryClient;
            _courseDirectoryProviderMapper = courseDirectoryProviderMapper;
            _metaDataHelper = metaDataHelper;
            _achievementRatesProvider = achievementRatesProvider;
            _satisfactionRatesProvider = satisfactionRatesProvider;
            _activeProviderClient = activeProviderClient;
            _searchIndexMaintainer = searchIndexMaintainer;
            _providerDataService = providerDataService;
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
                _log.Warn("Alias doesn't exists, creating a new one...");

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
            // From Course directory
            var employerProviders = _providerRepository.GetEmployerProviders();

            var courseDirectoryProviders = Task.Run(() => _courseDirectoryClient.GetApprenticeshipProvidersAsync());

            // FCS
            var activeProviders = Task.Run(() => _activeProviderClient.GetActiveProviders());

            // From LARS
            // TODO replace this with elastic search
            var frameworks = Task.Run(() => _metaDataHelper.GetAllFrameworkMetaData());
            var standards = Task.Run(() => _metaDataHelper.GetAllStandardsMetaData());

            // From database
            var byProvider = _achievementRatesProvider.GetAllByProvider().ToList();
            var national = _achievementRatesProvider.GetAllNational();

            var learnerSatisfactionRates = _satisfactionRatesProvider.GetAllLearnerSatisfactionByProvider().ToList();
            var employerSatisfactionRates = _satisfactionRatesProvider.GetAllEmployerSatisfactionByProvider().ToList();

            var heiProviders = _providerRepository.GetHeiProviders();

            await Task.WhenAll(frameworks, standards, courseDirectoryProviders, activeProviders);
            var courseDirectoryUkPrns = courseDirectoryProviders.Result.Select(x => x.Ukprn).ToList();

            var ps = new List<Provider>();

            foreach (var ukprn in activeProviders.Result)
            {
                Provider provider;
                if (courseDirectoryUkPrns.Contains(ukprn))
                {
                    var courseDirectoryProvider = courseDirectoryProviders.Result.First(x => x.Ukprn == ukprn);
                    provider = _courseDirectoryProviderMapper.Map(courseDirectoryProvider);
                }
                else
                {
                    provider = new Provider { Ukprn = ukprn };
                }
                
                var byProvidersFiltered = byProvider.Where(bp => bp.Ukprn == provider.Ukprn);

                provider.IsEmployerProvider = employerProviders.Contains(provider.Ukprn.ToString());
                provider.IsHigherEducationInstitute = heiProviders.Contains(provider.Ukprn.ToString());

                provider.Frameworks.ForEach(m => _providerDataService.UpdateFramework(m, frameworks.Result, byProvidersFiltered, national));
                provider.Standards.ForEach(m => _providerDataService.UpdateStandard(m, standards.Result, byProvidersFiltered, national));

                _providerDataService.SetLearnerSatisfactionRate(learnerSatisfactionRates, provider);
                _providerDataService.SetEmployerSatisfactionRate(employerSatisfactionRates, provider);

                ps.Add(provider);
            }

            var providerSiteEnteries = ps.Where(x => courseDirectoryUkPrns.Contains(x.Ukprn)).ToList();

            var bulkStandardTasks = new List<Task<IBulkResponse>>();
            var bulkFrameworkTasks = new List<Task<IBulkResponse>>();
            var bulkProviderTasks = new List<Task<IBulkResponse>>();

            _log.Debug("Indexing " + providerSiteEnteries.Count() + " provider sites");
            bulkStandardTasks.AddRange(_searchIndexMaintainer.IndexStandards(indexName, providerSiteEnteries));
            bulkFrameworkTasks.AddRange(_searchIndexMaintainer.IndexFrameworks(indexName, providerSiteEnteries));

            _log.Debug("Indexing " + ps.Count() + " providers");
            bulkProviderTasks.AddRange(_searchIndexMaintainer.IndexProviders(indexName, ps));

            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkStandardTasks), "StandardProvider");
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkFrameworkTasks), "FrameworkProvider");
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkProviderTasks), "ProviderDocument");
        }
    }
}