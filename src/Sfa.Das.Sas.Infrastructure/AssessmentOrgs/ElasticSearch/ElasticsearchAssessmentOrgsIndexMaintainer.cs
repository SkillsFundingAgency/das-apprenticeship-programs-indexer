namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
    using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;
    using Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;

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
            Client.CreateIndex(indexName, i => i
                .Settings(settings => settings
                    .NumberOfShards(_elasticsearchConfiguration.LarsIndexShards())
                    .NumberOfReplicas(_elasticsearchConfiguration.LarsIndexReplicas()))
                    .Map<AssessmentOrgsDocument>(m => m.AutoMap()));
        }

        public void IndexStandardOrganisationsData(string indexName, List<StandardOrganisationsData> standardOrganisationsData)
        {
            Log.Debug($"Indexing {standardOrganisationsData.Count} StandardOrganisationsData documents");
            IndexEntries(indexName, standardOrganisationsData, ElasticsearchMapper.CreateStandardOrganisationDocument);
        }

        public void IndexOrganisations(string indexName, List<Organisation> organisations)
        {
            Log.Debug($"Indexing {organisations.Count} OrganisationsDocument");
            IndexEntries(indexName, organisations, ElasticsearchMapper.CreateOrganisationDocument);
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