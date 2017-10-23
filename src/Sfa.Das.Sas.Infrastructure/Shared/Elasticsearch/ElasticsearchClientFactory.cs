using System;
using Elasticsearch.Net;
using FeatureToggle.Core.Fluent;
using Nest;
using Sfa.Das.Sas.Indexer.Infrastructure.Extensions;
using Sfa.Das.Sas.Indexer.Infrastructure.FeatureToggles;
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
            ConnectionSettings settings;
            if (Is<IgnoreSslCertificateFeature>.Enabled)
            {
                settings = new ConnectionSettings(
                    new StaticConnectionPool(_infrastructureSettings.ElasticServerUrls),
                    new MyCertificateIgnoringHttpConnection());
            }
            else
            {
                settings = new ConnectionSettings(
                    new StaticConnectionPool(_infrastructureSettings.ElasticServerUrls));
            }

            settings.BasicAuthentication(_infrastructureSettings.ElasticsearchUsername, _infrastructureSettings.ElasticsearchPassword);
            settings.DisableDirectStreaming();
            settings.MaximumRetries(40);
            settings.RequestTimeout(TimeSpan.FromMinutes(4));

            var client = new ElasticClient(settings);
            return client;
        }
    }
}