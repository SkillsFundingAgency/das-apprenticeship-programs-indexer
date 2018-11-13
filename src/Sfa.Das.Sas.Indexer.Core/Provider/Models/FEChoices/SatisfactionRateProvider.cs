namespace Sfa.Das.Sas.Indexer.Core.Models
{
    public class SatisfactionRateProvider
    {
        public long Ukprn { get; set; }

        public double? FinalScore { get; set; }

        public int TotalCount { get; set; }

        public int ResponseCount { get; set; }
    }
}
