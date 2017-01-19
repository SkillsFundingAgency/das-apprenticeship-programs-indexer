using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Nest;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
using Sfa.Das.Sas.Indexer.Core.Exceptions;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    using System;
    using System.Linq;

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

        public async Task IndexEntries(string indexName, ICollection<Provider> indexEntries)
        {
            var bulkStandardTasks = new List<Task<IBulkResponse>>();
            var bulkFrameworkTasks = new List<Task<IBulkResponse>>();
            var bulkProviderTasks = new List<Task<IBulkResponse>>();

            bulkStandardTasks.AddRange(IndexStandards(indexName, indexEntries));
            bulkFrameworkTasks.AddRange(IndexFrameworks(indexName, indexEntries));
            bulkProviderTasks.AddRange(IndexProviders(indexName, indexEntries));

            LogResponse(await Task.WhenAll(bulkStandardTasks), "StandardProvider");
            LogResponse(await Task.WhenAll(bulkFrameworkTasks), "FrameworkProvider");

            var a = await Task.WhenAll(bulkProviderTasks);
            var patata = a;
            //LogResponse(await Task.WhenAll(bulkProviderTasks), "ProviderDocument");
        }

        public List<Task<IBulkResponse>> IndexFrameworks(string indexName, ICollection<Provider> indexEntries)
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
                                bulkProviderLocation.Index<FrameworkProvider>(c => c.Document(frameworkProvider));
                            }
                        }

                        foreach (var location in framework.DeliveryLocations.Where(_anyNotAtEmployer))
                        {
                            if (location.DeliveryLocation.Address.GeoPoint != null)
                            {
                                var frameworkProvider = ElasticsearchMapper.CreateFrameworkProviderDocument(provider, framework, location);
                                bulkProviderLocation.Index<FrameworkProvider>(c => c.Document(frameworkProvider));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Something failed indexing framework providers:" + ex.Message);
                throw;
            }

            return bulkProviderLocation.GetTasks();
        }

        public List<Task<IBulkResponse>> IndexProviders(string indexName, ICollection<Provider> indexEntries)
        {
            var bulkProviderLocation = new BulkProviderClient(indexName, Client);
            try
            {
                foreach (var provider in indexEntries)
                {
                    var mappedProvider = ElasticsearchMapper.CreateProviderDocument(provider);
                    bulkProviderLocation.Index<ProviderDocument>(c => c.Document(mappedProvider));
                }
            }
            catch (Exception ex)
            {
                _log.Error("Something failed indexing provider documents:" + ex.Message);
                throw;
            }

            return bulkProviderLocation.GetTasks();
        }

        public List<Task<IBulkResponse>> IndexStandards(string indexName, IEnumerable<Provider> indexEntries)
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
                            bulkProviderLocation.Index<StandardProvider>(c => c.Document(standardProvider));
                        }

                        foreach (var location in standard.DeliveryLocations.Where(_anyNotAtEmployer))
                        {
                            if (location.DeliveryLocation.Address.GeoPoint != null)
                            {
                                var standardProvider = ElasticsearchMapper.CreateStandardProviderDocument(provider, standard, location);
                                bulkProviderLocation.Index<StandardProvider>(c => c.Document(standardProvider));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Something failed indexing standard providers:" + ex.Message);
                throw;
            }

            return bulkProviderLocation.GetTasks();
        }
    }
}