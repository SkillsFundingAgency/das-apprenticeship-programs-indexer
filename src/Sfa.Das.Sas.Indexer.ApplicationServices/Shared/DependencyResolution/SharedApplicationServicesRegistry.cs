namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using StructureMap;

    public class SharedApplicationServicesRegistry : Registry
    {
        public SharedApplicationServicesRegistry()
        {
            For<IAppServiceSettings>().Use<AppServiceSettings>();
            For<IIndexerServiceFactory>().Use<IndexerServiceFactory>();
        }
    }
}