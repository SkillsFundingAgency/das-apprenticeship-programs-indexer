using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DependencyResolution
{
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using StructureMap;

    public class AssessmentOrgsInfrastructureRegistry : Registry
    {
        public AssessmentOrgsInfrastructureRegistry()
        {
            For<IMaintainAssessmentOrgsIndex>().Use<ElasticsearchAssessmentOrgsIndexMaintainer>();
        }
    }
}