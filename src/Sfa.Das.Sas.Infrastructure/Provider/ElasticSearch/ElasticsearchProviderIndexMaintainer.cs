using System.Globalization;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Nest;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.Core.Exceptions;
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
            var response = Client.CreateIndex(
                indexName,
                i => i
                    .Settings(settings => settings
                        .NumberOfShards(_elasticsearchConfiguration.ProviderIndexShards())
                        .NumberOfReplicas(_elasticsearchConfiguration.ProviderIndexReplicas()))
                    .Mappings(ms => ms
                        .Map<StandardProvider>(m => m.AutoMap())
                        .Map<FrameworkProvider>(m => m.AutoMap())));
        }

        public async Task IndexProviders(string indexName, ICollection<Provider> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateProviderDocument).ConfigureAwait(true);
            }
        }

        public async Task IndexApiProviders(string indexName, ICollection<Provider> entries)
        {
            var smallLists = SplitAndReturn(entries.ToList(), 10000);

            foreach (var smallList in smallLists)
            {
                await IndexEntries(indexName, smallList, ElasticsearchMapper.CreateProviderApiDocument).ConfigureAwait(true);
            }
        }

        public async Task IndexStandards(string indexName, ICollection<Provider> indexEntries)
        {
            var bulkProviderLocation = new BulkProviderClient(indexName, Client);
            var bulkTasks = new List<Task<IBulkResponse>>();

            try
            {
                var count = 0;

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
                            foreach (var deliveryInformation in deliveryLocationsOnly100)
                            {
                                var standardProvider = ElasticsearchMapper.CreateStandardProviderDocument(provider, standard, deliveryInformation);
                                bulkProviderLocation.Index<StandardProvider>(c => c.Document(standardProvider));
                                count++;
                            }
                        }

                        foreach (var location in standard.DeliveryLocations.Where(_anyNotAtEmployer))
                        {
                            if (location.DeliveryLocation.Address.GeoPoint != null)
                            {
                                var standardProvider = ElasticsearchMapper.CreateStandardProviderDocument(provider, standard, location);
                                bulkProviderLocation.Index<StandardProvider>(c => c.Document(standardProvider));
                                count++;
                            }
                        }
                    }

                    if (count >= 10000)
                    {
                        count = 0;
                        bulkTasks = new List<Task<IBulkResponse>>();
                        bulkTasks.AddRange(bulkProviderLocation.GetTasks());
                        LogResponse(await Task.WhenAll(bulkTasks), typeof(FrameworkProvider).Name.ToLower(CultureInfo.CurrentCulture));
                        bulkProviderLocation = new BulkProviderClient(indexName, Client);
                    }
                }

                if (count != 0)
                {
                    bulkTasks = new List<Task<IBulkResponse>>();
                    bulkTasks.AddRange(bulkProviderLocation.GetTasks());
                    LogResponse(await Task.WhenAll(bulkTasks), typeof(FrameworkProvider).Name.ToLower(CultureInfo.CurrentCulture));
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Something failed indexing framework providers:" + ex.Message);
                throw;
            }
        }

        public async Task IndexFrameworks(string indexName, ICollection<Provider> indexEntries)
        {
            var bulkProviderLocation = new BulkProviderClient(indexName, Client);
            var bulkTasks = new List<Task<IBulkResponse>>();

            try
            {
                var count = 0;
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
                                count++;
                            }
                        }

                        foreach (var location in framework.DeliveryLocations.Where(_anyNotAtEmployer))
                        {
                            if (location.DeliveryLocation.Address.GeoPoint != null)
                            {
                                var frameworkProvider = ElasticsearchMapper.CreateFrameworkProviderDocument(provider, framework, location);
                                bulkProviderLocation.Index<FrameworkProvider>(c => c.Document(frameworkProvider));
                                count++;
                            }
                        }
                    }

                    if (count >= 10000)
                    {
                        count = 0;
                        bulkTasks = new List<Task<IBulkResponse>>();
                        bulkTasks.AddRange(bulkProviderLocation.GetTasks());
                        LogResponse(await Task.WhenAll(bulkTasks), typeof(FrameworkProvider).Name.ToLower(CultureInfo.CurrentCulture));
                        bulkProviderLocation = new BulkProviderClient(indexName, Client);
                    }
                }

                if (count != 0)
                {
                    bulkTasks = new List<Task<IBulkResponse>>();
                    bulkTasks.AddRange(bulkProviderLocation.GetTasks());
                    LogResponse(await Task.WhenAll(bulkTasks), typeof(FrameworkProvider).Name.ToLower(CultureInfo.CurrentCulture));
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Something failed indexing standard providers:" + ex.Message);
                throw;
            }
        }

        private async Task IndexEntries<T1, T2>(string indexName, IEnumerable<T1> entries, Func<T1, T2> method)
            where T1 : class
            where T2 : class
        {
            try
            {
                var bulkLarsClient = new BulkProviderClient(indexName, Client);

                foreach (var entry in entries)
                {
                    try
                    {
                        var doc = method(entry);

                        bulkLarsClient.Index<T2>(c => c.Document(doc));
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IEnumerable<List<T>> SplitAndReturn<T>(List<T> entries, int size)
        {
            for (int i = 0; i < entries.Count; i = i + size)
            {
                var actualSize = Math.Min(size, entries.Count - i);
                yield return entries.GetRange(i, actualSize);
            }
        }
    }
}