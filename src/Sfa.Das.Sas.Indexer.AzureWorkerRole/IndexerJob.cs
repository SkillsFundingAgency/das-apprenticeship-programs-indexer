using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue;

namespace Sfa.Das.Sas.Indexer.AzureWorkerRole
{
    public class IndexerJob : IIndexerJob
    {
        private readonly IGenericControlQueueConsumer _controlQueueConsumer;

        public IndexerJob(IGenericControlQueueConsumer controlQueueConsumer)
        {
            _controlQueueConsumer = controlQueueConsumer;
        }

        public void Run()
        {
            var tasks = new List<Task>
            {
                _controlQueueConsumer.CheckMessage<IMaintainApprenticeshipIndex>(),
                _controlQueueConsumer.CheckMessage<IMaintainProviderIndex>()
            };

            Task.WaitAll(tasks.ToArray());
        }
    }
}