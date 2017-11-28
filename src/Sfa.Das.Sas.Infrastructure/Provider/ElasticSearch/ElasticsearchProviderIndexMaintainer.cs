﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Exceptions;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.ElasticSearch
{
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
            var response = Client.CreateIndex(
                indexName,
                i => i
                    .Settings(settings => settings
                        .NumberOfShards(_elasticsearchConfiguration.ProviderIndexShards())
                        .NumberOfReplicas(_elasticsearchConfiguration.ProviderIndexReplicas()))
                    .Mappings(ms => ms
                        .Map<StandardProvider>(m => m.AutoMap())
                        .Map<FrameworkProvider>(m => m.AutoMap())));

            if (response.ApiCall.HttpStatusCode != (int)HttpStatusCode.OK)
            {
                throw new ConnectionException($"Received non-200 response when trying to create the Apprenticeship Provider Index, Status Code:{response.ApiCall.HttpStatusCode}");
            }
        }

        public override bool IndexContainsDocuments(string indexName)
        {
            var providerDocuments = Client.Search<ProviderDocument>(s => s.Index(indexName).Size(0).MatchAll());
            var providerApiDocuments = Client.Search<ProviderApiDocument>(s => s.Index(indexName).Size(0).MatchAll());
            var standardProviderDocuments = Client.Search<StandardProvider>(s => s.Index(indexName).Size(0).MatchAll());
            var frameworkProviderDocuments = Client.Search<FrameworkProvider>(s => s.Index(indexName).Size(0).MatchAll());

            return providerDocuments.HitsMetaData.Total > 0
                   && providerApiDocuments.HitsMetaData.Total > 0
                   && standardProviderDocuments.HitsMetaData.Total > 10000
                   && frameworkProviderDocuments.HitsMetaData.Total > 100000;
        }

        public async Task IndexEntries(string indexName, ICollection<Core.Models.Provider.Provider> indexEntries)
        {
            var bulkStandardTasks = new List<Task<IBulkResponse>>();
            var bulkFrameworkTasks = new List<Task<IBulkResponse>>();
            var bulkProviderTasks = new List<Task<IBulkResponse>>();

            bulkStandardTasks.AddRange(IndexStandards(indexName, indexEntries));
            bulkFrameworkTasks.AddRange(IndexFrameworks(indexName, indexEntries));
            bulkProviderTasks.AddRange(IndexProviders(indexName, indexEntries));

            LogResponse(await Task.WhenAll(bulkStandardTasks), "StandardProvider");
            LogResponse(await Task.WhenAll(bulkFrameworkTasks), "FrameworkProvider");
            LogResponse(await Task.WhenAll(bulkProviderTasks), "ProviderDocument");
        }

        public List<Task<IBulkResponse>> IndexFrameworks(string indexName, ICollection<Core.Models.Provider.Provider> indexEntries)
        {
            var bulkProviderLocation = new BulkProviderClient(indexName, Client);

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
                            foreach (var deliveryInformation in deliveryLocationsOnly100)
                            {
                                var frameworkProvider = ElasticsearchMapper.CreateFrameworkProviderDocument(provider, framework, deliveryInformation);
                                bulkProviderLocation.Create<FrameworkProvider>(c => c.Document(frameworkProvider));
                            }
                        }

                        foreach (var location in framework.DeliveryLocations.Where(_anyNotAtEmployer))
                        {
                            if (location.DeliveryLocation.Address.GeoPoint != null)
                            {
                                var frameworkProvider = ElasticsearchMapper.CreateFrameworkProviderDocument(provider, framework, location);
                                bulkProviderLocation.Create<FrameworkProvider>(c => c.Document(frameworkProvider));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Something failed indexing framework providers:" + ex.Message);
                throw;
            }

            return bulkProviderLocation.GetTasks();
        }

        public List<Task<IBulkResponse>> IndexProviders(string indexName, ICollection<Core.Models.Provider.Provider> indexEntries)
        {
            var bulkProviderLocation = new BulkProviderClient(indexName, Client);
            try
            {
                foreach (var provider in indexEntries)
                {
                    var mappedProvider = ElasticsearchMapper.CreateProviderDocument(provider);
                    bulkProviderLocation.Create<ProviderDocument>(c => c.Document(mappedProvider));
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Something failed indexing provider documents:" + ex.Message);
                throw;
            }

            return bulkProviderLocation.GetTasks();
        }

        public List<Task<IBulkResponse>> IndexApiProviders(string indexName, ICollection<Core.Models.Provider.Provider> indexEntries)
        {
            var bulkProviderLocation = new BulkProviderClient(indexName, Client);
            try
            {
                foreach (var provider in indexEntries)
                {
                    var mappedProvider = ElasticsearchMapper.CreateProviderApiDocument(provider);
                    bulkProviderLocation.Create<ProviderApiDocument>(c => c.Document(mappedProvider));
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Something failed indexing provider api documents:" + ex.Message);
                throw;
            }

            return bulkProviderLocation.GetTasks();
        }

        public List<Task<IBulkResponse>> IndexStandards(string indexName, IEnumerable<Core.Models.Provider.Provider> indexEntries)
        {
            var bulkProviderLocation = new BulkProviderClient(indexName, Client);
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
                            var standardProvider = ElasticsearchMapper.CreateStandardProviderDocument(provider, standard, deliveryLocationsOnly100);
                            bulkProviderLocation.Create<StandardProvider>(c => c.Document(standardProvider));
                        }

                        foreach (var location in standard.DeliveryLocations.Where(_anyNotAtEmployer))
                        {
                            if (location.DeliveryLocation.Address.GeoPoint != null)
                            {
                                var standardProvider = ElasticsearchMapper.CreateStandardProviderDocument(provider, standard, location);
                                bulkProviderLocation.Create<StandardProvider>(c => c.Document(standardProvider));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Something failed indexing standard providers:" + ex.Message);
                throw;
            }

            return bulkProviderLocation.GetTasks();
        }
    }
}