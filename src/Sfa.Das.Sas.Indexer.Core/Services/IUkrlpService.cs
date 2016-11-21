namespace Sfa.Das.Sas.Indexer.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;

    public interface IUkrlpService
    {
        Task<List<Provider>> GetLearnerProviderInformationAsync(string[] ukprns);
    }
}