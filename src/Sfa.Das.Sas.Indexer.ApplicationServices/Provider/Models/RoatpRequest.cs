using System.Collections.Generic;
using MediatR;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models
{
    public class RoatpRequest : IAsyncRequest<List<RoatpProviderResult>>, IRequest<List<RoatpProviderResult>>
    {
    }
}
