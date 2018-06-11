using System;
using System.Threading.Tasks;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared
{
    public interface IIndexerService<T>
    {
        ILog Log { get; }
        Task CreateScheduledIndex(DateTime scheduledRefreshDateTime);
    }
}