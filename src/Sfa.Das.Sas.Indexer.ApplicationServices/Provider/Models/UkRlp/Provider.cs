using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp
{
    public class Provider
    {
        public string UnitedKingdomProviderReferenceNumber { get; set; }
        public string ProviderName { get; set; }
        public IEnumerable<ProviderContact> ProviderContact { get; set; }
    }
}