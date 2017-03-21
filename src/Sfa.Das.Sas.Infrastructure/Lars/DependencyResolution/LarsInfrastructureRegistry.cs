using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Lars.ElasticSearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.DependencyResolution
{
    public class LarsInfrastructureRegistry : Registry
    {
        public LarsInfrastructureRegistry()
        {
            For<ILarsSettings>().Use<LarsSettings>();
            For<IMaintainLarsIndex>().Use<ElasticsearchLarsIndexMaintainer>();
        }
    }
}