using NUnit.Framework;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services;

namespace Sfa.Das.Sas.Indexer.UnitTests.MetaDataCreationTool.Services
{
    [TestFixture]
    public class AssessmentOrgsExcelPackageServiceTests
    {
        [TestCase("11112222", 11112222L)]
        [TestCase("011122222", 11122222L)]
        [TestCase("1111222", null)]
        [TestCase("111122222", null)]
        [TestCase("", null)]
        [TestCase("-", null)]
        public void ShouldProcessUkprnStringToReturnOnlyValidUkprns(string ukprnInput, long? expected)
        {
            var sut = new AssessmentOrgsExcelPackageService();
            var actual = sut.CheckForValidUkprn(ukprnInput);
            Assert.AreEqual(expected, actual);
        }
    }
}
