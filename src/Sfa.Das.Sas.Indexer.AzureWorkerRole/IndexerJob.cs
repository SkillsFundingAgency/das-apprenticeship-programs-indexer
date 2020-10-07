using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;

namespace Sfa.Das.Sas.Indexer.AzureWorkerRole
{
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
           // _controlQueueConsumer.StartIndexer<IMaintainLarsIndex>().Wait();
            _controlQueueConsumer.StartIndexer<IMaintainAssessmentOrgsIndex>().Wait();
           // _controlQueueConsumer.StartIndexer<IMaintainApprenticeshipIndex>().Wait();
           // _controlQueueConsumer.StartIndexer<IMaintainProviderIndex>().Wait();
        }
    }
}