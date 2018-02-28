using System;
using System.Collections;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;

namespace Sfa.Das.Sas.Indexer.UnitTests.Core.Models
{
    [TestFixture]
    public class LarsStandardTests
    {
        [Test]
        public void ShouldBeValidIfItHasNoEndDate()
        {
            // Arrange
            var sut = new LarsStandard
            {
                EffectiveFrom = DateTime.MinValue,
                EffectiveTo = null
            };

            // Act
            var result = sut.IsValidDate(DateTime.UtcNow);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeValidIfTheStartDateisTodayAndEndDateIsInFuture()
        {
            // Arrange
            var sut = new LarsStandard
            {
                EffectiveFrom = DateTime.UtcNow,
                EffectiveTo = DateTime.MaxValue
            };

            // Act
            var result = sut.IsValidDate(DateTime.UtcNow);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeInvalidIfItHasNoStartDate()
        {
            // Arrange
            var sut = new LarsStandard
            {
                EffectiveFrom = null,
                EffectiveTo = null
            };

            // Act
            var result = sut.IsValidDate(DateTime.UtcNow);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeInvalidIfTheStartDateIsInTheFuture()
        {
            // Arrange
            var sut = new LarsStandard
            {
                EffectiveFrom = DateTime.MaxValue,
                EffectiveTo = null
            };

            // Act
            var result = sut.IsValidDate(DateTime.UtcNow);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeInvalidIfTheEndDateIsInThePast()
        {
            // Arrange
            var sut = new LarsStandard
            {
                EffectiveFrom = DateTime.MaxValue,
                EffectiveTo = DateTime.UtcNow.AddYears(-1)
            };

            // Act
            var result = sut.IsValidDate(DateTime.UtcNow);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCaseSource(typeof(LarsStandardDateTestDataClass), "LarsStandardTestData")]
        public bool Testcase(DateTime effectivefrom, DateTime effectiveto)
        {
            // Arrange
            var sut = new LarsStandard
            {
                EffectiveFrom = effectivefrom,
                EffectiveTo = effectiveto
            };

            // Act
            return sut.IsValidDate(DateTime.UtcNow);
        }
    }

    public class LarsStandardDateTestDataClass
    {
        public static IEnumerable LarsStandardTestData
        {
            get
            {
                yield return new TestCaseData(DateTime.UtcNow, DateTime.MaxValue).SetName("ShouldBeValidIfTheStartDateisTodayAndEndDateIsInFuture").Returns(true);
                yield return new TestCaseData(DateTime.MinValue, DateTime.UtcNow).SetName("ShouldBeValidIfTheStartDateinPastAndEndDateIsToday").Returns(true);
            }
        }
    }
}