using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.ProviderFeedback
{
    public class ProviderFeedbackResult
    {
        public ProviderFeedbackResult(IEnumerable<EmployerFeedback> employerFeedback)
        {
            EmployerFeedback = employerFeedback;
        }

        public IEnumerable<EmployerFeedback> EmployerFeedback { get; private set; }
    }
}