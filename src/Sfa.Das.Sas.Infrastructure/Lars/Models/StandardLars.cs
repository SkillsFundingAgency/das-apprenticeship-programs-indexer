using System;
using System.Collections.Generic;
using Nest;
using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class StandardLars : LarsDocument
    {
        public StandardLars()
            : base(nameof(StandardLars))
        {
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int StandardSectorCode { get; set; }

        public int NotionalEndLevel { get; set; }

        public double SectorSubjectAreaTier1 { get; set; }

        public double SectorSubjectAreaTier2 { get; set; }

        public int Duration { get; set; }

        public int FundingCap { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public List<FundingPeriod> FundingPeriods { get; set; }

        public DateTime? LastDateForNewStarts { get; set; }

		public bool RegulatedStandard { get; set; }
    }
}
