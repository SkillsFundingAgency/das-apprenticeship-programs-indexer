namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;

    public interface IUkrlpService
    {
        UkrlpProviderResponse GetProviders(IEnumerable<int> ukprns);
    }
}