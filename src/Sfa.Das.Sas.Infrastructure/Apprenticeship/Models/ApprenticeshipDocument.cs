namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models
{
    using System.Collections.Generic;
    using Nest;

    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

    public class ApprenticeshipDocument
    {
        [Text(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public string Title { get; set; }

        public int Level { get; set; }

        public double SectorSubjectAreaTier1 { get; set; }

        public double SectorSubjectAreaTier2 { get; set; }

        public int FundingCap { get; set; }

        public int Duration { get; set; }

        public TypicalLength TypicalLength { get; set; }

        [Text(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public IEnumerable<string> Keywords { get; set; }

        [Keyword(NullValue = "null")]
        public string TitleKeyword => Title;
    }
}