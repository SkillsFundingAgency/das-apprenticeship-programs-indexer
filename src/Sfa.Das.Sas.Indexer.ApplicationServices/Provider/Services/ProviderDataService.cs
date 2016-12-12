using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public class ProviderDataService : IProviderDataService
    {
        private readonly ILog _logger;
        private readonly IGetApprenticeshipProviders _providerRepository;
        private readonly IGetCourseDirectoryProviders _courseDirectoryClient;
        private readonly IUkrlpService _ukrlpService;
        private readonly IMetaDataHelper _metaDataHelper;
        private readonly IAchievementRatesProvider _achievementRatesProvider;
        private readonly ISatisfactionRatesProvider _satisfactionRatesProvider;
        private readonly IGetActiveProviders _activeProviderClient;

        public ProviderDataService(
            IGetApprenticeshipProviders providerRepository,
            IGetCourseDirectoryProviders courseDirectoryClient,
            IUkrlpService ukrlpService,
            IMetaDataHelper metaDataHelper,
            IAchievementRatesProvider achievementRatesProvider,
            ISatisfactionRatesProvider satisfactionRatesProvider,
            IGetActiveProviders activeProviderClient,
            ILog logger)
        {
            _logger = logger;
            _providerRepository = providerRepository;
            _courseDirectoryClient = courseDirectoryClient;
            _ukrlpService = ukrlpService;
            _metaDataHelper = metaDataHelper;
            _achievementRatesProvider = achievementRatesProvider;
            _satisfactionRatesProvider = satisfactionRatesProvider;
            _activeProviderClient = activeProviderClient;
        }

        public void SetLearnerSatisfactionRate(LearnerSatisfactionRateResult satisfactionRates, Core.Models.Provider.Provider provider)
        {
            var learnerSatisfaction = satisfactionRates.Rates.SingleOrDefault(sr => sr.Ukprn == provider.Ukprn);

            provider.LearnerSatisfaction = learnerSatisfaction?.FinalScore != null && learnerSatisfaction.FinalScore > 0
                ? (double?)Math.Round(learnerSatisfaction?.FinalScore ?? 0.0)
                : null;
        }

        public void SetEmployerSatisfactionRate(EmployerSatisfactionRateResult satisfactionRates, Core.Models.Provider.Provider provider)
        {
            var employerSatisfaction = satisfactionRates.Rates.SingleOrDefault(sr => sr.Ukprn == provider.Ukprn);

            provider.EmployerSatisfaction = employerSatisfaction?.FinalScore != null && employerSatisfaction.FinalScore > 0
                ? (double?)Math.Round(employerSatisfaction?.FinalScore ?? 0.0)
                : null;
        }

        public void UpdateStandard(StandardInformation si, StandardMetaDataResult standards, IEnumerable<AchievementRateProvider> achievementRates, AchievementRateNationalResult nationalAchievementRates)
        {
            var metaData = standards.Standards.FirstOrDefault(m => m.Id == si.Code);

            if (metaData != null)
            {
                var achievementRate = achievementRates.Where(m =>
                    IsEqual(m.Ssa2Code, metaData.SectorSubjectAreaTier2))
                    .Where(m => TestLevel(m.ApprenticeshipLevel, metaData.NotionalEndLevel))
                    .ToList();

                var nationalAchievementRate = nationalAchievementRates.Rates.Where(m =>
                    IsEqual(m.Ssa2Code, metaData.SectorSubjectAreaTier2))
                    .Where(m => TestLevel(m.ApprenticeshipLevel, metaData.NotionalEndLevel))
                    .ToList();

                var rate = ExtractValues(achievementRate);
                si.OverallAchievementRate = rate.Item1;
                si.OverallCohort = rate.Item2;

                si.NationalOverallAchievementRate =
                    GetNationalOverallAchievementRate(nationalAchievementRate);
            }
        }

        public void UpdateFramework(FrameworkInformation fi, FrameworkMetaDataResult frameworks, IEnumerable<AchievementRateProvider> achievementRates, AchievementRateNationalResult nationalAchievementRates)
        {
            var metaData = frameworks.Frameworks.FirstOrDefault(m =>
                m.FworkCode == fi.Code &&
                m.PwayCode == fi.PathwayCode &&
                m.ProgType == fi.ProgType);

            if (metaData != null)
            {
                var achievementRate = achievementRates.Where(m =>
                    IsEqual(m.Ssa2Code, metaData.SectorSubjectAreaTier2))
                    .Where(m => TestLevel(m.ApprenticeshipLevel, ApprenticeshipLevelMapper.MapToLevel(metaData.ProgType)))
                    .ToList();

                var nationalAchievementRate = nationalAchievementRates.Rates.Where(m =>
                    IsEqual(m.Ssa2Code, metaData.SectorSubjectAreaTier2))
                    .Where(m => TestLevel(m.ApprenticeshipLevel, ApprenticeshipLevelMapper.MapToLevel(metaData.ProgType)))
                    .ToList();

                var rate = ExtractValues(achievementRate);

                fi.OverallAchievementRate = rate.Item1;

                fi.OverallCohort = rate.Item2;

                fi.NationalOverallAchievementRate =
                    GetNationalOverallAchievementRate(nationalAchievementRate);
            }
        }

        public async Task<ProviderSourceDto> LoadDatasetsAsync()
        {
            var courseDirectoryProviders = await _courseDirectoryClient.GetApprenticeshipProvidersAsync();
            var activeProviders = await _activeProviderClient.GetActiveProviders();

            _logger.Debug($"Finished loading course directory and active providers");

            // TODO replace this with elastic search
            var frameworks = Task.Run(() => _metaDataHelper.GetAllFrameworkMetaData());
            var standards = Task.Run(() => _metaDataHelper.GetAllStandardsMetaData());

            await Task.WhenAll(frameworks, standards);

            _logger.Debug($"Finished loading frameworks, standards");

            var ukrlpProviders = _ukrlpService.GetProviders(activeProviders);

            return new ProviderSourceDto
            {
                CourseDirectoryProviders = courseDirectoryProviders,
                ActiveProviders = activeProviders,
                UkrlpProviders = ukrlpProviders,
                Frameworks = frameworks.Result,
                Standards = standards.Result,
                EmployerProviders = _providerRepository.GetEmployerProviders(),
                AchievementRateProviders = _achievementRatesProvider.GetAllByProvider(),
                AchievementRateNationals = _achievementRatesProvider.GetAllNational(),
                LearnerSatisfactionRates = _satisfactionRatesProvider.GetAllLearnerSatisfactionByProvider(),
                EmployerSatisfactionRates = _satisfactionRatesProvider.GetAllEmployerSatisfactionByProvider(),
                HeiProviders = _providerRepository.GetHeiProviders()
            };
        }

        private static double? GetNationalOverallAchievementRate(List<AchievementRateNational> nationalAchievementRate)
        {
            var nationalOverallAchievementRate = nationalAchievementRate
                .OrderByDescending(m => m.HybridEndYear)
                .FirstOrDefault()?
                .OverallAchievementRate;

            if (nationalOverallAchievementRate != null)
            {
                return Math.Round((double)nationalOverallAchievementRate);
            }

            return null;
        }

        private static bool IsLevelFourOrHigher(string achievementRateProviderLevel, int level)
        {
            return achievementRateProviderLevel == "4+" && level > 3;
        }

        private static bool IsLevelTwoOrThree(string achievementRateProviderLevel, int level)
        {
            return (achievementRateProviderLevel == "2" || achievementRateProviderLevel == "3") && achievementRateProviderLevel == level.ToString();
        }

        private Tuple<double?, string> ExtractValues(List<AchievementRateProvider> achievementRate)
        {
            if (achievementRate.Count > 1)
            {
                _logger.Warn($"Multiple achievement rates found - UPPRN: {achievementRate.FirstOrDefault()?.Ukprn}");
            }

            var v1 = achievementRate.FirstOrDefault()?.OverallAchievementRate != null
                             ? (double?)Math.Round(achievementRate.FirstOrDefault()?.OverallAchievementRate ?? 0.0)
                             : null;

            var v2 = achievementRate.FirstOrDefault()?.OverallCohort;
            return new Tuple<double?, string>(v1, v2);
        }

        private bool IsEqual(double d1, double d2)
        {
            return Math.Abs(d1 - d2) < 0.01;
        }

        private bool TestLevel(string achievementRateProviderLevel, int level)
        {
            return IsLevelTwoOrThree(achievementRateProviderLevel, level) || IsLevelFourOrHigher(achievementRateProviderLevel, level);
        }
    }
}
