namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Services
{
    using Indexer.Infrastructure.Shared.Services;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;

    [TestFixture]
    public class ProcessOrganisationTypeTests
    {
        [TestCase("Assessment Organisation", "Assessment Organisation", false)]
        [TestCase(" Assessment Organisation", "Assessment Organisation", false)]
        [TestCase("Assessment Organisation ", "Assessment Organisation", false)]
        [TestCase("assessment organisation ", "Assessment Organisation", false)]
        [TestCase("Awarding Organisation", "Awarding Organisation", false)]
        [TestCase("Employer or trade body", "Employer or trade body", false)]
        [TestCase("Higher Education Institution", "Higher Education Institution", false)]
        [TestCase("Other", "Other", false)]
        [TestCase("Professional body", "Professional body", false)]
        [TestCase("Sector Skills Council", "Sector Skills Council", false)]
        [TestCase("Training Provider", "Training Provider", false)]
        [TestCase("Unmatched Item", "Other", true)]
        public void RunTests(string organisationalType, string expectedOrganisationalType, bool raiseWarningBecauseUnmatchedValue)
        {
            var logObject = new Mock<ILog>();
            var actualOrganisationalType = new OrganisationTypeProcessor(logObject.Object).ProcessOrganisationType(organisationalType);

            Assert.AreEqual(actualOrganisationalType, expectedOrganisationalType);

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
