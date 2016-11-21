﻿using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;

namespace Sfa.Das.Sas.Indexer.IntegrationTests.Indexers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Castle.Core.Internal;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;
    using Sfa.Das.Sas.Indexer.Core.Services;

    [TestFixture]
    public class ProviderDataServiceTest
    {
        private ProviderDataService _sut;

        private Mock<IGetApprenticeshipProviders> _mockProviderRepository;

        private Mock<IGetActiveProviders> _mockActiveProviderRepository;

        private Mock<IMetaDataHelper> _mockMetaDataHelper;

        private Mock<IAchievementRatesProvider> _achievementProvider;

        private Mock<ISatisfactionRatesProvider> _satisfactionProvider;

        [SetUp]
        public void SetUp()
        {
            _mockProviderRepository = new Mock<IGetApprenticeshipProviders>();
            _mockActiveProviderRepository = new Mock<IGetActiveProviders>();
            _mockMetaDataHelper = new Mock<IMetaDataHelper>();
            _achievementProvider = new Mock<IAchievementRatesProvider>();
            _satisfactionProvider = new Mock<ISatisfactionRatesProvider>();

            _mockMetaDataHelper.Setup(x => x.GetAllFrameworkMetaData()).Returns(FrameworkResults());
            _mockMetaDataHelper.Setup(x => x.GetAllStandardsMetaData()).Returns(StandardResults());

            _achievementProvider.Setup(m => m.GetAllByProvider()).Returns(GetAchievementData());
            _achievementProvider.Setup(m => m.GetAllNational()).Returns(GetNationalAchievementData());

            _satisfactionProvider.Setup(m => m.GetAllLearnerSatisfactionByProvider()).Returns(GetLearnerSatisfactionRateData());

            _sut = new ProviderDataService( Mock.Of<ILog>());
        }

        //[Test]
        //public void ShouldFilterProvidersIfTheFeatureIsEnabled()
        //{
        //    _mockFeatures.Setup(x => x.FilterInactiveProviders).Returns(true);
        //    _mockActiveProviderRepository.Setup(x => x.GetActiveProviders()).Returns(new[] { 123 });
        //    _mockProviderRepository.Setup(x => x.GetEmployerProviders()).Returns(GetEmployerProviders);
        //    _mockProviderRepository.Setup(x => x.GetApprenticeshipProvidersAsync()).Returns(TwoProvidersTask());
        //    _mockProviderRepository.Setup(x => x.GetHeiProviders()).Returns(HeiProviders);

        //    var result = _sut.GetProviders().Result;

        //    Assert.AreEqual(2, result.Count);

        //    _mockActiveProviderRepository.VerifyAll();
        //    _mockProviderRepository.VerifyAll();
        //}

        //[Test]
        //public void ShouldntFilterProvidersIfTheFeatureIsDisabled()
        //{
        //    _mockFeatures.Setup(x => x.FilterInactiveProviders).Returns(false);
        //    _mockProviderRepository.Setup(x => x.GetEmployerProviders()).Returns(GetEmployerProviders);
        //    _mockProviderRepository.Setup(x => x.GetApprenticeshipProvidersAsync()).Returns(TwoProvidersTask());
        //    _mockProviderRepository.Setup(x => x.GetHeiProviders()).Returns(HeiProviders);

        //    var result = _sut.GetProviders().Result;

        //    Assert.AreEqual(3, result.Count);

        //    _mockProviderRepository.VerifyAll();
        //}

        //[Test]
        //public void ShouldUpdateFrameworkInformation()
        //{
        //    _mockFeatures.Setup(x => x.FilterInactiveProviders).Returns(false);
        //    _mockProviderRepository.Setup(x => x.GetEmployerProviders()).Returns(GetEmployerProviders);
        //    _mockProviderRepository.Setup(x => x.GetApprenticeshipProvidersAsync()).Returns(TwoProvidersTask());
        //    _mockProviderRepository.Setup(x => x.GetHeiProviders()).Returns(HeiProviders);

        //    var result = _sut.GetProviders().Result;

        //    Assert.AreEqual(3, result.Count);
        //    var framework = result.FirstOrDefault()?.Frameworks.FirstOrDefault();
        //    var frameworkSecond = result.ElementAt(1)?.Frameworks.ElementAt(0);
        //    framework?.OverallCohort.Should().Be("68");
        //    framework?.OverallAchievementRate.Should().Be(68);
        //    framework?.NationalOverallAchievementRate.Should().Be(78);

        //    frameworkSecond?.OverallCohort.Should().BeNull();
        //    frameworkSecond?.OverallAchievementRate.Should().Be(null);
        //    frameworkSecond?.NationalOverallAchievementRate.Should().Be(null);
        //}

        //[Test]
        //public void ShouldUpdateStandardInformation()
        //{
        //    _mockFeatures.Setup(x => x.FilterInactiveProviders).Returns(false);
        //    _mockProviderRepository.Setup(x => x.GetEmployerProviders()).Returns(GetEmployerProviders);
        //    _mockProviderRepository.Setup(x => x.GetApprenticeshipProvidersAsync()).Returns(TwoProvidersTask());
        //    _mockProviderRepository.Setup(x => x.GetHeiProviders()).Returns(HeiProviders);

        //    var result = _sut.GetProviders().Result;

        //    Assert.AreEqual(3, result.Count);
        //    var standard = result.SingleOrDefault(m => !m.Standards.IsNullOrEmpty())?.Standards.FirstOrDefault();
        //    standard?.OverallCohort.Should().Be("58");
        //    standard?.OverallAchievementRate.Should().Be(58);
        //    standard?.NationalOverallAchievementRate.Should().Be(100);
        //}

        //[Test]
        //public void ShouldUpdateLearnerSatisfactionRateIfAvailable()
        //{
        //    _mockFeatures.Setup(x => x.FilterInactiveProviders).Returns(false);
        //    _mockProviderRepository.Setup(x => x.GetEmployerProviders()).Returns(GetEmployerProviders);
        //    _mockProviderRepository.Setup(x => x.GetApprenticeshipProvidersAsync()).Returns(TwoProvidersTask());
        //    _mockProviderRepository.Setup(x => x.GetHeiProviders()).Returns(HeiProviders);

        //    var result = _sut.GetProviders().Result;

        //    Assert.AreEqual(3, result.Count);

        //    result.Count(ls => ls.Ukprn == 456 && ls.LearnerSatisfaction == 67).Should().Be(1);
        //    result.Count(ls => ls.Ukprn == 123 && ls.LearnerSatisfaction == null).Should().Be(2);
        //}

        //[Test]
        //public void ShouldLeaveLearnerSatisfactionRateNullIfUnavailable()
        //{
        //    _mockFeatures.Setup(x => x.FilterInactiveProviders).Returns(false);
        //    _mockProviderRepository.Setup(x => x.GetEmployerProviders()).Returns(GetEmployerProviders);
        //    _mockProviderRepository.Setup(x => x.GetApprenticeshipProvidersAsync()).Returns(ThreeProvidersTask());
        //    _mockProviderRepository.Setup(x => x.GetHeiProviders()).Returns(HeiProviders);

        //    var result = _sut.GetProviders().Result;

        //    Assert.AreEqual(4, result.Count);
            
        //    result.Any(ls => ls.Ukprn == 789 && ls.LearnerSatisfaction == null).Should().Be(true);
        //}

        private IEnumerable<AchievementRateProvider> GetAchievementData()
        {
            return new List<AchievementRateProvider>
            {
                new AchievementRateProvider { Ukprn = 456, ApprenticeshipLevel = "2", Ssa2Code = 22.2, OverallAchievementRate = 57.7, OverallCohort = "58" },
                new AchievementRateProvider { Ukprn = 123, ApprenticeshipLevel = "3", Ssa2Code = 2.2, OverallAchievementRate = 67.7, OverallCohort = "68" },
                new AchievementRateProvider { Ukprn = 123, ApprenticeshipLevel = "4", Ssa2Code = 43.2, OverallAchievementRate = 77.9, OverallCohort = "77" },
            };
        }

        private IEnumerable<AchievementRateNational> GetNationalAchievementData()
        {
            return new List<AchievementRateNational>
            {
                new AchievementRateNational { ApprenticeshipLevel = "2", Ssa2Code = 22.2, OverallAchievementRate = 88.8, HybridEndYear = "2041/2042" },
                new AchievementRateNational { ApprenticeshipLevel = "2", Ssa2Code = 22.2, OverallAchievementRate = 99.9, HybridEndYear = "2042/2043" },

                new AchievementRateNational { ApprenticeshipLevel = "3", Ssa2Code = 2.2, OverallAchievementRate = 66.6, HybridEndYear = "1994/1995" },
                new AchievementRateNational { ApprenticeshipLevel = "3", Ssa2Code = 2.2, OverallAchievementRate = 77.7, HybridEndYear = "1995/1996" },

                new AchievementRateNational { ApprenticeshipLevel = "4", Ssa2Code = 43.2, OverallAchievementRate = 66.6, HybridEndYear = "1994/1995" },
                new AchievementRateNational { ApprenticeshipLevel = "4", Ssa2Code = 43.2, OverallAchievementRate = 77.8, HybridEndYear = "1995/1996" },
                new AchievementRateNational { ApprenticeshipLevel = "4", Ssa2Code = 43.2, OverallAchievementRate = 88.7, HybridEndYear = "1993/1994" }
            };
        }

        private IEnumerable<SatisfactionRateProvider> GetLearnerSatisfactionRateData()
        {
            return new List<SatisfactionRateProvider>
            {
                new SatisfactionRateProvider {Ukprn = 456, FinalScore = 67.1, TotalCount = 678, ResponseCount = 670},
                new SatisfactionRateProvider {Ukprn = 123, FinalScore = 0, TotalCount = 879, ResponseCount = 0}
            };
        }

        private ICollection<string> GetEmployerProviders()
        {
            return new List<string> { "123" };
        }

        private ICollection<string> HeiProviders()
        {
            return new List<string> { "123" };
        }

        private Task<IEnumerable<Provider>> TwoProvidersTask()
        {
            return Task.FromResult(TwoProviders());
        }

        private Task<IEnumerable<Provider>> ThreeProvidersTask()
        {
            var providers = TwoProviders().Concat(new List<Provider>{new Provider
            {
                Ukprn = 789,
                Frameworks = new List<FrameworkInformation>
                {
                    new FrameworkInformation { Code = 43, PathwayCode = 5, ProgType = 20 }
                }
            }
            });

            return Task.FromResult(providers);
        }

        private List<StandardMetaData> StandardResults()
        {
            return new List<StandardMetaData>
                       {
                           new StandardMetaData
                           {
                               Id = 5,
                               NotionalEndLevel = 2,
                               SectorSubjectAreaTier2 = 22.2
                           }
                       };
        }

        private List<FrameworkMetaData> FrameworkResults()
        {
            return new List<FrameworkMetaData>
                       {
                           new FrameworkMetaData
                           {
                               FworkCode = 42,
                               PwayCode = 5,
                               ProgType = 2,
                               SectorSubjectAreaTier2 = 2.2
                           },
                           new FrameworkMetaData
                           {
                               FworkCode = 43,
                               PwayCode = 5,
                               ProgType = 20,
                               SectorSubjectAreaTier2 = 43.2
                           }
                       };
        }

        private IEnumerable<Provider> TwoProviders()
        {
            yield return new Provider()
            {
                Ukprn = 123,
                Frameworks = new List<FrameworkInformation>
                {
                    new FrameworkInformation { Code = 42, PathwayCode = 5, ProgType = 2 }
                }
            };
            yield return new Provider // Level 4+
            {
                Ukprn = 123,
                Frameworks = new List<FrameworkInformation>
                {
                    new FrameworkInformation { Code = 43, PathwayCode = 5, ProgType = 20 }
                }
            };
            yield return new Provider
            {
                Ukprn = 456,
                Standards = new List<StandardInformation>
                {
                    new StandardInformation { Code = 5 }
                }
            };
        }
    }
}