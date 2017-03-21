namespace Sfa.Das.Sas.Indexer.Core.Provider.Models
{
    using System.Collections.Generic;
    using MediatR;

    public class RoatpProviderRequest : IAsyncRequest<List<RoatpProviderResult>>, IRequest<List<RoatpProviderResult>>
    {
    }
}