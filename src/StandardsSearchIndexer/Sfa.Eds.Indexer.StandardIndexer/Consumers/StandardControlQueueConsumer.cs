﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Sfa.Eds.Das.Indexer.Common.AzureAbstractions;
using Sfa.Eds.Das.Indexer.Common.Settings;
using Sfa.Eds.Das.StandardIndexer.Services;
using Sfa.Eds.Das.StandardIndexer.Settings;

namespace Sfa.Eds.Das.StandardIndexer.Consumers
{
    public class StandardControlQueueConsumer : IStandardControlQueueConsumer
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICloudQueueService _cloudQueueService;

        private readonly IAzureSettings _azureSettings;
        private readonly IStandardIndexerService _standardIndexerService;

        public StandardControlQueueConsumer(
            IStandardIndexerService standardIndexerService,
            IAzureSettings azureSettings,
            ICloudQueueService cloudQueueService)
        {
            _azureSettings = azureSettings;
            _cloudQueueService = cloudQueueService;
            _standardIndexerService = standardIndexerService;
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
                            _standardIndexerService.CreateScheduledIndex(message.InsertionTime?.DateTime ?? DateTime.Now);
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