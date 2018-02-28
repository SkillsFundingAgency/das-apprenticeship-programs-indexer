using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

namespace Sfa.Das.Sas.Indexer.UnitTests.ApplicationServices.Services
{
    [TestFixture]
    public class ProviderExclusionServiceTests
    {
        const int ukprn = 12344321;
        private Mock<IIndexSettings<IMaintainProviderIndex>> _mockSettings;
        [SetUp]
        public void Setup()
        {
            _mockSettings = new Mock<IIndexSettings<IMaintainProviderIndex>>(); 
        }

        [Test]
        public void CheckExclusionListIsPersistingThroughCalls()
        {
            _mockSettings.Setup(x => x.ProviderExclusionList).Returns("11112222,11113333,33331111,12345667");

            var providerExclusionService = new ProviderExclusionService(_mockSettings.Object);

            providerExclusionService.IsProviderInExclusionList(ukprn);
            providerExclusionService.IsProviderInExclusionList(ukprn);
            providerExclusionService.IsProviderInExclusionList(ukprn);

            _mockSettings.Verify(x => x.ProviderExclusionList, Times.Once);
        }

        [Test]
        public void CheckExclusionListIsPersistingThroughCallsEvenWhenEmpty()
        {
            _mockSettings.Setup(x => x.ProviderExclusionList).Returns(string.Empty);

            var providerExclusionService = new ProviderExclusionService(_mockSettings.Object);
            providerExclusionService.IsProviderInExclusionList(ukprn);
            providerExclusionService.IsProviderInExclusionList(ukprn);
            providerExclusionService.IsProviderInExclusionList(ukprn);

            _mockSettings.Verify(x => x.ProviderExclusionList, Times.Once);
        }

        [TestCase(12345677, " 12344322 , 12312312 , 12345678 ", false)]
        [TestCase(12345677, "", false)]
        [TestCase(12345677, ",", false)]
        [TestCase(1234567, "1234567", false)]
        [TestCase(12345678, "12344322,12312312,12345678", true)]
        [TestCase(12345678, " 12344322 , 12312312 , 12345678 ", true)]
        [TestCase(12345677, "12345677", true)]

        public void CheckExlusionListIsReturningCorrectResults(int prn, string exclusionList, bool expectedResult)
        {
            _mockSettings.Setup(x => x.ProviderExclusionList).Returns(exclusionList);
            var providerExclusionService = new ProviderExclusionService(_mockSettings.Object);
            var actual = providerExclusionService.IsProviderInExclusionList(prn);
            Assert.AreEqual(expectedResult,actual);
        }
    }
}
