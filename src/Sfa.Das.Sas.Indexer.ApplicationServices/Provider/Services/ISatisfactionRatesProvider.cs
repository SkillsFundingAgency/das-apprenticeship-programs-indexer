using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Core.Services
{
    using Sfa.Das.Sas.Indexer.Core.Models;

    public interface ISatisfactionRatesProvider
    {
        EmployerSatisfactionRateResult GetAllEmployerSatisfactionByProvider();
        LearnerSatisfactionRateResult GetAllLearnerSatisfactionByProvider();
    }
}
