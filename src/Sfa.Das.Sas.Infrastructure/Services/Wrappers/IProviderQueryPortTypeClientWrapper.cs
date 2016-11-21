namespace Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers
{
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.Infrastructure.Uklrp;

    public interface IProviderQueryPortTypeClientWrapper
    {
        Task<response> RetrieveAllProvidersAsync(ProviderQueryStructure providerQueryStructure);
    }
}
