using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Newtonsoft.Json;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Extensions;
using Sfa.Das.Sas.Indexer.Core.Logging.Metrics;
using Sfa.Das.Sas.Indexer.Core.Logging.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
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
        private readonly ILog _log;
        private readonly IProviderExclusionService _providerExclusionService;

        public ProviderIndexer(
            IIndexSettings<IMaintainProviderIndex> settings,
            ICourseDirectoryProviderMapper courseDirectoryProviderMapper,
            IUkrlpProviderMapper ukrlpProviderMapper,
            IMaintainProviderIndex searchIndexMaintainer,
            IProviderDataService providerDataService,
            ILog log, 
            IProviderExclusionService providerExclusionService)
        {
            _settings = settings;
            _providerDataService = providerDataService;
            _courseDirectoryProviderMapper = courseDirectoryProviderMapper;
            _ukrlpProviderMapper = ukrlpProviderMapper;
            _searchIndexMaintainer = searchIndexMaintainer;
            _log = log;
            _providerExclusionService = providerExclusionService;
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

        public async Task IndexEntries(string indexName)
        {
            var bulkStandardTasks = new List<Task<IBulkResponse>>();
            var bulkFrameworkTasks = new List<Task<IBulkResponse>>();
            var bulkApiProviderTasks = new List<Task<IBulkResponse>>();

            // Load data
            var timing = ExecutionTimer.GetTiming(() => _providerDataService.LoadDatasetsAsync());

            var source = await timing.Result;
            _log.Debug("Loaded data for provider index", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            // Providers
            var providersApi = CreateApiProviders(source).ToList();

            _log.Debug("Indexing " + providersApi.Count + " API providers", new Dictionary<string, object> { { "TotalCount", providersApi.Count } });
            bulkApiProviderTasks.AddRange(_searchIndexMaintainer.IndexApiProviders(indexName, providersApi));
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkApiProviderTasks), "ProviderApiDocument");

            // Provider Sites
            var apprenticeshipProviders = CreateApprenticeshipProviders(source).ToList();

            _log.Debug("Indexing " + apprenticeshipProviders.Count + " provider sites", new Dictionary<string, object> { { "TotalCount", apprenticeshipProviders.Count } });

            try
            {
                bulkStandardTasks.AddRange(_searchIndexMaintainer.IndexStandards(indexName, apprenticeshipProviders));
                bulkFrameworkTasks.AddRange(_searchIndexMaintainer.IndexFrameworks(indexName, apprenticeshipProviders));
            }
            catch (Exception e)
            {
                _log.Error(e, "Something failed indexing apprenticeship providers");
            }

            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkStandardTasks), "StandardProvider");
            _searchIndexMaintainer.LogResponse(await Task.WhenAll(bulkFrameworkTasks), "FrameworkProvider");
        }

        private IEnumerable<CoreProvider> CreateApiProviders(ProviderSourceDto source)
        {
            var invalid = 0;
            var roatpProviderResults = source.RoatpProviders.ToList();
            _log.Debug("Mapping API providers from valid ROATP providers", new Dictionary<string, object> { { "TotalCount", roatpProviderResults.Count } });

            var missing = roatpProviderResults.Where(x => source.UkrlpProviders.MatchingProviderRecords.All(y => y.UnitedKingdomProviderReferenceNumber != x.Ukprn)).ToList();
            if (missing.Any())
            {
                _log.Warn("Missing providers from UKRLP", new Dictionary<string, object> { { "TotalCount", missing.Count }, { "Body", JsonConvert.SerializeObject(missing.Select(x => x.Ukprn)) } });
            }

            foreach (var roatpProvider in roatpProviderResults)
            {
                var ukrlpProvider = source.UkrlpProviders.MatchingProviderRecords.FirstOrDefault(x => x.UnitedKingdomProviderReferenceNumber == roatpProvider.Ukprn);
                var courseDirectory = source.CourseDirectoryProviders.Providers.FirstOrDefault(x => x.Ukprn.ToString() == roatpProvider.Ukprn);

                CoreProvider provider;

                var roatProviderUkprn = int.Parse(roatpProvider.Ukprn);

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
                    _log.Warn("Provider doesn't exist on UKRLP", new Dictionary<string, object> { { "UKPRN", roatProviderUkprn } });
                    continue;
                }

                if (courseDirectory != null)
                {
                    provider.ContactDetails.Email = provider.ContactDetails.Email ?? courseDirectory.Email;
                    provider.ContactDetails.Website = provider.ContactDetails.Website ?? courseDirectory.Website;
                    provider.ContactDetails.Phone = provider.ContactDetails.Phone ?? courseDirectory.Phone;
                    provider.MarketingInfo = courseDirectory.MarketingInfo;
                    provider.IsLevyPayerOnly = !source.ActiveProviders.Providers.Contains(courseDirectory.Ukprn);
            }

                provider.IsEmployerProvider = roatpProvider.ProviderType == ProviderType.EmployerProvider;
                provider.IsHigherEducationInstitute = source.HeiProviders.Providers.Contains(provider.Ukprn.ToString());

                provider.HasParentCompanyGuarantee = roatpProvider.ParentCompanyGuarantee;
                provider.IsNew = roatpProvider.NewOrganisationWithoutFinancialTrackRecord;

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
                if (_providerExclusionService.IsProviderInExclusionList(courseDirectoryProvider.Ukprn)) { continue; }

                CoreProvider provider;

                var ukrlpProvider = source.UkrlpProviders.MatchingProviderRecords.FirstOrDefault(x => x.UnitedKingdomProviderReferenceNumber == courseDirectoryProvider.Ukprn.ToString());

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
                if (_providerExclusionService.IsProviderInExclusionList(courseDirectoryProvider.Ukprn)) { continue; }

                CoreProvider provider;
                var roatpProvider = new RoatpProviderResult();

                var providerFromRoatp = false;

                var ukrlpProvider = source.UkrlpProviders.MatchingProviderRecords.FirstOrDefault(x => x.UnitedKingdomProviderReferenceNumber == courseDirectoryProvider.Ukprn.ToString());

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