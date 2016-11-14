namespace Sfa.Das.Sas.Indexer.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;

    public interface IGetApprenticeshipProviders
    {
        Task<IEnumerable<Provider>> GetApprenticeshipProvidersAsync();

        ICollection<string> GetEmployerProviders();

        ICollection<string> GetHeiProviders();
    }
}