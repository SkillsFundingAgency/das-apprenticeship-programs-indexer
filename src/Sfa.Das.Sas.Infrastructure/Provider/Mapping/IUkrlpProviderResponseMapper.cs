using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Mapping
{
    public interface IUkrlpProviderResponseMapper
    {
        ApplicationServices.Provider.Models.UkRlp.Provider MapFromUkrlpProviderRecord(ProviderRecordStructure record);
    }
}