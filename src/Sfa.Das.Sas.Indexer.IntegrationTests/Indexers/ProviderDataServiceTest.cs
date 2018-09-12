namespace Sfa.Das.Sas.Indexer.IntegrationTests.Indexers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.ProviderFeedback;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

    [TestFixture]
    public class ProviderDataServiceTest
    {
        private ProviderDataService _sut;

        private Mock<IMediator> _mockMediator;

        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();

            _mockMediator.Setup(m => m.SendAsync(It.IsAny<RoatpProviderRequest>())).ReturnsAsync(new List<RoatpProviderResult>());
            _mockMediator.Setup(x => x.Send(It.IsAny<FrameworkMetaDataRequest>())).Returns(FrameworkResults());
            _mockMediator.Setup(x => x.Send(It.IsAny<StandardMetaDataRequest>())).Returns(StandardResults());
            _mockMediator.Setup(x => x.SendAsync(It.IsAny<FcsProviderRequest>())).ReturnsAsync(new FcsProviderResult { Providers = new List<int> { 123, 456 } });
            _mockMediator.Setup(x => x.SendAsync(It.IsAny<CourseDirectoryRequest>())).ReturnsAsync(new CourseDirectoryResult());

            _mockMediator.Setup(m => m.Send(It.IsAny<AchievementRateProviderRequest>())).Returns(GetAchievementData());
            _mockMediator.Setup(m => m.Send(It.IsAny<AchievementRateNationalRequest>())).Returns(GetNationalAchievementData());

            _mockMediator.Setup(m => m.Send(It.IsAny<LearnerSatisfactionRateRequest>())).Returns(GetLearnerSatisfactionRateData());
            _mockMediator.Setup(m => m.Send(It.IsAny<UkrlpProviderRequest>())).Returns(new UkrlpProviderResponse { MatchingProviderRecords = new List<Ukrlp.SoapApi.Types.Provider>() });
            _mockMediator.Setup(m => m.SendAsync(It.IsAny<ProviderFeedbackRequest>())).ReturnsAsync(new ProviderFeedbackResult(new List<EmployerFeedback>()));

            _sut = new ProviderDataService(_mockMediator.Object, Mock.Of<ILog>());
        }

        [Test]
        [Ignore("too slow")]
        public void ShouldRequestData()
        {
            // Act
            var result = _sut.LoadDatasetsAsync().Result;

            // Assert
            _mockMediator.VerifyAll();
        }

        private AchievementRateProviderResult GetAchievementData()
        {
            return new AchievementRateProviderResult
            {
                Rates = new List<AchievementRateProvider>
                {
                    new AchievementRateProvider { Ukprn = 456, ApprenticeshipLevel = "2", Ssa2Code = 22.2, OverallAchievementRate = 57.7, OverallCohort = "58" },
                    new AchievementRateProvider { Ukprn = 123, ApprenticeshipLevel = "3", Ssa2Code = 2.2, OverallAchievementRate = 67.7, OverallCohort = "68" },
                    new AchievementRateProvider { Ukprn = 123, ApprenticeshipLevel = "4", Ssa2Code = 43.2, OverallAchievementRate = 77.9, OverallCohort = "77" },
                }
            };
        }

        private AchievementRateNationalResult GetNationalAchievementData()
        {
            return new AchievementRateNationalResult
            {
                Rates = new List<AchievementRateNational>
                {
                    new AchievementRateNational { ApprenticeshipLevel = "2", Ssa2Code = 22.2, OverallAchievementRate = 88.8, HybridEndYear = "2041/2042" },
                    new AchievementRateNational { ApprenticeshipLevel = "2", Ssa2Code = 22.2, OverallAchievementRate = 99.9, HybridEndYear = "2042/2043" },

                    new AchievementRateNational { ApprenticeshipLevel = "3", Ssa2Code = 2.2, OverallAchievementRate = 66.6, HybridEndYear = "1994/1995" },
                    new AchievementRateNational { ApprenticeshipLevel = "3", Ssa2Code = 2.2, OverallAchievementRate = 77.7, HybridEndYear = "1995/1996" },

                    new AchievementRateNational { ApprenticeshipLevel = "4", Ssa2Code = 43.2, OverallAchievementRate = 66.6, HybridEndYear = "1994/1995" },
                    new AchievementRateNational { ApprenticeshipLevel = "4", Ssa2Code = 43.2, OverallAchievementRate = 77.8, HybridEndYear = "1995/1996" },
                    new AchievementRateNational { ApprenticeshipLevel = "4", Ssa2Code = 43.2, OverallAchievementRate = 88.7, HybridEndYear = "1993/1994" }
                }
            };
        }

        private LearnerSatisfactionRateResult GetLearnerSatisfactionRateData()
        {
            return new LearnerSatisfactionRateResult
            {
                Rates = new List<SatisfactionRateProvider>
                {
                    new SatisfactionRateProvider { Ukprn = 456, FinalScore = 67.1, TotalCount = 678, ResponseCount = 670 },
                    new SatisfactionRateProvider { Ukprn = 123, FinalScore = 0, TotalCount = 879, ResponseCount = 0 }
                }
            };
        }

        private StandardMetaDataResult StandardResults()
        {
            return new StandardMetaDataResult
            {
                Standards = new List<StandardMetaData>
                {
                    new StandardMetaData
                    {
                        Id = 5,
                        NotionalEndLevel = 2,
                        SectorSubjectAreaTier2 = 22.2
                    }
                }
            };
        }

        private FrameworkMetaDataResult FrameworkResults()
        {
            return new FrameworkMetaDataResult
            {
                Frameworks =

                    new List<FrameworkMetaData>
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
                    }
            };
        }
    }
}