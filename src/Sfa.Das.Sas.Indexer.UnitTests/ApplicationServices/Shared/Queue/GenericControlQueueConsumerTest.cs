using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;

namespace Sfa.Das.Sas.Indexer.UnitTests.ApplicationServices.Queue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

    [TestFixture]
    public sealed class GenericControlQueueConsumerTest
    {

        private GenericControlQueueConsumer _sut;
        private Mock<IAppServiceSettings> _mockServiceSettings;
        private Mock<IIndexerServiceFactory> _mockIndexerServiceFactory;
        private Mock<IIndexerService<IMaintainProviderIndex>> _mockIndexerService;
        private Mock<ILog> _mockLogger;
        private Mock<IMonitoringService> _mockMonitoringService;

        [SetUp]
        public void Setup()
        {
            _mockServiceSettings = new Mock<IAppServiceSettings>();
            _mockIndexerServiceFactory = new Mock<IIndexerServiceFactory>();
            _mockIndexerService = new Mock<IIndexerService<IMaintainProviderIndex>>();
            _mockLogger = new Mock<ILog>();
            _mockMonitoringService = new Mock<IMonitoringService>();

            _mockIndexerServiceFactory.Setup(x => x.GetIndexerService<IMaintainProviderIndex>()).Returns(_mockIndexerService.Object);

            _sut = new GenericControlQueueConsumer(
                _mockServiceSettings.Object,
                _mockIndexerServiceFactory.Object,
                _mockLogger.Object,
                _mockMonitoringService.Object);
        }

        [Test]
        public void ShouldDoNothingIfIndexerServiceIsNull()
        {
            _mockIndexerServiceFactory.Setup(x => x.GetIndexerService<IMaintainProviderIndex>()).Returns((IIndexerService<IMaintainProviderIndex>)null);

            var task = _sut.StartIndexer<IMaintainProviderIndex>();
            task.Wait(1000);

            _mockServiceSettings.Verify(x => x.QueueName(It.IsAny<Type>()), Times.Never);
        }

        [Test]
        public void ShouldLogErrorWhenSomethingFails()
        {
            try
            {
                var task = _sut.StartIndexer<IMaintainProviderIndex>();
                task.Wait(1000);
            }
            catch (Exception)
            {
                _mockLogger.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once());
                _mockMonitoringService.Verify(x => x.SendMonitoringNotification(It.IsAny<string[]>()), Times.Never);
            }
        }
    }
}
