using System;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class StandardLars
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int NotionalEndLevel { get; set; }

        public string StandardUrl { get; set; }

        public double SectorSubjectAreaTier1 { get; set; }

        public double SectorSubjectAreaTier2 { get; set; }

        public int Duration { get; set; }

        public int FundingCap { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}
