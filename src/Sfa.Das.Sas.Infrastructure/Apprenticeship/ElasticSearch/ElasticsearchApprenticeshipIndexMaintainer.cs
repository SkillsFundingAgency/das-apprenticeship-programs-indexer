using System.Linq;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
    using SFA.DAS.NLog.Logger;

    public sealed class ElasticsearchApprenticeshipIndexMaintainer : ElasticsearchIndexMaintainerBase, IMaintainApprenticeshipIndex
    {
        private readonly IElasticsearchConfiguration _elasticsearchConfiguration;

        public ElasticsearchApprenticeshipIndexMaintainer(
            IElasticsearchCustomClient elasticsearchClient,
            IElasticsearchMapper elasticsearchMapper,
            ILog logger,
            IElasticsearchConfiguration elasticsearchConfiguration)
            : base(elasticsearchClient, elasticsearchMapper, logger, "Apprenticeship")
        {
            _elasticsearchConfiguration = elasticsearchConfiguration;
        }

        public override void CreateIndex(string indexName)
        {
            Client.CreateIndex(indexName, i => i
                .Settings(settings => settings
                    .NumberOfShards(_elasticsearchConfiguration.ApprenticeshipIndexShards())
                    .NumberOfReplicas(_elasticsearchConfiguration.ApprenticeshipIndexReplicas())
                    .Analysis(a => _elasticsearchConfiguration.ApprenticeshipAnalysisDescriptor()))
                .Map(_elasticsearchConfiguration.ApprenticeshipMappingDescriptor()));
        }

        public void IndexStandards(string indexName, IEnumerable<StandardMetaData> entries)
        {
            IndexApprenticeships(indexName, entries, ElasticsearchMapper.CreateStandardDocument);
        }

        public void IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> entries)
        {
            IndexApprenticeships(indexName, entries, ElasticsearchMapper.CreateFrameworkDocument);
        }

        private void IndexApprenticeships<T1, T2>(string indexName, IEnumerable<T1> entries, Func<T1, T2> method)
            where T1 : class
            where T2 : class
        {
            var entriesList = entries.Select(method).ToList();

            Client.BulkAllGeneric(entriesList, indexName);
        }
    }
}