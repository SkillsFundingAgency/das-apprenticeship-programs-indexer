using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;

    public interface IUkrlpService
    {
        UkrlpProviderResponse Handle(UkrlpProviderRequest request);
    }
}