using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Settings;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.Core.Services;
    using StructureMap;

    public class LarsApplicationServicesRegistry : Registry
    {
        public LarsApplicationServicesRegistry()
        {
            For<IIndexSettings<IMaintainLarsIndex>>().Use<LarsIndexSettings>();
            For<IMetaDataHelper>().Use<MetaDataHelper>();
            For<IIndexerService<IMaintainLarsIndex>>().Use<IndexerService<IMaintainLarsIndex>>();
            For<IGenericIndexerHelper<IMaintainLarsIndex>>().Use<LarsIndexer>();
        }
    }
}