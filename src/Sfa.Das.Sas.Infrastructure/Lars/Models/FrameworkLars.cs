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

        public int FworkCode { get; set; }

        public int ProgType { get; set; }

        public int PwayCode { get; set; }

        public string PathwayName { get; set; }

        public string NasTitle { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public IEnumerable<JobRoleItem> JobRoleItems { get; set; }

        public IEnumerable<string> Keywords { get; set; }

        public double SectorSubjectAreaTier1 { get; set; }

        public double SectorSubjectAreaTier2 { get; set; }

        public string EntryRequirements { get; set; }

        public string ProfessionalRegistration { get; set; }

        public string CompletionQualifications { get; set; }

        public string FrameworkOverview { get; set; }

        public IEnumerable<string> CompetencyQualification { get; set; }

        public IEnumerable<string> KnowledgeQualification { get; set; }

        public IEnumerable<string> CombinedQualification { get; set; }

        public int Duration { get; set; }

        public int FundingCap { get; set; }

        public List<FundingPeriod> FundingPeriods { get; set; }
    }
}
