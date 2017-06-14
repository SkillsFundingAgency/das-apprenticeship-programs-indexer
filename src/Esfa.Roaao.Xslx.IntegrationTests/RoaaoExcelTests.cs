using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.AzureWorkerRole.DependencyResolution;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Esfa.Roaao.Xslx.IntegrationTests
{
    [TestClass]
    public class RoaaoExcelTests
    {
        private AssessmentOrganisationsDTO results;

        [TestInitialize]
        public void Init()
        {
            // Arrange
            var container = IoC.Initialize();
            var sut = container.GetInstance<IGetAssessmentOrgsData>();

            // Act
            results = sut.GetAssessmentOrganisationsData();
        }


        [TestMethod]
        public void EpaShouldExist()
        {
            foreach (var standardOrganisationsData in results.StandardOrganisationsData)
            {
                Assert.IsTrue(results.Organisations.Any(x => x.EpaOrganisationIdentifier == standardOrganisationsData.EpaOrganisationIdentifier), $"Couldn't find the Assessment Org for {standardOrganisationsData.EpaOrganisationIdentifier}");
            }
        }
    }
}
