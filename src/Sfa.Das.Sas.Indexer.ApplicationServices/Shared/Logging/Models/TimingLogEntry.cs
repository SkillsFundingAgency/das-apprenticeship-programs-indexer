using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Models
{
    public class TimingLogEntry : ILogEntry
    {
        public string Name => "Timing";

        public double ElaspedMilliseconds { get; set; }
    }
}
