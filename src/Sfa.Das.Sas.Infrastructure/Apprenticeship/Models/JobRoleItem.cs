using Nest;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models
{
    public class JobRoleItem
    {
        [String(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public string Title { get; set; }

        [String(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustomText)]
        public string Description { get; set; }
    }
}