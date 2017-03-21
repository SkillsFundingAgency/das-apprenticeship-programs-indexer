using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.ElasticSearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DependencyResolution
{
    public class ApprenticeshipInfrastructureRegistry : Registry
    {
        public ApprenticeshipInfrastructureRegistry()
        {
            For<ILarsSettings>().Use<LarsSettings>();
            For<IMaintainApprenticeshipIndex>().Use<ElasticsearchApprenticeshipIndexMaintainer>();
        }
    }
}