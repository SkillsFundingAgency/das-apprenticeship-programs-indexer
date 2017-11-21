using System;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models.Provider
{
    public class ProviderFramework
    {
        public string FrameworkId { get; set; }
        public int FworkCode { get; set; }
        public int ProgType { get; set; }
        public int PwayCode { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string PathwayName { get; set; }
    }
}
