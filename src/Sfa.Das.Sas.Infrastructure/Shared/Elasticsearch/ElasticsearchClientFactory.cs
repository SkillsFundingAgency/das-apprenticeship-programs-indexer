using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;
using FeatureToggle.Core.Fluent;
using Nest;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.Infrastructure.Extensions;
using Sfa.Das.Sas.Indexer.Infrastructure.FeatureToggles;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    public class ElasticsearchClientFactory : IElasticsearchClientFactory
    {
        private readonly IInfrastructureSettings _infrastructureSettings;
        private readonly ILog _logger;

        public ElasticsearchClientFactory(IInfrastructureSettings infrastructureSettings, ILog logger)
        {
            _infrastructureSettings = infrastructureSettings;
            _logger = logger;
        }

        public IElasticClient GetElasticClient()
        {
            ConnectionSettings settings;
            if (Is<IgnoreSslCertificateFeature>.Enabled)
            {
                settings = new ConnectionSettings(
                    new SingleNodeConnectionPool(_infrastructureSettings.ElasticServerUrls.FirstOrDefault()),
                    new MyCertificateIgnoringHttpConnection());
            }
            else
            {
                settings = new ConnectionSettings(
                    new SingleNodeConnectionPool(_infrastructureSettings.ElasticServerUrls.FirstOrDefault()));
            }

            if (!Debugger.IsAttached || !Is<IgnoreSslCertificateFeature>.Enabled)
            {
                settings.BasicAuthentication(_infrastructureSettings.ElasticsearchUsername, _infrastructureSettings.ElasticsearchPassword);
            }

            settings.OnRequestCompleted(r =>
            {
                _logger.Debug(r.DebugInformation);
            });

            var client = new ElasticClient(settings);
            return client;
        }
    }
}