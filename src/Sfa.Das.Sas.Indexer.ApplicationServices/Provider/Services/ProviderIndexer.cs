using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Newtonsoft.Json;
using Sfa.Das.Sas.Indexer.Core.Logging.Metrics;
using Sfa.Das.Sas.Indexer.Core.Logging.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Nest;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Extensions;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;
    using Sfa.Das.Sas.Indexer.Core.Services;
    using CoreProvider = Sfa.Das.Sas.Indexer.Core.Models.Provider.Provider;

    public sealed class ProviderIndexer : IGenericIndexerHelper<IMaintainProviderIndex>
    {
        private readonly IMaintainProviderIndex _searchIndexMaintainer;

        private readonly IProviderDataService _providerDataService;

        private readonly IIndexSettings<IMaintainProviderIndex> _settings;
        private readonly ICourseDirectoryProviderMapper _courseDirectoryProviderMapper;
        private readonly IUkrlpProviderMapper _ukrlpProviderMapper;
        private readonly ILog _log;

        public ProviderIndexer(
            IIndexSettings<IMaintainProviderIndex> settings,
            ICourseDirectoryProviderMapper courseDirectoryProviderMapper,
            IUkrlpProviderMapper ukrlpProviderMapper,
            IMaintainProviderIndex searchIndexMaintainer,
            IProviderDataService providerDataService,
            ILog log)
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

        public bool IsIndexCorrectlyCreated(string indexName, int totalAmountDocuments)
        {
            return _searchIndexMaintainer.IndexIsCompletedAndContainsDocuments(indexName, totalAmountDocuments);
        }

        public void ChangeUnderlyingIndexForAlias(string newIndexName)
        {
            if (!_searchIndexMaintainer.AliasExists(_settings.IndexesAlias))
            {
                _log.Warn("Alias doesn't exist, creating a new one...");

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

        public async Task<bool> IndexEntries(string indexName)
        {
            var bulkStandardTasks = new List<Task<IBulkResponse>>();
            var bulkFrameworkTasks = new List<Task<IBulkResponse>>();
            var bulkApiProviderTasks = new List<Task<IBulkResponse>>();

            // Load data
            var timing = ExecutionTimer.GetTiming(() => _providerDataService.LoadDatasetsAsync());

            var source = await timing.Result;
            _log.Debug("Loaded data for provider index", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            // Providers
            var providers = CreateProviders(source).ToList();
            var providersApi = CreateApiProviders(source).ToList();
            var apprenticeshipProviders = CreateApprenticeshipProviders(source).ToList();

            _log.Debug("Indexing " + providersApi.Count + " API providers", new Dictionary<string, object> { { "TotalCount", providersApi.Count } });
            var totalAmountDocuments = GetTotalAmountDocumentsToBeIndexed(providers, providersApi, apprenticeshipProviders);
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkApiProviderTasks), "ProviderApiDocument");

            // Provider Sites
            await IndexProviders(indexName, providers);

            _log.Debug("Indexing " + apprenticeshipProviders.Count + " provider sites", new Dictionary<string, object> { { "TotalCount", apprenticeshipProviders.Count } });
            await IndexApiProviders(indexName, providersApi);

            await IndexStandards(indexName, apprenticeshipProviders);
            await IndexFrameworks(indexName, apprenticeshipProviders);
            Task.WaitAll();

            return IsIndexCorrectlyCreated(indexName, totalAmountDocuments);
        }

        private async Task IndexProviders(string indexName, ICollection<CoreProvider> providers)
        {
            try
            {
                _log.Debug($"Indexing {providers.Count} providers into Providers index");

                await _searchIndexMaintainer.IndexProviders(indexName, providers).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing Providers");
            }
        }

        private async Task IndexApiProviders(string indexName, ICollection<CoreProvider> providers)
        {
            try
            {
                _log.Debug($"Indexing {providers.Count} API providers into Providers index");

                await _searchIndexMaintainer.IndexApiProviders(indexName, providers).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing API Providers");
            }
        }

        private async Task IndexStandards(string indexName, ICollection<CoreProvider> apprenticeshipProviders)
        {
            try
            {
                _log.Debug($"Indexing {apprenticeshipProviders.Count} standard providers into Providers index");

                await _searchIndexMaintainer.IndexStandards(indexName, apprenticeshipProviders).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing Standard Providers");
            }
        }

        private async Task IndexFrameworks(string indexName, ICollection<CoreProvider> apprenticeshipProviders)
        {
            try
            {
                _log.Debug($"Indexing {apprenticeshipProviders.Count} framework providers into Providers index");

                await _searchIndexMaintainer.IndexFrameworks(indexName, apprenticeshipProviders).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Error indexing Framework Providers");
            }
        }

        private int GetTotalAmountDocumentsToBeIndexed(List<CoreProvider> providers, List<CoreProvider> providersApi, List<CoreProvider> apprenticeshipProviders)
        {
            Func<DeliveryInformation, bool> _onlyAtEmployer = x => x.DeliveryModes.All(xx => xx == ModesOfDelivery.OneHundredPercentEmployer);
            Func<DeliveryInformation, bool> _anyNotAtEmployer = x => x.DeliveryModes.Any(xx => xx != ModesOfDelivery.OneHundredPercentEmployer);

            var count = providers.Count();

            count += providersApi.Count();

            foreach (var apprenticeshipProvider in apprenticeshipProviders)
            {
                foreach (var apprenticeshipProviderFramework in apprenticeshipProvider.Frameworks)
                {
                    var deliveryLocationsOnly100 = apprenticeshipProviderFramework.DeliveryLocations
                            .Where(_onlyAtEmployer)
                            .Where(x => x.DeliveryLocation.Address.GeoPoint != null)
                            .ToArray();

                    if (deliveryLocationsOnly100.Any())
                    {
                        count += deliveryLocationsOnly100.Count();
                    }

                    count += apprenticeshipProviderFramework.DeliveryLocations.Where(_anyNotAtEmployer).Count(location => location.DeliveryLocation.Address.GeoPoint != null);
                }

                foreach (var apprenticeshipProviderStandard in apprenticeshipProvider.Standards)
                {
                    var deliveryLocationsOnly100 = apprenticeshipProviderStandard.DeliveryLocations
                            .Where(_onlyAtEmployer)
                            .Where(x => x.DeliveryLocation.Address.GeoPoint != null)
                            .ToArray();

                    if (deliveryLocationsOnly100.Any())
                    {
                        count += deliveryLocationsOnly100.Count();
                    }

                    count += apprenticeshipProviderStandard.DeliveryLocations.Where(_anyNotAtEmployer).Count(location => location.DeliveryLocation.Address.GeoPoint != null);
                }
            }

            return count;
        }

        private IEnumerable<CoreProvider> CreateProviders(ProviderSourceDto source)
        {
            foreach (var ukprn in source.ActiveProviders.Providers)
            {
                var ukrlpProvider = source.UkrlpProviders.FirstOrDefault(x => x.UnitedKingdomProviderReferenceNumber == ukprn.ToString());

                CoreProvider provider;

                if (source.CourseDirectoryUkPrns.Contains(ukprn))
                {
                    var courseDirectoryProvider = source.CourseDirectoryProviders.Providers.First(x => x.Ukprn == ukprn);
                    provider = _courseDirectoryProviderMapper.Map(courseDirectoryProvider);
                }
                else if (source.UkrlpProviders.Any(x => x.UnitedKingdomProviderReferenceNumber == ukprn.ToString()))
                {
                    provider = _ukrlpProviderMapper.Map(ukrlpProvider);
                }
                else
                {
                    // skip this provider if they don't exist in Course Directory or UKRLP
                    continue;
                }

                provider.Name = ukrlpProvider?.ProviderName;
                provider.Addresses = ukrlpProvider?.ProviderContact.Select(_ukrlpProviderMapper.MapAddress);
                provider.Aliases = ukrlpProvider?.ProviderAliases;

                provider.IsEmployerProvider = source.EmployerProviders.Providers.Contains(provider.Ukprn.ToString());
                provider.IsHigherEducationInstitute = source.HeiProviders.Providers.Contains(provider.Ukprn.ToString());

                _providerDataService.SetLearnerSatisfactionRate(source.LearnerSatisfactionRates, provider);
                _providerDataService.SetEmployerSatisfactionRate(source.EmployerSatisfactionRates, provider);

                yield return provider;
            }
        }

        private IEnumerable<CoreProvider> CreateApiProviders(ProviderSourceDto source)
        {
            var invalid = 0;
            var roatpProviderResults = source.RoatpProviders.ToList();
            _log.Debug("Mapping API providers from valid ROATP providers", new Dictionary<string, object> { { "TotalCount", roatpProviderResults.Count } });

            var missing = roatpProviderResults.Where(x => source.UkrlpProviders.All(y => y.UnitedKingdomProviderReferenceNumber != x.Ukprn)).ToList();
            if (missing.Any())
            {
                _log.Warn("Missing providers from UKRLP", new Dictionary<string, object> { { "TotalCount", missing.Count }, { "Body", JsonConvert.SerializeObject(missing.Select(x => x.Ukprn)) } });
            }

            foreach (var roatpProvider in roatpProviderResults)
            {
                var ukrlpProvider = source.UkrlpProviders.FirstOrDefault(x => x.UnitedKingdomProviderReferenceNumber == roatpProvider.Ukprn);

                CoreProvider provider;

                var roatProviderUkprn = int.Parse(roatpProvider.Ukprn);

                //if (source.CourseDirectoryUkPrns.Contains(roatProviderUkprn))
                //{
                //    var courseDirectoryProvider = source.CourseDirectoryProviders.Providers.First(x => x.Ukprn == roatProviderUkprn);
                //    provider = _courseDirectoryProviderMapper.Map(courseDirectoryProvider);
                //}
                //else 
                if (ukrlpProvider != null)
                {
                    provider = _ukrlpProviderMapper.Map(ukrlpProvider);
                    if (!string.IsNullOrEmpty(ukrlpProvider?.ProviderName))
                    {
                        provider.Name = ukrlpProvider.ProviderName;
                    }

                    provider.Addresses = ukrlpProvider?.ProviderContact.Select(_ukrlpProviderMapper.MapAddress);
                    provider.Aliases = ukrlpProvider?.ProviderAliases;
                }
                else
                {
                    // skip this provider if they don't exist in Course Directory or UKRLP
                    _log.Warn("Provider doesn't exist on Course Directory or UKRLP", new Dictionary<string, object> { { "UKPRN", roatProviderUkprn } });
                    continue;
                }


                //var byProvidersFiltered = source.AchievementRateProviders.Rates.Where(bp => bp.Ukprn == provider.Ukprn);

                provider.IsEmployerProvider = roatpProvider.ProviderType == ProviderType.EmployerProvider;

                provider.IsHigherEducationInstitute = source.HeiProviders.Providers.Contains(provider.Ukprn.ToString());

                //provider.Frameworks.ForEach(m => _providerDataService.UpdateFramework(m, source.Frameworks, byProvidersFiltered, source.AchievementRateNationals));
                //provider.Standards.ForEach(m => _providerDataService.UpdateStandard(m, source.Standards, byProvidersFiltered, source.AchievementRateNationals));

                _providerDataService.SetLearnerSatisfactionRate(source.LearnerSatisfactionRates, provider);
                _providerDataService.SetEmployerSatisfactionRate(source.EmployerSatisfactionRates, provider);

                if (!provider.IsValid())
                {
                    _log.Warn(
                        "API Provider is invalid",
                        new Dictionary<string, object> { { "Body", JsonConvert.SerializeObject(provider,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) }, { "UKPRN", provider.Ukprn } });
                    invalid++;
                }
                else
                {
                    yield return provider;
                }
            }

            if (invalid > 0)
            {
                _log.Warn("Invalid API Providers were found", new Dictionary<string, object> { { "TotalCount", invalid } });
            }
        }

        private IEnumerable<CoreProvider> CreateApprenticeshipProviders(ProviderSourceDto source)
        {
            var apprenticeshipProviders = CreateApprenticeshipProvidersFcs(source).ToList();
            apprenticeshipProviders.AddRange(CreateApprenticeshipProvidersRoatp(source).ToList());

            return apprenticeshipProviders;
        }

        private IEnumerable<CoreProvider> CreateApprenticeshipProvidersFcs(ProviderSourceDto source)
        {
            foreach (var courseDirectoryProvider in source.CourseDirectoryProviders.Providers)
            {
                CoreProvider provider;

                if (!source.ActiveProviders.Providers.Contains(courseDirectoryProvider.Ukprn))
                {
                    continue;
                }

                var ukrlpProvider = source.UkrlpProviders.FirstOrDefault(x => x.UnitedKingdomProviderReferenceNumber == courseDirectoryProvider.Ukprn.ToString());

                provider = _courseDirectoryProviderMapper.Map(courseDirectoryProvider);

                provider.LegalName = ukrlpProvider?.ProviderName;

                provider.IsHigherEducationInstitute = source.HeiProviders.Providers.Contains(provider.Ukprn.ToString());

                provider.HasNonLevyContract = true;
                provider.HasParentCompanyGuarantee = false;
                provider.IsNew = false;

                provider.IsLevyPayerOnly = false;

                var byProvidersFiltered = source.AchievementRateProviders.Rates.Where(bp => bp.Ukprn == provider.Ukprn);
                provider.Frameworks.ForEach(m => _providerDataService.UpdateFramework(m, source.Frameworks, byProvidersFiltered, source.AchievementRateNationals));
                provider.Standards.ForEach(m => _providerDataService.UpdateStandard(m, source.Standards, byProvidersFiltered, source.AchievementRateNationals));

                _providerDataService.SetLearnerSatisfactionRate(source.LearnerSatisfactionRates, provider);
                _providerDataService.SetEmployerSatisfactionRate(source.EmployerSatisfactionRates, provider);

                yield return provider;
            }
        }

        private IEnumerable<CoreProvider> CreateApprenticeshipProvidersRoatp(ProviderSourceDto source)
        {
            foreach (var courseDirectoryProvider in source.CourseDirectoryProviders.Providers)
            {
                CoreProvider provider;
                var roatpProvider = new RoatpProviderResult();

                var providerFromRoatp = false;

                var ukrlpProvider = source.UkrlpProviders.FirstOrDefault(x => x.UnitedKingdomProviderReferenceNumber == courseDirectoryProvider.Ukprn.ToString());

                foreach (var roatpProviderResult in source.RoatpProviders)
                {
                    if (roatpProviderResult.Ukprn == courseDirectoryProvider.Ukprn.ToString()
                        && roatpProviderResult.ProviderType == ProviderType.MainProvider
                        && roatpProviderResult.IsDateValid())
                    {
                        providerFromRoatp = true;
                        roatpProvider = roatpProviderResult;
                    }
                }

                if (!providerFromRoatp)
                {
                    continue;
                }

                provider = _courseDirectoryProviderMapper.Map(courseDirectoryProvider);

                provider.LegalName = ukrlpProvider?.ProviderName;
                provider.IsEmployerProvider = source.EmployerProviders.Providers.Contains(provider.Ukprn.ToString());

                provider.IsHigherEducationInstitute = source.HeiProviders.Providers.Contains(provider.Ukprn.ToString());

                provider.HasNonLevyContract = false; //roatpProvider.ContractedForNonLeviedEmployers;
                provider.HasParentCompanyGuarantee = roatpProvider.ParentCompanyGuarantee;
                provider.IsNew = roatpProvider.NewOrganisationWithoutFinancialTrackRecord;

                provider.IsLevyPayerOnly = !source.ActiveProviders.Providers.Contains(courseDirectoryProvider.Ukprn);

                var byProvidersFiltered = source.AchievementRateProviders.Rates.Where(bp => bp.Ukprn == provider.Ukprn);
                provider.Frameworks.ForEach(m => _providerDataService.UpdateFramework(m, source.Frameworks, byProvidersFiltered, source.AchievementRateNationals));
                provider.Standards.ForEach(m => _providerDataService.UpdateStandard(m, source.Standards, byProvidersFiltered, source.AchievementRateNationals));

                _providerDataService.SetLearnerSatisfactionRate(source.LearnerSatisfactionRates, provider);
                _providerDataService.SetEmployerSatisfactionRate(source.EmployerSatisfactionRates, provider);

                yield return provider;
            }
        }
    }
}