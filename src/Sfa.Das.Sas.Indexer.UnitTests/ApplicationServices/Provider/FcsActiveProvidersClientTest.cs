using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Fsc;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.UnitTests.ApplicationServices.Provider
{
    [TestFixture]
    public class FcsActiveProvidersClientTest
    {
        [Test]
        public void ShouldGetFcsActiveProviders()
        {
            var moqVstsClient = new Mock<IVstsClient>();
            var moqIProvideSettings = new Mock<IProvideSettings>();
            var appsettings = new AppServiceSettings(moqIProvideSettings.Object);
            var moqIConvertFromCsv = new Mock<IConvertFromCsv>();

            moqVstsClient.Setup(m => m.GetFileContent(It.IsAny<string>())).Returns(string.Empty);
            moqIConvertFromCsv.Setup(m => m.CsvTo<ActiveProviderCsvRecord>(It.IsAny<string>())).Returns(new[] { new ActiveProviderCsvRecord { UkPrn = 26 }, new ActiveProviderCsvRecord { UkPrn = 126 } });

            var client = new FcsActiveProvidersClient(moqVstsClient.Object, appsettings, moqIConvertFromCsv.Object, Mock.Of<ILogProvider>());
            var result = client.GetActiveProviders();

            result.Result.Count().Should().Be(2);
            result.Result.All(m => new[] { 26, 126 }.Contains(m)).Should().BeTrue();
        }
    }
}