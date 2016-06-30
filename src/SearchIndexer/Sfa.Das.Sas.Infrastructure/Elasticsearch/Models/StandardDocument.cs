﻿using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models
{
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

    public sealed class StandardDocument : ApprenticeshipDocument, IIndexEntry
    {

        public string AssessmentPlanPdf { get; set; }

        public string EntryRequirements { get; set; }

        [String(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public IEnumerable<string> JobRoles { get; set; }

        [String(Analyzer = ElasticsearchConfiguration.AnalyserEnglishCustom)]
        public IEnumerable<string> Keywords { get; set; }

        public string OverviewOfRole { get; set; }

        public string ProfessionalRegistration { get; set; }

        public string Qualifications { get; set; }

        public int StandardId { get; set; }

        public string StandardPdf { get; set; }

        public string WhatApprenticesWillLearn { get; set; }
    }
}