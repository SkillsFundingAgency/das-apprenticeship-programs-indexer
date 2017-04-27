using NUnit.Framework;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;

namespace Sfa.Das.Sas.Indexer.UnitTests.Core.Models
{
    [TestFixture]
    public class ProviderTests
    {
        [Test]
        public void ShouldBeInvalidIfMissingAName()
        {
            // Arrange
            var sut = CreateValidProvider();
            sut.Name = null;

            // Act
            var result = sut.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeInvalidIfUkprnIsShort()
        {
            // Arrange
            var sut = CreateValidProvider();
            sut.Ukprn = 0;

            // Act
            var result = sut.IsValid();

            // Assert
            Assert.IsFalse(result);
        }

        private Provider CreateValidProvider()
        {
            return new Provider
            {
                Ukprn = 12345678,
                Name = "Test"
            };
        }
    }
}