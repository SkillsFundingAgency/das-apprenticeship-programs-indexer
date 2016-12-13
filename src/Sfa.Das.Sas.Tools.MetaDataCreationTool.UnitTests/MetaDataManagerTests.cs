using System.Collections.Generic;
using System.Linq;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services.Interfaces;

namespace Sfa.Das.Sas.Tools.MetaDataCreationTool.UnitTests
{
    using FluentAssertions;

    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Tools.MetaDataCreationTool.Models.Git;

    [TestFixture]
    public class MetaDataManagerTests
    {
        [Test]
        public void GenerateStandardMetadataFilesTestShouldPushOnlyMissingStandardsToGit()
        {
            // ILarsDataService larsDataService, IVstsService vstsService, IAppServiceSettings settings
            var mockLarsDataService = new Mock<ILarsDataService>();
            var mockVstsService = new Mock<IVstsService>();
            var mockSettings = new Mock<IAppServiceSettings>();
            var mockLogger = new Mock<ILog>(MockBehavior.Loose);
            List<FileContents> standardsToAdd = null;

            var currentStandards = new List<LarsStandard>
                                       {
                                           new LarsStandard { Id = 1, Title = "One" },
                                           new LarsStandard { Id = 2, Title = "Two" },
                                           new LarsStandard { Id = 3, Title = "Three" }
                                       };
            var existingMetaDataIds = new List<string> { "1", "2" };

            mockLarsDataService.Setup(x => x.GetListOfCurrentStandards()).Returns(currentStandards);
            mockVstsService.Setup(x => x.GetExistingStandardIds()).Returns(existingMetaDataIds);
            mockVstsService.Setup(x => x.PushCommit(It.IsAny<List<FileContents>>())).Callback<List<FileContents>>(x => { standardsToAdd = x; });

            var metaDataManager = new MetaDataManager(mockLarsDataService.Object, mockVstsService.Object, mockSettings.Object, null, mockLogger.Object);

            metaDataManager.GenerateStandardMetadataFiles();

            Assert.That(standardsToAdd.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllAsJsonShouldReturnDictionary()
        {
            // ILarsDataService larsDataService, IVstsService vstsService, IAppServiceSettings settings
            var mockLarsDataService = new Mock<ILarsDataService>();
            var mockVstsService = new Mock<IVstsService>();
            var mockSettings = new Mock<IAppServiceSettings>();
            var mockAngleSharpService = new Mock<IAngleSharpService>();
            var mockLogger = new Mock<ILog>(MockBehavior.Loose);

            mockSettings.Setup(x => x.MetadataApiUri).Returns("www.abba.co.uk");

            var metaDataManager = new MetaDataManager(mockLarsDataService.Object, mockVstsService.Object, mockSettings.Object, mockAngleSharpService.Object, mockLogger.Object);

            var standardJson = metaDataManager.GetStandardsMetaData();

            Assert.That(standardJson, Is.TypeOf<List<StandardMetaData>>());
        }

        [Test]
        public void ShouldUpdateMetadataFromLars()
        {
            var mockLarsDataService = new Mock<ILarsDataService>();
            var mockVstsService = new Mock<IVstsService>();
            var mockSettings = new Mock<IAppServiceSettings>();
            var mockAngleSharpService = new Mock<IAngleSharpService>();
            var mockLogger = new Mock<ILog>(MockBehavior.Loose);

            mockSettings.Setup(x => x.MetadataApiUri).Returns("www.abba.co.uk");

            // Add link
            var larsStandards = new List<LarsStandard> { new LarsStandard { Id = 2, Title = "Title1", NotionalEndLevel = 4 } };
            mockLarsDataService.Setup(m => m.GetListOfCurrentStandards()).Returns(larsStandards);

            mockAngleSharpService.Setup(m => m.GetLinks("StandardUrl", ".attachment-details h2 a", "Apprenticeship")).Returns(new List<string> { "/link/to/ApprenticeshipPDF" });
            mockAngleSharpService.Setup(m => m.GetLinks("StandardUrl", ".attachment-details h2 a", "Assessment")).Returns(new List<string> { "/link/to/AssessmentPDF" });

            var standardsFromRepo = new List<StandardMetaData> { new StandardMetaData { Id = 2, Title = "Title1", Published = true }, new StandardMetaData { Id = 3, Title = "Title2", Published = true } };
            
            mockVstsService.Setup(m => m.GetStandards()).Returns(standardsFromRepo);

            var metaDataManager = new MetaDataManager(mockLarsDataService.Object, mockVstsService.Object, mockSettings.Object, mockAngleSharpService.Object, mockLogger.Object);

            var standardJson = metaDataManager.GetStandardsMetaData();

            var standard = standardJson.FirstOrDefault(m => m.Id == 2);
            standard.NotionalEndLevel.Should().Be(4);

            var standard2 = standardJson.FirstOrDefault(m => m.Id == 3);
            standard2.StandardPdfUrl.Should().BeNullOrEmpty();
            standard2.AssessmentPlanPdfUrl.Should().BeNullOrEmpty();
            standard2.NotionalEndLevel.Should().Be(0);

            Assert.That(standardJson, Is.TypeOf<List<StandardMetaData>>());
        }

        [Test]
        public void ShouldCreatePdfLinks()
        {
            var mockLarsDataService = new Mock<ILarsDataService>();
            var mockVstsService = new Mock<IVstsService>();
            var mockSettings = new Mock<IAppServiceSettings>();
            var mockAngleSharpService = new Mock<IAngleSharpService>();
            var mockLogger = new Mock<ILog>(MockBehavior.Loose);

            mockSettings.Setup(x => x.GovWebsiteUrl).Returns("https://www.gov.uk/");
            mockSettings.Setup(x => x.MetadataApiUri).Returns("https://www.abba.co.uk/");

            // Add link
            var larsStandards = new List<LarsStandard> { new LarsStandard { Id = 2, Title = "Title1", NotionalEndLevel = 4, StandardUrl = "StandardUrl" } };
            mockLarsDataService.Setup(m => m.GetListOfCurrentStandards()).Returns(larsStandards);

            mockAngleSharpService.Setup(m => m.GetLinks("StandardUrl", ".attachment-details h2 a", "Apprenticeship")).Returns(new List<string> { "/link/to/ApprenticeshipPDF" });
            mockAngleSharpService.Setup(m => m.GetLinks("StandardUrl", ".attachment-details h2 a", "Assessment")).Returns(new List<string> { "/link/to/AssessmentPDF" });

            var standardsFromRepo = new List<StandardMetaData> { new StandardMetaData { Id = 2, Title = "Title1", Published = true }, new StandardMetaData { Id = 3, Title = "Title2" } };

            mockVstsService.Setup(x => x.GetStandards()).Returns(standardsFromRepo);

            var metaDataManager = new MetaDataManager(mockLarsDataService.Object, mockVstsService.Object, mockSettings.Object, mockAngleSharpService.Object, mockLogger.Object);

            var standardJson = metaDataManager.GetStandardsMetaData();

            var standard = standardJson.FirstOrDefault(m => m.Id == 2);
            standard.StandardPdfUrl.Should().Be("https://www.gov.uk/link/to/ApprenticeshipPDF");
            standard.AssessmentPlanPdfUrl.Should().Be("https://www.gov.uk/link/to/AssessmentPDF");
        }

        [Test]
        public void ShouldNotCreatePdfLinksIfNoUriToStandard()
        {
            var mockLarsDataService = new Mock<ILarsDataService>();
            var mockVstsService = new Mock<IVstsService>();
            var mockSettings = new Mock<IAppServiceSettings>();
            var mockAngleSharpService = new Mock<IAngleSharpService>();
            var mockLogger = new Mock<ILog>(MockBehavior.Loose);

            mockSettings.Setup(x => x.MetadataApiUri).Returns("https://www.abba.co.uk/");

            // Add link
            var larsStandards = new List<LarsStandard> { new LarsStandard { Id = 2, Title = "Title1", NotionalEndLevel = 4 } };
            mockLarsDataService.Setup(m => m.GetListOfCurrentStandards()).Returns(larsStandards);

            mockAngleSharpService.Setup(m => m.GetLinks("StandardUrl", ".attachment-details h2 a", "Apprenticeship")).Returns(new List<string> { "/link/to/ApprenticeshipPDF" });
            mockAngleSharpService.Setup(m => m.GetLinks("StandardUrl", ".attachment-details h2 a", "Assessment")).Returns(new List<string> { "/link/to/AssessmentPDF" });

            var standardsFromRepo = new List<StandardMetaData> { new StandardMetaData { Id = 2, Title = "Title1", Published = true }, new StandardMetaData { Id = 3, Title = "Title2", Published = true } };
            mockVstsService.Setup(m => m.GetStandards()).Returns(standardsFromRepo);

            var metaDataManager = new MetaDataManager(mockLarsDataService.Object, mockVstsService.Object, mockSettings.Object, mockAngleSharpService.Object, mockLogger.Object);

            var standardJson = metaDataManager.GetStandardsMetaData();

            var standard = standardJson.FirstOrDefault(m => m.Id == 2);
            standard.StandardPdfUrl.Should().BeEmpty();
            standard.AssessmentPlanPdfUrl.Should().BeEmpty();
        }
    }
}