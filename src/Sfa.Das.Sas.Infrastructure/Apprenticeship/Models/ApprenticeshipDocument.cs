﻿using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models
{
    public partial class ApprenticeshipDocument
    {
        public ApprenticeshipDocument(ProgrammeType type)
        {
            ApprenticeshipType = type;
        }

        [StringEnum]
        public ProgrammeType ApprenticeshipType { get; set; }

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

        public bool Published { get; set; }

        public string EntryRequirements { get; set; }

        public string ProfessionalRegistration { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public List<FundingPeriod> FundingPeriods { get; set; }

    }
}