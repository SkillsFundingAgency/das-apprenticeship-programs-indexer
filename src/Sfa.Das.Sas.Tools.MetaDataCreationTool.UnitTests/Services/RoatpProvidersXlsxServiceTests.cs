namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.UnitTests.Services
{
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services;

    [TestFixture]
    public class RoatpProvidersXlsxServiceTests
    {
        [TestCase("main provider", ProviderType.MainProvider)]
        [TestCase("Main Provider", ProviderType.MainProvider)]
        [TestCase("Mian provider", ProviderType.Unknown)]
        [TestCase("MainProvider", ProviderType.Unknown)]
        [TestCase(" ", ProviderType.Unknown)]
        [TestCase("", ProviderType.Unknown)]
        [TestCase(null, ProviderType.Unknown)]
        [TestCase("Supporting Provider", ProviderType.SupportingProvider)]
        [TestCase("Employer Provider", ProviderType.EmployerProvider)]
        [TestCase(" Employer Provider", ProviderType.EmployerProvider)]
        [TestCase("Employer Provider ", ProviderType.EmployerProvider)]
        public void ShouldMatchTheProviderType(string input, ProviderType expected)
        {
            // Arrange
            var sut = new RoatpProvidersXlsxService(null, new Mock<ILog>().Object);

            // Act
            var result = sut.GetProviderType(input, null);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase("Mian provider")]
        [TestCase("MainProvider")]
        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("MainProvider")]
        public void ShouldLogUnknownProvider(string providertype)
        {
            // Arrange
            var logObject = new Mock<ILog>();
            var sut = new RoatpProvidersXlsxService(null, logObject.Object);

            // Act
            var result = sut.GetProviderType(providertype, null);

            // Assert
            logObject.Verify(x => x.Warn(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Once());
        }

        [TestCase("main provider")]
        [TestCase("Main Provider")]
        [TestCase(" Employer Provider")]
        [TestCase("Employer Provider ")]
        public void ShouldNotLogknownProvider(string providertype)
        {
            // Arrange
            var logObject = new Mock<ILog>();
            var sut = new RoatpProvidersXlsxService(null, logObject.Object);

            // Act
            var result = sut.GetProviderType(providertype, null);

            // Assert
            logObject.Verify(x => x.Warn(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Never());
        }
    }
}
