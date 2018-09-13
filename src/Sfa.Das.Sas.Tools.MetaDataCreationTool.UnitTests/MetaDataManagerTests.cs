using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models.Git;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces;

    [TestFixture]
    public class MetaDataManagerTests
    {
        [Test]
        public void GetAllAsJsonShouldReturnDictionary()
        {
            var mockLarsDataService = new Mock<ILarsDataService>();
            var mockElasticsearchDataService = new Mock<IElasticsearchLarsDataService>();
            var mockApprenticeshipRepoService = new Mock<IApprenticeshipRepoService>();
            var mockSettings = new Mock<IAppServiceSettings>();
            var mockAngleSharpService = new Mock<IAngleSharpService>();
            var mockLogger = new Mock<ILog>(MockBehavior.Loose);

            mockSettings.Setup(x => x.MetadataApiUri).Returns("www.abba.co.uk");

            var metaDataManager = new MetaDataManager(mockLarsDataService.Object, mockElasticsearchDataService.Object, mockApprenticeshipRepoService.Object, mockSettings.Object, mockAngleSharpService.Object, mockLogger.Object);

            var standardJson = metaDataManager.GetStandardsMetaData();

            Assert.That(standardJson, Is.TypeOf<List<StandardMetaData>>());
        }

        [Test]
        public void ShouldUpdateMetadataFromLars()
        {
            var mockLarsDataService = new Mock<ILarsDataService>();
            var mockElasticsearchDataService = new Mock<IElasticsearchLarsDataService>();
            var mockApprenticeshipRepoService = new Mock<IApprenticeshipRepoService>();
            var mockSettings = new Mock<IAppServiceSettings>();
            var mockAngleSharpService = new Mock<IAngleSharpService>();
            var mockLogger = new Mock<ILog>(MockBehavior.Loose);

            mockSettings.Setup(x => x.MetadataApiUri).Returns("www.abba.co.uk");

            // Add link
            var larsStandards = new List<LarsStandard> { new LarsStandard { Id = 2, Title = "Title1", NotionalEndLevel = 4 } };
            mockElasticsearchDataService.Setup(m => m.GetListOfStandards()).Returns(larsStandards);

            mockAngleSharpService.Setup(m => m.GetLinks("StandardUrl", ".attachment-details h2 a", "Apprenticeship")).Returns(new List<string> { "/link/to/ApprenticeshipPDF" });
            mockAngleSharpService.Setup(m => m.GetLinks("StandardUrl", ".attachment-details h2 a", "Assessment")).Returns(new List<string> { "/link/to/AssessmentPDF" });

            var standardsFromRepo = new List<StandardMetaData> { new StandardMetaData { Id = 2, Title = "Title1", Published = true }, new StandardMetaData { Id = 3, Title = "Title2", Published = true } };

            mockApprenticeshipRepoService.Setup(m => m.GetStandards()).Returns(standardsFromRepo);

            var metaDataManager = new MetaDataManager(mockLarsDataService.Object, mockElasticsearchDataService.Object, mockApprenticeshipRepoService.Object, mockSettings.Object, mockAngleSharpService.Object, mockLogger.Object);

            var standardJson = metaDataManager.GetStandardsMetaData();

            var standard = standardJson.FirstOrDefault(m => m.Id == 2);
            standard.NotionalEndLevel.Should().Be(4);

            Assert.That(standardJson, Is.TypeOf<List<StandardMetaData>>());
        }
    }
}