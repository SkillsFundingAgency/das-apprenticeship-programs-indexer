namespace Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers
{
    using System.Threading.Tasks;
    using Ukrlp;

    public interface IProviderQueryPortTypeClientWrapper
    {
        Task<response> RetrieveAllProvidersAsync(ProviderQueryStructure providerQueryStructure);
    }
}
