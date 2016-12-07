using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DependencyResolution
{
    public class ApprenticeshipInfrastructureRegistry : Registry
    {
        public ApprenticeshipInfrastructureRegistry()
        {
            For<ILarsSettings>().Use<LarsSettings>();
            For<ILogApprenticeships>().Use(x => new NLogApprenticeshipService(x.ParentType, x.GetInstance<IInfrastructureSettings>())).AlwaysUnique();
            For<IMaintainApprenticeshipIndex>().Use<ElasticsearchApprenticeshipIndexMaintainer>();
        }
    }
}