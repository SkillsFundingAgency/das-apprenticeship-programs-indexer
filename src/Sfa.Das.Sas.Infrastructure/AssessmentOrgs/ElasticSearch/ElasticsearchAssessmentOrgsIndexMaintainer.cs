using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models;

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
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
    using Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models;

    public sealed class ElasticsearchAssessmentOrgsIndexMaintainer : ElasticsearchIndexMaintainerBase, IMaintainAssessmentOrgsIndex
    {
        private readonly ILog _logger;
        private readonly IElasticsearchConfiguration _elasticsearchConfiguration;

        public ElasticsearchAssessmentOrgsIndexMaintainer(
            IElasticsearchCustomClient elasticsearchClient,
            IElasticsearchMapper elasticsearchMapper,
            ILog logger,
            IElasticsearchConfiguration elasticsearchConfiguration)
            : base(elasticsearchClient, elasticsearchMapper, logger, "AssessmentOrgs")
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
                    .Map<OrganisationDocument>(m => m.AutoMap())
                    .Map<StandardOrganisationsData>(m => m.AutoMap())));

            if (response.ApiCall.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new ConnectionException($"Received non-200 response when trying to create the Assessment Organisations Index, Status Code:{response.ApiCall.HttpStatusCode}, Message: {response.OriginalException.Message}");
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

        public async Task IndexStandardOrganisationsData(string indexName, List<StandardOrganisationsData> standardOrganisationsData)
        {
            Log.Debug($"Indexing {standardOrganisationsData.Count} StandardOrganisationsData documents");
            await IndexEntries(indexName, standardOrganisationsData, ElasticsearchMapper.CreateStandardOrganisationDocument).ConfigureAwait(true);
        }

        public async Task IndexOrganisations(string indexName, List<Organisation> organisations)
        {
            Log.Debug($"Indexing {organisations.Count} OrganisationsDocument");
            await IndexEntries(indexName, organisations, ElasticsearchMapper.CreateOrganisationDocument).ConfigureAwait(true);
        }
    }
}