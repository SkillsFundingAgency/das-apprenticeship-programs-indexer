using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.ElasticSearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
using StructureMap;

namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.DependencyResolution
{
    public class AssessmentOrgsInfrastructureRegistry : Registry
    {
        public AssessmentOrgsInfrastructureRegistry()
        {
            For<IMaintainAssessmentOrgsIndex>().Use<ElasticsearchAssessmentOrgsIndexMaintainer>();
        }
    }
}