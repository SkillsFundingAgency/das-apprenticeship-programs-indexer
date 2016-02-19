﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Sfa.Eds.Das.Indexer.Common.AzureAbstractions;
using Sfa.Eds.Das.Indexer.Common.Settings;
using Sfa.Eds.Das.ProviderIndexer.Services;
using Sfa.Eds.Das.ProviderIndexer.Settings;

namespace Sfa.Eds.Das.ProviderIndexer.Consumers
{
    public class ProviderControlQueueConsumer : IProviderControlQueueConsumer
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICloudQueueService _cloudQueueService;

        private readonly IAzureSettings _azureSettings;
        private readonly IProviderIndexerService _providerIndexerService;

        public ProviderControlQueueConsumer(
            IProviderIndexerService providerIndexerService,
            IAzureSettings azureSettings,
            ICloudQueueService cloudQueueService)
        {
            _azureSettings = azureSettings;
            _cloudQueueService = cloudQueueService;
            _providerIndexerService = providerIndexerService;
        }

        public Task CheckMessage()
        {
            return Task.Run(() =>
            {
                try
                {
                    var queue = _cloudQueueService.GetQueueReference(_azureSettings.ConnectionString, _azureSettings.QueueName);
                    var cloudQueueMessages = queue.GetMessages(10);
                    var messages = cloudQueueMessages.OrderByDescending(x => x.InsertionTime);

                    if (messages.Any())
                    {
                        var message = messages.FirstOrDefault();
                        if (message != null)
                        {
                            _providerIndexerService.CreateScheduledIndex(message.InsertionTime?.DateTime ?? DateTime.Now);
                        }
                    }

                    foreach (var cloudQueueMessage in messages)
                    {
                        queue.DeleteMessage(cloudQueueMessage);
                    }
                }
                catch (Exception ex)
                {
                    Log.Fatal("Something failed creating index: " + ex);
                    throw;
                }
            });
        }
    }
}