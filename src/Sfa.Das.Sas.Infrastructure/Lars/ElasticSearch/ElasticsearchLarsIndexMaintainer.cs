using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;
    using Nest;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.Core.Exceptions;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models;

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
                    .NumberOfShards(_elasticsearchConfiguration.ApprenticeshipIndexShards())
                    .NumberOfReplicas(_elasticsearchConfiguration.ApprenticeshipIndexReplicas())
                    .Analysis(a => _elasticsearchConfiguration.ApprenticeshipAnalysisDescriptor()))
                .Mappings(ms => ms
                    .Map<StandardDocument>(m => m.AutoMap())
                    .Map<FrameworkDocument>(m => m.AutoMap())));

            if (response.ApiCall.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new ConnectionException($"Received non-200 response when trying to create the Apprenticeship Index, Status Code:{response.ApiCall.HttpStatusCode}");
            }
        }

        public async Task IndexStandards(string indexName, IEnumerable<LarsStandard> entries)
        {
            await IndexEntries(indexName, entries, ElasticsearchMapper.CreateLarsStandardDocument).ConfigureAwait(true);
        }

        public async Task IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> entries)
        {
            await IndexEntries(indexName, entries, ElasticsearchMapper.CreateLarsFrameworkDocument).ConfigureAwait(true);
        }

        public async Task IndexFundingMetadata(string indexName, IEnumerable<FundingMetaData> entries)
        {
            await IndexEntries(indexName, entries, ElasticsearchMapper.CreateFundingMetaDataDocument).ConfigureAwait(true);
        }

        public async Task IndexFrameworkAimMetaData(string indexName, IEnumerable<FrameworkAimMetaData> entries)
        {
            await IndexEntries(indexName, entries, ElasticsearchMapper.CreateFrameworkAimMetaDataDocument).ConfigureAwait(true);
        }

        public async Task IndexApprenticeshipComponentTypeMetaData(string indexName, IEnumerable<ApprenticeshipComponentTypeMetaData> entries)
        {
            await IndexEntries(indexName, entries, ElasticsearchMapper.CreateApprenticeshipComponentTypeMetaDataDocument).ConfigureAwait(true);
        }

        public async Task IndexLearningDeliveryMetaData(string indexName, IEnumerable<LearningDeliveryMetaData> entries)
        {
            await IndexEntries(indexName, entries, ElasticsearchMapper.CreateLearningDeliveryMetaDataDocument).ConfigureAwait(true);
        }

        private async Task IndexEntries<T1, T2>(string indexName, IEnumerable<T1> entries, Func<T1, T2> method)
            where T1 : class
            where T2 : class
        {
            var bulkProviderClient = new BulkProviderClient(indexName, Client);

            foreach (var entry in entries)
            {
                try
                {
                    var doc = method(entry);

                    bulkProviderClient.Create<T2>(c => c.Document(doc));
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Error indexing {typeof(T1)}");
                }
            }

            var bulkTasks = new List<Task<IBulkResponse>>();
            bulkTasks.AddRange(bulkProviderClient.GetTasks());
            LogResponse(await Task.WhenAll(bulkTasks), typeof(T1).Name.ToLower(CultureInfo.CurrentCulture));
        }
    }
}