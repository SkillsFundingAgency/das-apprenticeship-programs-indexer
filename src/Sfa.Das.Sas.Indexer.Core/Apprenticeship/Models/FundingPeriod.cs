using System;

namespace Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models
{
    public class FundingPeriod
    {
        public int FundingCap { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}
