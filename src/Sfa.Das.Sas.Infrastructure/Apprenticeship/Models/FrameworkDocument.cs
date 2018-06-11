using System;
using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models
{
    public sealed class FrameworkDocument : ApprenticeshipDocument, IIndexEntry
    {
        [Keyword(NullValue = "null")]
        public string FrameworkId { get; set; }

        public bool Published { get; set; }

        public int FrameworkCode { get; set; }

        [Text(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public string FrameworkName { get; set; }

        public int PathwayCode { get; set; }

        public int ProgType { get; set; }

        [Text(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public string PathwayName { get; set; }

        public IEnumerable<JobRoleItem> JobRoleItems { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string EntryRequirements { get; set; }

        public string ProfessionalRegistration { get; set; }

        public string CompletionQualifications { get; set; }

        public string FrameworkOverview { get; set; }

        public IEnumerable<string> CompetencyQualification { get; set; }

        public IEnumerable<string> KnowledgeQualification { get; set; }

        public IEnumerable<string> CombinedQualification { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        [Keyword(NullValue = "null")]
        public string FrameworkIdKeyword => FrameworkId;
    }
}