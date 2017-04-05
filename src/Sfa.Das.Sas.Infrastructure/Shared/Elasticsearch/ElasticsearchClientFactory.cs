using Elasticsearch.Net;
using Nest;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    public class ElasticsearchClientFactory : IElasticsearchClientFactory
    {
        private readonly IInfrastructureSettings _infrastructureSettings;

        public ElasticsearchClientFactory(IInfrastructureSettings infrastructureSettings)
        {
            _infrastructureSettings = infrastructureSettings;
        }

        public IElasticClient GetElasticClient()
        {
            using (var settings = new ConnectionSettings(new StaticConnectionPool(_infrastructureSettings.ElasticServerUrls)))
            {
                settings.BasicAuthentication(_infrastructureSettings.ElasticsearchUsername, _infrastructureSettings.ElasticsearchPassword);
                return new ElasticClient(settings);
            }
        }
    }
}