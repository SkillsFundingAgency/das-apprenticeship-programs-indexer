﻿using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models
{
    public sealed class StandardDocument : IApprenticeshipDocument, IIndexEntry
    {
        [String(Analyzer = "english")]
        public string Title { get; set; }
        public int Level { get; set; }
        public string AssessmentPlanPdf { get; set; }

        public string EntryRequirements { get; set; }

        [String(Analyzer = "english")]
        public IEnumerable<string> JobRoles { get; set; }

        [String(Analyzer = "english")]
        public IEnumerable<string> Keywords { get; set; }

        public string OverviewOfRole { get; set; }
        public string ProfessionalRegistration { get; set; }
        public string Qualifications { get; set; }
        public int StandardId { get; set; }
        public string StandardPdf { get; set; }
        public string WhatApprenticesWillLearn { get; set; }
        public TypicalLength TypicalLength { get; set; }
    }
}