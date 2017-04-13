using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp
{
    using System.Collections.Generic;

    public class UkrlpProviderResponse
    {
        public IEnumerable<Ukrlp.SoapApi.Types.Provider> MatchingProviderRecords { get; set; } = new List<Ukrlp.SoapApi.Types.Provider>();
    }
}