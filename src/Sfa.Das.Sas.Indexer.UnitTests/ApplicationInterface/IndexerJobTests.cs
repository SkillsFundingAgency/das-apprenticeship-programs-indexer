using Moq;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue;
using Sfa.Das.Sas.Indexer.AzureWorkerRole;

namespace Sfa.Das.Sas.Indexer.UnitTests.ApplicationInterface
{
    [TestFixture]
    public class IndexerJobTests
    {
        [Test]
        [Ignore("too slow")]
        public void ShouldCheckForProvidersAndStandardsToIndex()
        {
            // Arrange
            var mockConsumer = new Mock<IGenericControlQueueConsumer>();
            var sut = new IndexerJob(mockConsumer.Object);

            // Act
            sut.Run();

            // Assert
            mockConsumer.Verify(x => x.CheckMessage<IMaintainApprenticeshipIndex>());
            mockConsumer.Verify(x => x.CheckMessage<IMaintainProviderIndex>());
        }
    }
}