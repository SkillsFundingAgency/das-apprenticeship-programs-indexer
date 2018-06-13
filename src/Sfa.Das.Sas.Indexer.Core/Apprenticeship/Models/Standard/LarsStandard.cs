using System;

namespace Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard
{
    public sealed class LarsStandard
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int NotionalEndLevel { get; set; }
        public double SectorSubjectAreaTier1 { get; set; }

        public double SectorSubjectAreaTier2 { get; set; }

        public int Duration { get; set; }

        public int FundingCap { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public int StandardSectorCode { get; set; }

        public bool IsValidDate(DateTime currentDate)
        {
            return EffectiveFrom != null && EffectiveFrom.Value.Date <= currentDate.Date && (EffectiveTo == null || EffectiveTo.Value.Date >= currentDate.Date);
        }
    }
}
