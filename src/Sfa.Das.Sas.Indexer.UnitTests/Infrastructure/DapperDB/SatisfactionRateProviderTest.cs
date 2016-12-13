namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.DapperDB
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Indexer.Core.Logging;
    using Indexer.Infrastructure.DapperBD;
    using Indexer.Infrastructure.Settings;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class SatisfactionRateProviderTest
    {
        [Test]
        [Category("ExternalDependency")]
        [Category("Integration")]
        [Explicit]
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