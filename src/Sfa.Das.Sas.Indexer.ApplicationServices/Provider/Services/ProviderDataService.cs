using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureToggle.Core.Fluent;
using MediatR;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.FeatureToggles;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.ProviderFeedback;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Utility;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public class ProviderDataService : IProviderDataService
    {
        private readonly ILog _logger;
        private readonly IMediator _mediator;

        public ProviderDataService(
            IMediator mediator,
            ILog logger)
        {
            _logger = logger;
            _mediator = mediator;
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

        public void SetProviderFeedback(ProviderFeedbackResult providerFeedbackResult, Core.Models.Provider.Provider provider)
        {
            try
            {
                var feedbackToSet = new Feedback();
                var resultsForProvider = providerFeedbackResult.EmployerFeedback.Where(feedback => feedback.Ukprn == provider.Ukprn);
                if (resultsForProvider.Any())
                {
                    SetProviderRatings(feedbackToSet, resultsForProvider);
                    SetProviderStrengthsAndWeaknesses(feedbackToSet, resultsForProvider);

                    provider.ProviderFeedback = feedbackToSet;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unable to set provider feedback for UKPRN: {provider.Ukprn}");
            }
        }

        private static void SetProviderStrengthsAndWeaknesses(Feedback providerFeedback, IEnumerable<EmployerFeedbackSourceDto> feedbackForProvider)
        {
            var distinctAttributeList = feedbackForProvider.SelectMany(fp => fp.ProviderAttributes).GroupBy(pa => pa.Name).Select(group => group.Key);
            foreach (var providerAttributeName in distinctAttributeList)
            {
                var strengthAttribute = new ProviderAttribute { Name = providerAttributeName };
                var weaknessAttribute = new ProviderAttribute { Name = providerAttributeName };
                var matchingAttributeFeedback = feedbackForProvider
                    .Select(a => a.ProviderAttributes.SingleOrDefault(p => p.Name == providerAttributeName))
                    .Where(pf => pf != default(ProviderAttributeSourceDto));

                strengthAttribute.Count = matchingAttributeFeedback.Count(x => x.Value > 0);
                if (strengthAttribute.Count > 0)
                {
                    providerFeedback.Strengths.Add(strengthAttribute);
                }

                weaknessAttribute.Count = matchingAttributeFeedback.Count(x => x.Value < 0);
                if (weaknessAttribute.Count > 0)
                {
                    providerFeedback.Weaknesses.Add(weaknessAttribute);
                }
            }
        }

        private static void SetProviderRatings(Feedback providerFeedback, IEnumerable<EmployerFeedbackSourceDto> feedbackForProvider)
        {
            providerFeedback.ExcellentFeedbackCount = feedbackForProvider.Where(f => f.ProviderRating == ProviderRatings.Excellent).Count();
            providerFeedback.GoodFeedbackCount = feedbackForProvider.Where(f => f.ProviderRating == ProviderRatings.Good).Count();
            providerFeedback.PoorFeedbackCount = feedbackForProvider.Where(f => f.ProviderRating == ProviderRatings.Poor).Count();
            providerFeedback.VeryPoorFeedbackCount = feedbackForProvider.Where(f => f.ProviderRating == ProviderRatings.VeryPoor).Count();
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

                si.RegulatedStandard = metaData.RegulatedStandard;
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
            var roatpProviders = await _mediator.SendAsync(new RoatpProviderRequest());
            var courseDirectoryProviders = await _mediator.SendAsync(new CourseDirectoryRequest());
            var activeProviders = await _mediator.SendAsync(new FcsProviderRequest());

            _logger.Debug($"Finished loading course directory and active providers");

            var frameworks = Task.Run(() => _mediator.Send(new FrameworkMetaDataRequest()));
            var standards = Task.Run(() => _mediator.Send(new StandardMetaDataRequest()));

            await Task.WhenAll(frameworks, standards);

            _logger.Debug($"Finished loading frameworks, standards");

            var ukprnList = JoinUkprnLists(roatpProviders, activeProviders);

            return new ProviderSourceDto
            {
                CourseDirectoryProviders = courseDirectoryProviders,
                ActiveProviders = activeProviders,
                RoatpProviders = roatpProviders,
                UkrlpProviders = _mediator.Send(new UkrlpProviderRequest(ukprnList)),
                Frameworks = frameworks.Result,
                Standards = standards.Result,
                AchievementRateProviders = _mediator.Send(new AchievementRateProviderRequest()),
                AchievementRateNationals = _mediator.Send(new AchievementRateNationalRequest()),
                LearnerSatisfactionRates = _mediator.Send(new LearnerSatisfactionRateRequest()),
                EmployerSatisfactionRates = _mediator.Send(new EmployerSatisfactionRateRequest()),
                EmployerProviders = _mediator.Send(new EmployerProviderRequest()),
                HeiProviders = await _mediator.SendAsync(new HeiProvidersRequest()),
                ProviderFeedback = Is<ProviderFeedbackFeature>.Enabled ? await _mediator.SendAsync(new ProviderFeedbackRequest()) : new ProviderFeedbackResult(new List<EmployerFeedbackSourceDto>())
            };
        }

        private IEnumerable<int> JoinUkprnLists(IEnumerable<RoatpProviderResult> roatpProviders, FcsProviderResult activeProviders)
        {
            var ukprnList = new List<int>();
            foreach (var roatpProviderResult in roatpProviders)
            {
                var ukprn = int.Parse(roatpProviderResult.Ukprn);
                if (!ukprnList.Contains(ukprn))
                {
                    ukprnList.Add(ukprn);
                }
            }

            foreach (var activeProvider in activeProviders.Providers)
            {
                if (!ukprnList.Contains(activeProvider))
                {
                    ukprnList.Add(activeProvider);
                }
            }

            return ukprnList;
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
                _logger.Warn($"Multiple achievement rates found - UKPRN: {achievementRate.FirstOrDefault()?.Ukprn}");
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
