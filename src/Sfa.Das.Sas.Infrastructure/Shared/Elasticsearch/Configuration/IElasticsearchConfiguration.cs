namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration
{
    using Nest;

    public interface IElasticsearchConfiguration
    {
        AnalysisDescriptor ApprenticeshipAnalysisDescriptor();

        int ApprenticeshipIndexShards();

        int ApprenticeshipIndexReplicas();

        int ProviderIndexShards();

        int ProviderIndexReplicas();

        int LarsIndexShards();

        int LarsIndexReplicas();

        MappingsDescriptor ApprenticeshipMappingDescriptor();
    }
}