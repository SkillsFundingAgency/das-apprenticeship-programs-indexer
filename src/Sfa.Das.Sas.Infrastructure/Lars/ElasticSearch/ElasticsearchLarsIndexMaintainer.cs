namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Nest;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Exceptions;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models;
    using Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models;

    public sealed class ElasticsearchLarsIndexMaintainer : ElasticsearchIndexMaintainerBase, IMaintainLarsIndex
    {
        private readonly ILog _logger;
        private readonly IElasticsearchConfiguration _elasticsearchConfiguration;

        public ElasticsearchLarsIndexMaintainer(
            IElasticsearchCustomClient elasticsearchClient,
            IElasticsearchMapper elasticsearchMapper,
            ILog logger,
            IElasticsearchConfiguration elasticsearchConfiguration)
            : base(elasticsearchClient, elasticsearchMapper, logger, "Lars")
        {
            _logger = logger;
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

            if (response.ApiCall.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new ConnectionException($"Received non-200 response when trying to create the Lars Index, Status Code:{response.ApiCall.HttpStatusCode}, Message: {response.OriginalException.Message}");
            }
        }

        public async Task IndexStandards(string indexName, IEnumerable<LarsStandard> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateLarsStandardDocument).ConfigureAwait(true);
            }
        }

        public async Task IndexFrameworks(string indexName, IEnumerable<FrameworkMetaData> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateLarsFrameworkDocument).ConfigureAwait(true);
            }
        }

        public async Task IndexFundingMetadata(string indexName, IEnumerable<FundingMetaData> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateFundingMetaDataDocument).ConfigureAwait(true);
            }
        }

        public async Task IndexFrameworkAimMetaData(string indexName, IEnumerable<FrameworkAimMetaData> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateFrameworkAimMetaDataDocument).ConfigureAwait(true);
            }
        }

        public async Task IndexApprenticeshipComponentTypeMetaData(string indexName, IEnumerable<ApprenticeshipComponentTypeMetaData> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateApprenticeshipComponentTypeMetaDataDocument).ConfigureAwait(true);
            }
        }

        public async Task IndexLearningDeliveryMetaData(string indexName, IEnumerable<LearningDeliveryMetaData> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateLearningDeliveryMetaDataDocument).ConfigureAwait(true);
            }
        }

        public async Task IndexApprenticeshipFundingDetails(string indexName, IEnumerable<ApprenticeshipFunding> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateApprenticeshipFundingDocument).ConfigureAwait(true);
            }
        }

        private async Task IndexEntries<T1, T2>(string indexName, IEnumerable<T1> entries, Func<T1, T2> method)
            where T1 : class
            where T2 : class
        {
            var bulkLarsClient = new BulkProviderClient(indexName, Client);

            foreach (var entry in entries)
            {
                try
                {
                    var doc = method(entry);

                    bulkLarsClient.Create<T2>(c => c.Document(doc));
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Error indexing {typeof(T1)}");
                }
            }

            var bulkTasks = new List<Task<IBulkResponse>>();
            bulkTasks.AddRange(bulkLarsClient.GetTasks());
            LogResponse(await Task.WhenAll(bulkTasks), typeof(T1).Name.ToLower(CultureInfo.CurrentCulture));
        }

        private IEnumerable<List<T>> SplitAndReturn<T>(List<T> entries, int size)
        {
            for (int i = 0; i < entries.Count; i = i + size)
            {
                var actualSize = Math.Min(size, entries.Count - i);
                yield return entries.GetRange(i, actualSize);
            }
        }
    }
}