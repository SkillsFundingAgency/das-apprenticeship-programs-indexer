namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    using System;

    public class FundingDocument
    {
        public string LearnAimRef { get; set; }

        public string FundingCategory { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public int? RateWeighted { get; set; }
    }
}
