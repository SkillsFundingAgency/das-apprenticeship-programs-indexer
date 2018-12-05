namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Services
{
    using Indexer.Infrastructure.Shared.Services;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;

    [TestFixture]
    public class ProcessOrganisationTypeTests
    {
        [TestCase("Assessment Organisations", "Assessment Organisations", false)]
        [TestCase(" Assessment Organisations", "Other", true)]
        [TestCase("Assessment Organisations ", "Other", true)]
        [TestCase("assessment organisations", "Other", true)]
        [TestCase("Awarding Organisations", "Awarding Organisations", false)]
        [TestCase("Trade body", "Trade body", false)]
        [TestCase("HEI", "HEI", false)]
        [TestCase("Other", "Other", false)]
        [TestCase("Professional body", "Professional body", false)]
        [TestCase("NSA or SSC", "NSA or SSC", false)]
        [TestCase("Public Sector", "Public Sector", false)]
        [TestCase("College", "College", false)]
        [TestCase("Academy or Free School", "Academy or Free School", false)]
		[TestCase("Training Provider", "Training Provider", false)]
        [TestCase("Unmatched Item", "Other", true)]
        public void RunTests(string organisationalType, string expectedOrganisationalType, bool raiseWarningBecauseUnmatchedValue)
        {
            var logObject = new Mock<ILog>();
            var actualOrganisationalType = new OrganisationTypeProcessor(logObject.Object).ProcessOrganisationType(organisationalType);

            Assert.AreEqual(expectedOrganisationalType, actualOrganisationalType);

            if (raiseWarningBecauseUnmatchedValue)
            {
                logObject.Verify(x => x.Warn(It.IsAny<string>()), Times.Once());
            }
            else
            {
                logObject.Verify(x => x.Warn(It.IsAny<string>()), Times.Never());
            }
        }
    }
}
