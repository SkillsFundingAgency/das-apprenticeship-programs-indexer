using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Settings;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Services;
    using StructureMap;

    public class ProviderApplicationServicesRegistry : Registry
    {
        public ProviderApplicationServicesRegistry()
        {
            For<IIndexSettings<IMaintainProviderIndex>>().Use<ProviderIndexSettings>();
            For<IGenericIndexerHelper<IMaintainProviderIndex>>().Use<ProviderIndexer>();
            For<IIndexerService<IMaintainProviderIndex>>().Use<IndexerService<IMaintainProviderIndex>>();
            For<IProviderDataService>().Use<ProviderDataService>();
        }
    }
}