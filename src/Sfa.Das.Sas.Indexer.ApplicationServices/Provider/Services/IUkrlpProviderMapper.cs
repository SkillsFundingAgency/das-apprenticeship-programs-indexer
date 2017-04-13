using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services
{
    public interface IUkrlpProviderMapper
    {
        Core.Models.Provider.Provider Map(Ukrlp.SoapApi.Types.Provider ukrlpProvider);
        Core.Models.Provider.ContactAddress MapAddress(Ukrlp.SoapApi.Types.Contact contact);
    }
}
