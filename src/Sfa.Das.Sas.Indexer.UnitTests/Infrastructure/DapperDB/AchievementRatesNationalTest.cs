using System;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.DapperDB
{
    [TestFixture]
    public class AchievementRatesNationalTest
    {
        [Test]
        [Category("ExternalDependency")]
        [Category("Integration")]
        [Explicit]
        public void NationalTest()
        {
            var databaseProvider = new DatabaseProvider(new InfrastructureSettings(new MachineSettings()), Mock.Of<ILog>());
            var sut = new AchievementRatesNational(databaseProvider, Mock.Of<ILog>());

            var result = sut.Handle(null).Rates.ToArray();

            result.Length.Should().BeGreaterThan(0);
            var totalCount = result.Length;

            result.Any(m => Math.Abs(m.Ssa2Code) > 0.0).Should().BeTrue();
            result.Count(m => m.InstitutionType.Equals("All institution type", StringComparison.InvariantCultureIgnoreCase)).Should().Be(totalCount);
            result.Count(m => m.Age.Equals("All Age")).Should().Be(totalCount);
            result.Count(m => !m.SectorSubjectAreaTier2.Equals("All SSA T2", StringComparison.InvariantCultureIgnoreCase)).Should().Be(totalCount);
            result.Count(m => !m.ApprenticeshipLevel.Equals("All", StringComparison.InvariantCultureIgnoreCase)).Should().Be(totalCount);
        }
    }
}