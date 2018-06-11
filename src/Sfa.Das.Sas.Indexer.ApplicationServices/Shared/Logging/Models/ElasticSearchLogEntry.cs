using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Models
{
    public class ElasticSearchLogEntry : ILogEntry
    {
        public string Name => "ElasticSearch";

        public int? ReturnCode { get; set; }

        public long? SearchTime { get; set; }

        public double NetworkTime { get; set; }

        public string Url { get; set; }

        public string Body { get; set; }
    }
}
