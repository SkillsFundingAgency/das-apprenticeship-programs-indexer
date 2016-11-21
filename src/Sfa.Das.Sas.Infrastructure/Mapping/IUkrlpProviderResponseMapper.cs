using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Infrastructure.Uklrp;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Mapping
{
    public interface IUkrlpProviderResponseMapper
    {
        Provider MapFromUkrlpProviderRecord(ProviderRecordStructure record);
    }
}