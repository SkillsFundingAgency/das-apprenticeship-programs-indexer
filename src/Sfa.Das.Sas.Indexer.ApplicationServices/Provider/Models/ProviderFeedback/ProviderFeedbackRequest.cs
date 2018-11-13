using System.Collections.Generic;
using MediatR;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.ProviderFeedback
{
    public class ProviderFeedbackRequest : IAsyncRequest<ProviderFeedbackResult>, IRequest<ProviderFeedbackResult>
    {
    }
}
