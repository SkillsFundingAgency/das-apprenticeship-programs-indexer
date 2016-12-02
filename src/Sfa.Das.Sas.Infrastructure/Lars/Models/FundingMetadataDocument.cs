using System;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class FundingMetadataDocument
    {
        public string LearnAimRef { get; set; }

        public string FundingCategory { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public int? RateWeighted { get; set; }
    }
}
