namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
    using Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models;

    public sealed class ElasticsearchLarsIndexMaintainer : ElasticsearchIndexMaintainerBase, IMaintainLarsIndex
    {
        private readonly IElasticsearchConfiguration _elasticsearchConfiguration;

        public ElasticsearchLarsIndexMaintainer(
            IElasticsearchCustomClient elasticsearchClient,
            IElasticsearchMapper elasticsearchMapper,
            ILog logger,
            IElasticsearchConfiguration elasticsearchConfiguration)
            : base(elasticsearchClient, elasticsearchMapper, logger, "Lars")
        {
            _elasticsearchConfiguration = elasticsearchConfiguration;
        }

        public override void CreateIndex(string indexName)
        {
            var response = Client.CreateIndex(indexName, i => i
                .Settings(settings => settings
                    .NumberOfShards(_elasticsearchConfiguration.LarsIndexShards())
                    .NumberOfReplicas(_elasticsearchConfiguration.LarsIndexReplicas()))
                .Mappings(ms => ms
                    .Map<ApprenticeshipFundingDocument>(m => m.AutoMap())
                    .Map<LearningDeliveryDocument>(m => m.AutoMap())
                    .Map<FundingDocument>(m => m.AutoMap())
                    .Map<FrameworkAimDocument>(m => m.AutoMap())
                    .Map<FrameworkLars>(m => m.AutoMap())
                    .Map<StandardLars>(m => m.AutoMap())
                    .Map<ApprenticeshipComponentTypeDocument>(m => m.AutoMap())));
        }

        public void IndexStandards(string indexName, IEnumerable<LarsStandard> entries)
        {
            IndexEntries(indexName, entries, ElasticsearchMapper.CreateLarsStandardDocument);
        }

        public void IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> entries)
        {
            IndexEntries(indexName, entries, ElasticsearchMapper.CreateLarsFrameworkDocument);
        }

        public void IndexFundingMetadata(string indexName, IEnumerable<FundingMetaData> entries)
        {
            IndexEntries(indexName, entries, ElasticsearchMapper.CreateFundingMetaDataDocument);
        }

        public void IndexFrameworkAimMetaData(string indexName, IEnumerable<FrameworkAimMetaData> entries)
        {
            IndexEntries(indexName, entries, ElasticsearchMapper.CreateFrameworkAimMetaDataDocument);
        }

        public void IndexApprenticeshipComponentTypeMetaData(string indexName, IEnumerable<ApprenticeshipComponentTypeMetaData> entries)
        {
            IndexEntries(indexName, entries, ElasticsearchMapper.CreateApprenticeshipComponentTypeMetaDataDocument);
        }

        public void IndexLearningDeliveryMetaData(string indexName, IEnumerable<LearningDeliveryMetaData> entries)
        {
            IndexEntries(indexName, entries, ElasticsearchMapper.CreateLearningDeliveryMetaDataDocument);
        }

        public void IndexApprenticeshipFundingDetails(string indexName, IEnumerable<ApprenticeshipFundingMetaData> entries)
        {
            IndexEntries(indexName, entries, ElasticsearchMapper.CreateApprenticeshipFundingDocument);
        }

        private void IndexEntries<T1, T2>(string indexName, IEnumerable<T1> entries, Func<T1, T2> method)
            where T1 : class
            where T2 : class
        {
            var entriesList = entries.Select(method).ToList();

            Client.BulkAllGeneric(entriesList, indexName);
        }
    }
}