using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Mapping
{
    public interface IUkrlpProviderWrapperMapper
    {
        Provider MapFromUkrlpProviderRecord(ProviderRecordStructure record);
    }
}