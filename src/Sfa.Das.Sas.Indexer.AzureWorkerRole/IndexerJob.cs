using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;

namespace Sfa.Das.Sas.Indexer.AzureWorkerRole
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue;

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
                //_controlQueueConsumer.CheckMessage<IMaintainProviderIndex>(),
                //_controlQueueConsumer.CheckMessage<IMaintainLarsIndex>(),
                //_controlQueueConsumer.CheckMessage<IMaintainAssessmentOrgsIndex>()
            };

            Task.WaitAll(tasks.ToArray());
        }
    }
}