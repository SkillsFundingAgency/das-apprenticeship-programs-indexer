using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp
{
    public class UkrlpProviderResponse
    {
        public IEnumerable<Provider> MatchingProviderRecords { get; set; }
    }
}