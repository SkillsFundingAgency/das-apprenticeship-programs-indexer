using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Services;
    using StructureMap;

    public class ApprenticeshipApplicationServicesRegistry : Registry
    {
        public ApprenticeshipApplicationServicesRegistry()
        {
            For<IIndexSettings<IMaintainApprenticeshipIndex>>().Use<StandardIndexSettings>();
            For<IMetaDataHelper>().Use<MetaDataHelper>();
            For<IIndexerService<IMaintainApprenticeshipIndex>>().Use<IndexerService<IMaintainApprenticeshipIndex>>();
            For<IGenericIndexerHelper<IMaintainApprenticeshipIndex>>().Use<ApprenticeshipIndexer>();
        }
    }
}