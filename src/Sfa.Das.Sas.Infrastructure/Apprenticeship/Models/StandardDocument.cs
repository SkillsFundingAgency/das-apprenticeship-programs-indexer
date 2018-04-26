﻿using System;
using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models
{
    public sealed class StandardDocument : ApprenticeshipDocument, IIndexEntry
    {
        public int StandardId { get; set; }

        public bool Published { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string AssessmentPlanPdf { get; set; }

        public string EntryRequirements { get; set; }

        [String(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public IEnumerable<string> JobRoles { get; set; }

        public string OverviewOfRole { get; set; }

        public string ProfessionalRegistration { get; set; }

        public string Qualifications { get; set; }

        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string StandardPdf { get; set; }

        public string WhatApprenticesWillLearn { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        public DateTime? LastDateForNewStarts { get; set; }
        public int StandardSectorCode { get; set; }
    }
}