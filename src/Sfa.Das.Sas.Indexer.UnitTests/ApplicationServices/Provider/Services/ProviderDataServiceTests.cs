using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.ProviderFeedback;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;
using CoreProvider = Sfa.Das.Sas.Indexer.Core.Models.Provider.Provider;
using System;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.UnitTests.ApplicationServices.Provider.Services
{
    [TestFixture]
    public class ProviderDataServiceTests
    {
        private const int Ukprn = 88888888;
        private IFixture _fixture = new Fixture();
        private ProviderDataService _sut;
        private Mock<IMediator> _mediatorMock;
        private Mock<ILog> _loggerMock;
        private List<ProviderAttributeSourceDto> _provAttributes;
        private List<EmployerFeedbackSourceDto> _feedbackEntries;

        [SetUp]
        public void SetUp()
        {
            _provAttributes = _fixture
                .Build<ProviderAttributeSourceDto>()
                .With(x => x.Value, 0)
                .CreateMany(9).ToList();

            _feedbackEntries = _fixture
                .Build<EmployerFeedbackSourceDto>()
                .With(x => x.Ukprn, Ukprn)
                .With(x => x.ProviderAttributes, _provAttributes)
                .CreateMany(10)
                .ToList();

            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILog>();
            _sut = new ProviderDataService(_mediatorMock.Object, _loggerMock.Object);
        }

        [Test]
        public void NoFeedbackResults_FeedbackNotSet_OnProvider()
        {
            // Arrange
            var feedbackResult = new ProviderFeedbackResult(new List<EmployerFeedbackSourceDto>());
            var provider = new CoreProvider();

            // Act
            _sut.SetProviderFeedback(feedbackResult, provider);

            // Assert
            Assert.IsNull(provider.ProviderFeedback);
        }

        [Test]
        public void FeedbackResults_FeedbackSet_WithStrengths_OnProvider()
        {
            // Arrange
            SetProviderAttributesStrengths(3);
            var feedbackResult = new ProviderFeedbackResult(_feedbackEntries);
            var provider = new CoreProvider { Ukprn = Ukprn };

            // Act
            _sut.SetProviderFeedback(feedbackResult, provider);

            // Assert
            Assert.IsNotNull(provider.ProviderFeedback);
            Assert.IsNotEmpty(provider.ProviderFeedback.Strengths);
            Assert.AreEqual(3, provider.ProviderFeedback.Strengths.Count);
        }

        [Test]
        public void FeedbackResults_FeedbackSet_WithWeaknesses_OnProvider()
        {
            // Arrange
            SetProviderAttributesWeaknesses(3);
            var feedbackResult = new ProviderFeedbackResult(_feedbackEntries);
            var provider = new CoreProvider { Ukprn = Ukprn };

            // Act
            _sut.SetProviderFeedback(feedbackResult, provider);

            // Assert
            Assert.IsNotNull(provider.ProviderFeedback);
            Assert.IsNotEmpty(provider.ProviderFeedback.Weaknesses);
            Assert.AreEqual(3, provider.ProviderFeedback.Weaknesses.Count);
        }

        [Test]
        [TestCase(ProviderRatings.Excellent, 3)]
        [TestCase(ProviderRatings.Good, 1)]
        [TestCase(ProviderRatings.Poor, 4)]
        [TestCase(ProviderRatings.Poor, 9)]
        public void FeedbackResults_FeedbackSet_ShouldCalcRatings(string providerRating, int amount)
        {
            // Arrange
            SetRatings(providerRating, amount);
            var feedbackResult = new ProviderFeedbackResult(_feedbackEntries);
            var provider = new CoreProvider { Ukprn = Ukprn };

            // Act
            _sut.SetProviderFeedback(feedbackResult, provider);
            int ratingCountToCheck = GetRatingCount(providerRating, provider);

            // Assert
            Assert.AreEqual(amount, ratingCountToCheck);
        }

        [Test]
        public void FeedbackResults_FeedbackSet_Attributes_ShouldContain_DistinctList()
        {
            // Arrange
            SetProviderAttributesStrengths(9);
            var feedbackDiffAttributes = new EmployerFeedbackSourceDto { ProviderAttributes = new List<ProviderAttributeSourceDto> { new ProviderAttributeSourceDto { Name = "NewPa", Value = 1 } }, Ukprn = Ukprn };
            _feedbackEntries.Add(feedbackDiffAttributes);
            var feedbackResult = new ProviderFeedbackResult(_feedbackEntries);
            var provider = new CoreProvider { Ukprn = Ukprn };

            // Act
            _sut.SetProviderFeedback(feedbackResult, provider);

            // Assert
            Assert.AreEqual(10, provider.ProviderFeedback.Strengths.Count);
        }

        [TestCase(9.9, 10)]
        [TestCase(9.51, 10)]
        [TestCase(9.5, 10)]
        [TestCase(9.49, 9)]
        [TestCase(9.2, 9)]
        [TestCase(90.9, 91)]
        [TestCase(90.51, 91)]
        [TestCase(90.5, 91)]
        [TestCase(90.49, 90)]
        [TestCase(90.2, 90)]
        [TestCase(0, null)]
        [TestCase(null, null)]
        public void ShouldRoundEmployerSatisfaction(double? finalScore, double? expected)
        {
            // Arrange
            var provider = new CoreProvider
            {
                Ukprn = 12345
            };

            var satisfactionRates = new EmployerSatisfactionRateResult
            {
                Rates = new List<SatisfactionRateProvider>
                {
                    new SatisfactionRateProvider
                    {
                        FinalScore = finalScore,
                        Ukprn = provider.Ukprn
                    }
                }
            };

            // Act
            _sut.SetEmployerSatisfactionRate(satisfactionRates, provider);

            // Assert
            Assert.AreEqual(expected, provider.EmployerSatisfaction);
        }

        [TestCase(9.9, 10)]
        [TestCase(9.51, 10)]
        [TestCase(9.5, 10)]
        [TestCase(9.49, 9)]
        [TestCase(9.2, 9)]
        [TestCase(90.9, 91)]
        [TestCase(90.51, 91)]
        [TestCase(90.5, 91)]
        [TestCase(90.49, 90)]
        [TestCase(90.2, 90)]
        [TestCase(0, null)]
        [TestCase(null, null)]
        public void ShouldRoundLearnerSatisfaction(double? finalScore, double? expected)
        {
            // Arrange
            var provider = new CoreProvider
            {
                Ukprn = 12345
            };

            var satisfactionRates = new LearnerSatisfactionRateResult
            {
                Rates = new List<SatisfactionRateProvider>
                {
                    new SatisfactionRateProvider
                    {
                        FinalScore = finalScore,
                        Ukprn = provider.Ukprn
                    }
                }
            };

            // Act
            _sut.SetLearnerSatisfactionRate(satisfactionRates, provider);

            // Assert
            Assert.AreEqual(expected, provider.LearnerSatisfaction);
        }

        private static int GetRatingCount(string providerRating, CoreProvider provider)
        {
            switch (providerRating)
            {
                case ProviderRatings.Excellent:
                    return provider.ProviderFeedback.ExcellentFeedbackCount;
                case ProviderRatings.Good:
                    return provider.ProviderFeedback.GoodFeedbackCount;
                case ProviderRatings.Poor:
                    return provider.ProviderFeedback.PoorFeedbackCount;
                case ProviderRatings.VeryPoor:
                    return provider.ProviderFeedback.VeryPoorFeedbackCount;
                default: return 0;
            }
        }

        private void SetRatings(string providerRating, int count)
        {
            _feedbackEntries
                .Where(fe => fe.ProviderRating != providerRating)
                .Take(count)
                .ToList()
                .ForEach(fe => fe.ProviderRating = providerRating);
        }

        private void SetProviderAttributesStrengths(int count)
        {
            _provAttributes
                .Where(pa => pa.Value != -1)
                .Take(count)
                .ToList()
                .ForEach(pa => pa.Value = 1);
        }

        private void SetProviderAttributesWeaknesses(int count)
        {
            _provAttributes
                .Where(pa => pa.Value != 1)
                .Take(count)
                .ToList()
                .ForEach(pa => pa.Value = -1);
        }
    }
}
