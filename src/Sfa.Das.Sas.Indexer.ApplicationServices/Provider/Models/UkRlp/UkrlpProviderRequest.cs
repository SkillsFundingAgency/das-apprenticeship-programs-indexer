using System.Collections.Generic;
using MediatR;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp
{
    public class UkrlpProviderRequest : IRequest<UkrlpProviderResponse>
    {
        public UkrlpProviderRequest()
        {
        }

        public UkrlpProviderRequest(IEnumerable<int> result)
        {
            Providers = result;
        }

        public IEnumerable<int> Providers { get; set; }
    }
}