using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.ProviderFeedback
{
    public class ProviderFeedbackResult
    {
        public ProviderFeedbackResult(IEnumerable<EmployerFeedbackSourceDto> employerFeedback)
        {
            EmployerFeedback = employerFeedback;
        }

        public IEnumerable<EmployerFeedbackSourceDto> EmployerFeedback { get; private set; }
    }
}