using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using OfficeOpenXml;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Infrastructure;
using Sfa.Das.Sas.Tools.MetaDataCreationTool.Services;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.UnitTests.MetaDataCreationTool.Services
{
    [TestFixture]
    public class AssessmentOrgsXlsxServiceTests
    {
        [Test]
        public void ShouldFilterOrganisationsIfInvalidDataOrDuplicates()
        {
            var organisationsList = LoadOrganisationsTestData();

            var moqAssessmentOrgsExcelPackageService = new Mock<IAssessmentOrgsExcelPackageService>();
            var moqWebClient = new Mock<IWebClient>();
            var moqAppServiceSettings = new Mock<IAppServiceSettings>();
            var mockLog = new Mock<ILog>();

            moqAssessmentOrgsExcelPackageService.Setup(x => x.GetExcelPackageFromFilePath(It.IsAny<string>())).Returns(new ExcelPackage());
            moqAssessmentOrgsExcelPackageService.Setup(x => x.GetAssessmentOrganisations(It.IsAny<ExcelPackage>())).Returns(organisationsList);

            moqAppServiceSettings.Setup(x => x.VstsAssessmentOrgsUrl).Returns("http://www.abba.co.uk");

            var sut = new AssessmentOrgsXlsxService(moqAssessmentOrgsExcelPackageService.Object, moqWebClient.Object, moqAppServiceSettings.Object, mockLog.Object);

            var actual = sut.GetAssessmentOrganisationsData().Organisations;

            var expected = new List<Organisation>
            {
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA1",
                    EpaOrganisation = "EPAorganisation1"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA2",
                    EpaOrganisation = "EPAorganisation2"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA3",
                    EpaOrganisation = "EPAorganisation3"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA4",
                    EpaOrganisation = "EPAorganisation4"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA5",
                    EpaOrganisation = "EPAorganisation5"
                }
            };

            actual.Should().NotBeNull();
            actual.Count.Should().Be(expected.Count);
        }

        public List<Organisation> LoadOrganisationsTestData()
        {
            return new List<Organisation>
            {
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA1",
                    EpaOrganisation = "EPAorganisation1"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA2",
                    EpaOrganisation = "EPAorganisation2"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA3",
                    EpaOrganisation = "EPAorganisation3"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA4",
                    EpaOrganisation = "EPAorganisation4"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA4",
                    EpaOrganisation = "EPAorganisation4"
                },
                new Organisation
                {
                    EpaOrganisationIdentifier = "EPA5",
                    EpaOrganisation = "EPAorganisation5"
                },new Organisation
                {
                    EpaOrganisationIdentifier = "Epa5",
                    EpaOrganisation = "EPAorganisation5"
                }
            };
        }
    }
}
