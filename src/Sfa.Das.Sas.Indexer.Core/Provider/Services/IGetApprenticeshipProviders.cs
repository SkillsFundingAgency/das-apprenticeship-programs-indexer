using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Core.Services
{
    public interface IGetApprenticeshipProviders
    {
        EmployerProviderResult GetEmployerProviders();

        HeiProvidersResult GetHeiProviders();
    }
}