using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Core.Services
{
    using Sfa.Das.Sas.Indexer.Core.Models;

    public interface ISatisfactionRatesProvider
    {
        IEnumerable<SatisfactionRateProvider> GetAllByProvider();
    }
}
