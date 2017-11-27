namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Threading.Tasks;
    using ApplicationServices.AssessmentOrgs.Services;
    using Core.AssessmentOrgs.Models;
    using Core.Exceptions;
    using Elasticsearch;
    using Elasticsearch.Configuration;
    using Models;
    using Nest;
    using SFA.DAS.NLog.Logger;

    public sealed class ElasticsearchAssessmentOrgsIndexMaintainer : ElasticsearchIndexMaintainerBase, IMaintainAssessmentOrgsIndex
    {
        private readonly IElasticsearchConfiguration _elasticsearchConfiguration;

        public ElasticsearchAssessmentOrgsIndexMaintainer(
            IElasticsearchCustomClient elasticsearchClient,
            IElasticsearchMapper elasticsearchMapper,
            ILog logger,
            IElasticsearchConfiguration elasticsearchConfiguration)
            : base(elasticsearchClient, elasticsearchMapper, logger, "AssessmentOrgs")
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
                    .Map<OrganisationDocument>(m => m.AutoMap())
                    .Map<StandardOrganisationDocument>(m => m.AutoMap())));

            if (response.ApiCall.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new ConnectionException($"Received non-200 response when trying to create the Assessment Organisations Index, Status Code:{response.ApiCall.HttpStatusCode}, Message: {response.OriginalException.Message}");
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

        private async Task IndexEntries<T1, T2>(string indexName, IEnumerable<T1> entries, Func<T1, T2> method)
            where T1 : class
            where T2 : class
        {
            var bulkClient = new BulkProviderClient(indexName, Client);

            foreach (var entry in entries)
            {
                try
                {
                    var doc = method(entry);

                    bulkClient.Create<T2>(c => c.Document(doc));
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Error indexing {typeof(T1)}");
                }
            }

            var bulkTasks = new List<Task<IBulkResponse>>();
            bulkTasks.AddRange(bulkClient.GetTasks());
            LogResponse(await Task.WhenAll(bulkTasks), typeof(T1).Name.ToLower(CultureInfo.CurrentCulture));
        }
    }
}