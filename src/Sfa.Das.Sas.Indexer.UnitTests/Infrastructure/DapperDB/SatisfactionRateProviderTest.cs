namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.DapperDB
{
    using System.Linq;
    using FluentAssertions;
    using Indexer.Infrastructure.Settings;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD;

    [TestFixture]
    public class SatisfactionRateProviderTest
    {
        [Test]
        [Category("ExternalDependency")]
        [Category("Integration")]
        [Ignore("explicit tests show up in the VSTS report")]
        public void ProviderTest()
        {
            var databaseProvider = new DatabaseProvider(new InfrastructureSettings(new MachineSettings()), Mock.Of<ILog>());
            var sut = new LearnerSatisfactionRatesProvider(databaseProvider, Mock.Of<ILog>());

            var result = sut.Handle(null).Rates.ToArray();

            result.Length.Should().BeGreaterThan(0);
            var totalCount = result.Length;

            result.Count().Should().Be(totalCount);
        }
    }
}