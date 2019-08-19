using System;
using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models
{
    public sealed class StandardDocument : ApprenticeshipDocument, IIndexEntry
    {
        public StandardDocument()
            : base(nameof(StandardDocument))
        {
        }

        public int StandardId { get; set; }

        public bool Published { get; set; }

        public string EntryRequirements { get; set; }

        [Text(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public IEnumerable<string> JobRoles { get; set; }

        public string OverviewOfRole { get; set; }

        public string ProfessionalRegistration { get; set; }

        public string Qualifications { get; set; }

        public string WhatApprenticesWillLearn { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
        public DateTime? LastDateForNewStarts { get; set; }

        [Keyword(NullValue = "null")]
        public string StandardIdKeyword => StandardId.ToString();

        public int StandardSectorCode { get; set; }

        public List<FundingPeriod> FundingPeriods { get; set; }

		public bool RegulatedStandard { get; set; }
    }
}