using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    using System;

    public class FundingDocument
    {
        [Keyword(NullValue = "null")]
        public string LearnAimRef { get; set; }

        [Keyword(NullValue = "null")]
        public string FundingCategory { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime? EffectiveFrom { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime? EffectiveTo { get; set; }

        [Keyword(NullValue = "null")]
        public int? RateWeighted { get; set; }
    }
}
