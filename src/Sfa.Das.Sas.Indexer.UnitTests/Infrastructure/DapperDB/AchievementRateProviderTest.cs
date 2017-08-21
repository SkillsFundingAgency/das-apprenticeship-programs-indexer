namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.DapperDB
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Indexer.Infrastructure.Settings;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD;

    [TestFixture]
    public class AchievementRateProviderTest
    {
        [Test]
        [Category("ExternalDependency")]
        [Category("Integration")]
        [Ignore("explicit tests show up in the VSTS report")]
        public void ProviderTest()
        {
            var databaseProvider = new DatabaseProvider(new InfrastructureSettings(new MachineSettings()), Mock.Of<ILog>());
            var sut = new AchievementRatesProvider(databaseProvider, Mock.Of<ILog>());

            var result = sut.Handle(null).Rates.ToArray();

            result.Length.Should().BeGreaterThan(0);
            var totalCount = result.Length;

            result.Any(m => Math.Abs(m.Ssa2Code) > 0.0).Should().BeTrue();
            result.Count(m => m.Age.Equals("All Age")).Should().Be(totalCount);
            result.Count(m => !m.SectorSubjectAreaTier2.Equals("All SSA T2")).Should().Be(totalCount);
            result.Count(m => !m.ApprenticeshipLevel.Equals("All")).Should().Be(totalCount);
        }

    }
}