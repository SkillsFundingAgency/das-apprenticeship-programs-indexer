using Nest;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    using System;
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Models;

    public class FrameworkLars : LarsDocument
    {
        public FrameworkLars()
            : base(nameof(FrameworkLars))
        {
        }

        [Keyword(NullValue = "null")]
        public int FworkCode { get; set; }

        [Keyword(NullValue = "null")]
        public int ProgType { get; set; }

        [Keyword(NullValue = "null")]
        public int PwayCode { get; set; }

        [Keyword(NullValue = "null")]
        public string PathwayName { get; set; }

        [Keyword(NullValue = "null")]
        public string NasTitle { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime EffectiveFrom { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime? EffectiveTo { get; set; }

        [Keyword(NullValue = "null")]
        public IEnumerable<JobRoleItem> JobRoleItems { get; set; }

        [Keyword(NullValue = "null")]
        public IEnumerable<string> Keywords { get; set; }

        [Keyword(NullValue = "null")]
        public double SectorSubjectAreaTier1 { get; set; }

        [Keyword(NullValue = "null")]
        public double SectorSubjectAreaTier2 { get; set; }

        [Keyword(NullValue = "null")]
        public string EntryRequirements { get; set; }

        [Keyword(NullValue = "null")]
        public string ProfessionalRegistration { get; set; }

        [Keyword(NullValue = "null")]
        public string CompletionQualifications { get; set; }

        [Keyword(NullValue = "null")]
        public string FrameworkOverview { get; set; }

        [Keyword(NullValue = "null")]
        public IEnumerable<string> CompetencyQualification { get; set; }

        [Keyword(NullValue = "null")]
        public IEnumerable<string> KnowledgeQualification { get; set; }

        [Keyword(NullValue = "null")]
        public IEnumerable<string> CombinedQualification { get; set; }

        [Keyword(NullValue = "null")]
        public int Duration { get; set; }

        [Keyword(NullValue = "null")]
        public int FundingCap { get; set; }

        public List<FundingPeriod> FundingPeriods { get; set; }
    }
}
