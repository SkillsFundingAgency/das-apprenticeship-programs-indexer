namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration
{
    using Nest;
    using Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models;
    using System;

    public interface IElasticsearchConfiguration
    {
        AnalysisDescriptor ApprenticeshipAnalysisDescriptor();

        int ApprenticeshipIndexShards();

        int ApprenticeshipIndexReplicas();

        int ProviderIndexShards();

        int ProviderIndexReplicas();

        int LarsIndexShards();

        int LarsIndexReplicas();

        Func<TypeMappingDescriptor<StandardDocument>, ITypeMapping> ApprenticeshipsStandardMappingDescriptor();

        Func<TypeMappingDescriptor<FrameworkDocument>, ITypeMapping> ApprenticeshipsFrameworkMappingDescriptor();
    }
}