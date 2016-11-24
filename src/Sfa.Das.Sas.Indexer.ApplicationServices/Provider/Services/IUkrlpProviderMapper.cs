using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IUkrlpProviderMapper
    {
        Core.Models.Provider.Provider Map(ApplicationServices.Provider.Models.UkRlp.Provider ukrlpProvider);
        Core.Models.Provider.ContactAddress MapAddress(ProviderContact contact);
    }
}
