using System;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models.Provider
{
    public class ProviderStandard
    {
        public int StandardId { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
    }
}