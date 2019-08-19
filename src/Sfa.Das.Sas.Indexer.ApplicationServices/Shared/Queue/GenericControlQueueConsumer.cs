using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue
{
    public class GenericControlQueueConsumer : IGenericControlQueueConsumer
    {
        private readonly IAppServiceSettings _appServiceSettings;
        private readonly IIndexerServiceFactory _indexerServiceFactory;
        private readonly ILog _log;
        private readonly IMonitoringService _monitor;

        public GenericControlQueueConsumer(
            IAppServiceSettings appServiceSettings,
            IIndexerServiceFactory indexerServiceFactory,
            ILog log,
            IMonitoringService monitor)
        {
            _appServiceSettings = appServiceSettings;
            _indexerServiceFactory = indexerServiceFactory;
            _log = log;
            _monitor = monitor;
        }

        public async Task StartIndexer<T>()
            where T : IMaintainSearchIndexes
        {
            try
            {
                var indexerService = _indexerServiceFactory.GetIndexerService<T>();
                if (indexerService == null)
                {
                    return;
                }

                try
                {
                    var indexTime = DateTime.Now;

                    await indexerService.CreateScheduledIndex(indexTime).ConfigureAwait(false);
                }
                catch (AggregateException ex)
                {
                    foreach (var exception in ex.InnerExceptions)
                    {
                        indexerService.Log.Fatal(exception, exception.Message);
                    }
                }
                catch (Exception ex)
                {
                    indexerService.Log.Fatal(ex, ex.Message);
                }

                _monitor.SendMonitoringNotification(_appServiceSettings.MonitoringUrl(typeof(T)));
            }
            catch (Exception ex)
            {
                _log.Fatal(ex, $"Unexpected Failure {IndexerNameLookup.GetIndexTypeName(typeof(T))}");
            }
        }
    }
}