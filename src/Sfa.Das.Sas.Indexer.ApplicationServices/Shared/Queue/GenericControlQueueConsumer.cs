using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

    public class GenericControlQueueConsumer : IGenericControlQueueConsumer
    {
        private readonly IAppServiceSettings _appServiceSettings;
        private readonly IMessageQueueService _cloudQueueService;
        private readonly IIndexerServiceFactory _indexerServiceFactory;
        private readonly ILog _log;
        private readonly IMonitoringService _monitor;

        public GenericControlQueueConsumer(
            IAppServiceSettings appServiceSettings,
            IMessageQueueService cloudQueueService,
            IIndexerServiceFactory indexerServiceFactory,
            ILog log,
            IMonitoringService monitor)
        {
            _appServiceSettings = appServiceSettings;
            _cloudQueueService = cloudQueueService;
            _indexerServiceFactory = indexerServiceFactory;
            _log = log;
            _monitor = monitor;
        }

        public async Task CheckMessage<T>()
            where T : IMaintainSearchIndexes
        {
            var indexerService = _indexerServiceFactory.GetIndexerService<T>();

            if (indexerService == null)
            {
                return;
            }

            try
            {
                var queueName = _appServiceSettings.QueueName(typeof(T));

                if (string.IsNullOrEmpty(queueName))
                {
                    return;
                }

                var messages = _cloudQueueService.GetQueueMessages(queueName)?.ToArray();

                if (messages != null && messages.Any())
                {
                    var latestMessage = messages?.FirstOrDefault();

                    var extraMessages = messages?.Where(m => m != latestMessage).ToList();

                    // Delete all the messages except the first as they are not needed
                    _cloudQueueService.DeleteQueueMessages(queueName, extraMessages);

                    var indexTime = latestMessage?.InsertionTime ?? DateTime.Now;

                    await indexerService.CreateScheduledIndex(indexTime).ConfigureAwait(false);

                    _cloudQueueService.DeleteQueueMessage(queueName, latestMessage);
                }

                _monitor.SendMonitoringNotification(_appServiceSettings.MonitoringUrl(typeof(T)));
            }
            catch (Exception ex)
            {
                _log.Error(ex, $"Something failed creating index: {typeof(T)}");
            }
        }
    }
}