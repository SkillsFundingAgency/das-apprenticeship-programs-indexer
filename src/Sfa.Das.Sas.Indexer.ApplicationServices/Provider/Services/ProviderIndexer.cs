using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Extensions;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
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

        private ICollection<string> _employerProviders;
        private IEnumerable<Models.CourseDirectory.Provider> _courseDirectoryProviders;
        private IEnumerable<int> _activeProviders;
        IEnumerable<int> _courseDirectoryUkPrns;
        private IEnumerable<FrameworkMetaData> _frameworks;
        private IEnumerable<StandardMetaData> _standards;
        List<AchievementRateProvider> _achievementRateProviders;
        IEnumerable<AchievementRateNational> _achievementRateNationals;
        List<SatisfactionRateProvider> _learnerSatisfactionRates;
        List<SatisfactionRateProvider> _employerSatisfactionRates;
        ICollection<string> _heiProviders;

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
            await LoadDatasetsAsync();

            LoadDatasets();

            var providers = CreateProviders().ToList();

            var providerSiteEnteries = providers.Where(x => _courseDirectoryUkPrns.Contains(x.Ukprn)).ToList();

            var bulkStandardTasks = new List<Task<IBulkResponse>>();
            var bulkFrameworkTasks = new List<Task<IBulkResponse>>();
            var bulkProviderTasks = new List<Task<IBulkResponse>>();

            _log.Debug("Indexing " + providerSiteEnteries.Count + " provider sites");
            bulkStandardTasks.AddRange(_searchIndexMaintainer.IndexStandards(indexName, providerSiteEnteries));
            bulkFrameworkTasks.AddRange(_searchIndexMaintainer.IndexFrameworks(indexName, providerSiteEnteries));

            _log.Debug("Indexing " + providers.Count + " providers");
            bulkProviderTasks.AddRange(_searchIndexMaintainer.IndexProviders(indexName, providers));

            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkStandardTasks), "StandardProvider");
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkFrameworkTasks), "FrameworkProvider");
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkProviderTasks), "ProviderDocument");
        }

        private IEnumerable<Core.Models.Provider.Provider> CreateProviders()
        {
            foreach (var ukprn in _activeProviders)
            {
                Core.Models.Provider.Provider provider;
                if (_courseDirectoryUkPrns.Contains(ukprn))
                {
                    var courseDirectoryProvider = _courseDirectoryProviders.First(x => x.Ukprn == ukprn);
                    provider = _courseDirectoryProviderMapper.Map(courseDirectoryProvider);
                }
                else
                {
                    provider = new Core.Models.Provider.Provider {Ukprn = ukprn};
                }

                var byProvidersFiltered = _achievementRateProviders.Where(bp => bp.Ukprn == provider.Ukprn);

                provider.IsEmployerProvider = _employerProviders.Contains(provider.Ukprn.ToString());
                provider.IsHigherEducationInstitute = _heiProviders.Contains(provider.Ukprn.ToString());

                provider.Frameworks.ForEach(m => _providerDataService.UpdateFramework(m, _frameworks, byProvidersFiltered, _achievementRateNationals));
                provider.Standards.ForEach(m => _providerDataService.UpdateStandard(m, _standards, byProvidersFiltered, _achievementRateNationals));

                _providerDataService.SetLearnerSatisfactionRate(_learnerSatisfactionRates, provider);
                _providerDataService.SetEmployerSatisfactionRate(_employerSatisfactionRates, provider);

                yield return provider;
            }
        }

        private void LoadDatasets()
        {
            _employerProviders = _providerRepository.GetEmployerProviders();
            _achievementRateProviders = _achievementRatesProvider.GetAllByProvider().ToList();
            _achievementRateNationals = _achievementRatesProvider.GetAllNational();

            _learnerSatisfactionRates = _satisfactionRatesProvider.GetAllLearnerSatisfactionByProvider().ToList();
            _employerSatisfactionRates = _satisfactionRatesProvider.GetAllEmployerSatisfactionByProvider().ToList();

            _heiProviders = _providerRepository.GetHeiProviders();
        }

        private async Task LoadDatasetsAsync()
        {
            var courseDirectoryProviders = Task.Run(() => _courseDirectoryClient.GetApprenticeshipProvidersAsync());
            var activeProviders = Task.Run(() => _activeProviderClient.GetActiveProviders());

            // TODO replace this with elastic search
            var frameworks = Task.Run(() => _metaDataHelper.GetAllFrameworkMetaData());
            var standards = Task.Run(() => _metaDataHelper.GetAllStandardsMetaData());

            // From database
            await Task.WhenAll(frameworks, standards, courseDirectoryProviders, activeProviders);
            _courseDirectoryProviders = courseDirectoryProviders.Result;
            _activeProviders = activeProviders.Result;
            _courseDirectoryUkPrns = courseDirectoryProviders.Result.Select(x => x.Ukprn).ToList();
            _frameworks = frameworks.Result;
            _standards = standards.Result;
        }
    }
}