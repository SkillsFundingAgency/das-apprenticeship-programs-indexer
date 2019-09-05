using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.DependencyResolution
{
    using Settings;
    using StructureMap;

    public class SharedApplicationServicesRegistry : Registry
    {
        public SharedApplicationServicesRegistry()
        {
            For<IAppServiceSettings>().Use<AppServiceSettings>();
            For<ITokenService>().Use<TokenService>();
        }
    }
}