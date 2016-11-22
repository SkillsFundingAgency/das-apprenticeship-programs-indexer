using System.Threading.Tasks;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Queue
{
    public interface IGenericControlQueueConsumer
    {
        Task CheckMessage<T>()
            where T : IMaintainSearchIndexes;
    }
}