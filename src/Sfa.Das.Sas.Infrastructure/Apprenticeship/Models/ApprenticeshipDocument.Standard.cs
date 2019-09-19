using System;
using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models
{
    public partial class ApprenticeshipDocument
    {
        public int? StandardId { get; set; }

        [Text(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public IEnumerable<string> JobRoles { get; set; }

        public string OverviewOfRole { get; set; }

        public string Qualifications { get; set; }

        public string WhatApprenticesWillLearn { get; set; }

        public DateTime? LastDateForNewStarts { get; set; }

        [Keyword]
        public string StandardIdKeyword { get; set; }

        public int? StandardSectorCode { get; set; }

        public bool? RegulatedStandard { get; set; }
    }
}