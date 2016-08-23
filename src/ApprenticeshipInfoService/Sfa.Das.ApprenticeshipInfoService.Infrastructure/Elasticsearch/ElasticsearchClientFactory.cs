﻿using Elasticsearch.Net;
using Nest;
using Sfa.Das.ApprenticeshipInfoService.Core.Configuration;
using Sfa.Das.ApprenticeshipInfoService.Core.Models;

namespace Sfa.Das.ApprenticeshipInfoService.Infrastructure.Elasticsearch
{
    public sealed class ElasticsearchClientFactory : IElasticsearchClientFactory
    {
        private readonly IConfigurationSettings _applicationSettings;

        public ElasticsearchClientFactory(IConfigurationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public IElasticClient Create()
        {
            using (var settings = new ConnectionSettings(new StaticConnectionPool(_applicationSettings.ElasticServerUrls)))
            {
                settings.DisableDirectStreaming();
                settings.MapDefaultTypeNames(d => d.Add(typeof(FrameworkSearchResultsItem), "frameworkdocument"));

                return new ElasticClient(settings);
            }
        }
    }
}
