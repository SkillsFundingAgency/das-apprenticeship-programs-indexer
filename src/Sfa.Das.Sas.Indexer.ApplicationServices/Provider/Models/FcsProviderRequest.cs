using MediatR;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models
{
    public class FcsProviderRequest : IAsyncRequest<FcsProviderResult>, IRequest<FcsProviderResult>
    {
    }
}