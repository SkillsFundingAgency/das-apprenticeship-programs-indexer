using System;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared
{
    public interface IIndexerService<T>
    {
        Task CreateScheduledIndex(DateTime scheduledRefreshDateTime);
    }
}