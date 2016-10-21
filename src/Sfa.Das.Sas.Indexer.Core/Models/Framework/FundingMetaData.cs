﻿using System;

namespace Sfa.Das.Sas.Indexer.Core.Models.Framework
{
    public class FundingMetaData
    {
        public string LearnAimRef { get; set; }
        public string FundingCategory { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public int? RateWeighted { get; set; }
    }
}
