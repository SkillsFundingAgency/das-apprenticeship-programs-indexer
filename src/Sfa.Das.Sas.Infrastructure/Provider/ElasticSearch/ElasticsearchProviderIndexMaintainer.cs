﻿namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.Core.Models.Provider;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
    using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch;

    public sealed class ElasticsearchProviderIndexMaintainer : ElasticsearchIndexMaintainerBase, IMaintainProviderIndex
    {
        private readonly ILog _log;
        private readonly IElasticsearchConfiguration _elasticsearchConfiguration;
        private readonly Func<DeliveryInformation, bool> _onlyAtEmployer = x => x.DeliveryModes.All(xx => xx == ModesOfDelivery.OneHundredPercentEmployer);
        private readonly Func<DeliveryInformation, bool> _anyNotAtEmployer = x => x.DeliveryModes.Any(xx => xx != ModesOfDelivery.OneHundredPercentEmployer);

        public ElasticsearchProviderIndexMaintainer(
            IElasticsearchCustomClient elasticsearchClient,
            IElasticsearchMapper elasticsearchMapper,
            ILog log,
            IElasticsearchConfiguration elasticsearchConfiguration)
            : base(elasticsearchClient, elasticsearchMapper, log, "Provider")
        {
            _log = log;
            _elasticsearchConfiguration = elasticsearchConfiguration;
        }

        public override void CreateIndex(string indexName)
        {
            Client.CreateIndex(
                indexName,
                i => i
                    .Settings(settings => settings
                        .NumberOfShards(_elasticsearchConfiguration.ProviderIndexShards())
                        .NumberOfReplicas(_elasticsearchConfiguration.ProviderIndexReplicas()))
                    .Mappings(ms => ms
                        .Map<ProviderDocument>(m => m.AutoMap())
                        .Map<ProviderApiDocument>(m => m.AutoMap())
                        .Map<StandardProvider>(m => m.AutoMap())
                        .Map<FrameworkProvider>(m => m.AutoMap())));
        }

        public void IndexProviders(string indexName, ICollection<Provider> entries)
        {
            var providerList = entries.Select(provider => ElasticsearchMapper.CreateProviderDocument(provider)).ToList();

            Client.BulkAll(providerList, indexName);
        }

        public void IndexApiProviders(string indexName, ICollection<Provider> entries)
        {
            var apiProviderList = entries.Select(provider => ElasticsearchMapper.CreateProviderApiDocument(provider)).ToList();

            Client.BulkAll(apiProviderList, indexName);
        }

        public void IndexStandards(string indexName, ICollection<Provider> indexEntries)
        {
            var standardProviderList = new List<StandardProvider>();

            try
            {
                foreach (var provider in indexEntries)
                {
                    foreach (var standard in provider.Standards)
                    {
                        var deliveryLocationsOnly100 = standard.DeliveryLocations
                            .Where(_onlyAtEmployer)
                            .Where(x => x.DeliveryLocation.Address.GeoPoint != null)
                            .ToArray();

                        if (deliveryLocationsOnly100.Any())
                        {
                            standardProviderList.AddRange(deliveryLocationsOnly100.Select(deliveryInformation => ElasticsearchMapper.CreateStandardProviderDocument(provider, standard, deliveryInformation)));
                        }

                        standardProviderList.AddRange(from location in standard.DeliveryLocations.Where(_anyNotAtEmployer) where location.DeliveryLocation.Address.GeoPoint != null select ElasticsearchMapper.CreateStandardProviderDocument(provider, standard, location));
                    }
                }

                Client.BulkAll(standardProviderList, indexName);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Something failed indexing standard providers:" + ex.Message);
                throw;
            }
        }

        public void IndexFrameworks(string indexName, ICollection<Provider> indexEntries)
        {
            var frameworkProviderList = new List<FrameworkProvider>();

            try
            {
                foreach (var provider in indexEntries)
                {
                    foreach (var framework in provider.Frameworks)
                    {
                        var deliveryLocationsOnly100 = framework.DeliveryLocations
                            .Where(_onlyAtEmployer)
                            .Where(x => x.DeliveryLocation.Address.GeoPoint != null)
                            .ToArray();

                        if (deliveryLocationsOnly100.Any())
                        {
                            frameworkProviderList.AddRange(deliveryLocationsOnly100.Select(deliveryInformation => ElasticsearchMapper.CreateFrameworkProviderDocument(provider, framework, deliveryInformation)));
                        }

                        frameworkProviderList.AddRange(from location in framework.DeliveryLocations.Where(_anyNotAtEmployer) where location.DeliveryLocation.Address.GeoPoint != null select ElasticsearchMapper.CreateFrameworkProviderDocument(provider, framework, location));
                    }
                }

                Client.BulkAll(frameworkProviderList.AsEnumerable(), indexName);
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Something failed indexing framework providers:" + ex.Message);
                throw;
            }
        }
    }
}