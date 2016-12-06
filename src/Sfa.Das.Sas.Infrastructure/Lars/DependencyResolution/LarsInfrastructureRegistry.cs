namespace Sfa.Das.Sas.Indexer.Infrastructure.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using Sfa.Das.Sas.Indexer.Infrastructure.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using StructureMap;

    public class LarsInfrastructureRegistry : Registry
    {
        public LarsInfrastructureRegistry()
        {
            For<ILarsSettings>().Use<LarsSettings>();
            For<ILogLars>().Use(x => new NLogLarsService(x.ParentType, x.GetInstance<IInfrastructureSettings>())).AlwaysUnique();
            For<IMaintainLarsIndex>().Use<ElasticsearchLarsIndexMaintainer>();
        }
    }
}