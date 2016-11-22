using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.Core.Services;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.DependencyResolution
{
    public class ApplicationServicesRegistry : Registry
    {
        public ApplicationServicesRegistry()
        {
            For<IAppServiceSettings>().Use<AppServiceSettings>();
            For<IIndexerServiceFactory>().Use<IndexerServiceFactory>();

            // Providers
            For<IIndexSettings<IMaintainProviderIndex>>().Use<ProviderIndexSettings>();
            For<IGenericIndexerHelper<IMaintainProviderIndex>>().Use<ProviderIndexer>();
            For<IIndexerService<IMaintainProviderIndex>>().Use<IndexerService<IMaintainProviderIndex>>();
            For<IProviderDataService>().Use<ProviderDataService>();

            // Apprenticeships
            For<IIndexSettings<IMaintainApprenticeshipIndex>>().Use<StandardIndexSettings>();
            For<IMetaDataHelper>().Use<MetaDataHelper>();
            For<IIndexerService<IMaintainApprenticeshipIndex>>().Use<IndexerService<IMaintainApprenticeshipIndex>>();
            For<IGenericIndexerHelper<IMaintainApprenticeshipIndex>>().Use<ApprenticeshipIndexer>();
        }
    }
}