﻿using Sfa.Eds.Das.Indexer.Core.Models;

namespace Sfa.Eds.Das.Indexer.ApplicationServices.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Nest;

    using Sfa.Eds.Das.Indexer.ApplicationServices.Infrastructure;
    using Sfa.Eds.Das.Indexer.ApplicationServices.Services.Interfaces;
    using Sfa.Eds.Das.Indexer.ApplicationServices.Settings;
    using Sfa.Eds.Das.Indexer.Core;
    using Sfa.Eds.Das.ProviderIndexer.Models;

    public class ProviderHelper : IGenericIndexerHelper<Provider>
    {
        private readonly IGetActiveProviders _activeProviderClient;

        private readonly IGetProviders _courseDirectoryClient;

        private readonly IElasticClient _client;

        private readonly IIndexSettings<Provider> _settings;

        private readonly ILog Log;

        public ProviderHelper(
            IIndexSettings<Provider> settings,
            IElasticsearchClientFactory elasticsearchClientFactory,
            IGetProviders courseDirectoryClient,
            IGetActiveProviders activeProviderClient,
            ILog log)
        {
            _settings = settings;
            _courseDirectoryClient = courseDirectoryClient;
            _activeProviderClient = activeProviderClient;

            _client = elasticsearchClientFactory.GetElasticClient();
            Log = log;
        }

        public ICollection<Provider> LoadEntries()
        {
            var providers = _courseDirectoryClient.GetProviders();
            var activeProviders = _activeProviderClient.GetActiveProviders();
            Task.WaitAll();

            return providers.Result.Where(x => activeProviders.Contains(x.UkPrn)).ToList();
        }

        public bool CreateIndex(DateTime scheduledRefreshDateTime)
        {
            // TODO: replace this method with CreateIndexForBulkData when Course directory data is available
            var indexName = GetIndexNameAndDateExtension(scheduledRefreshDateTime);

            var indexExistsResponse = _client.IndexExists(indexName);

            // If it already exists and is empty, let's delete it.
            if (indexExistsResponse.Exists)
            {
                Log.Warn("Index already exists, deleting and creating a new one");

                _client.DeleteIndex(indexName);
            }

            // create index
            var json = @"
                {
                    ""mappings"": 
                    {
                        ""provider"": 
                        { 
                            ""properties"": 
                            {
                                ""id"":
                                {
                                    ""type"": ""long""
                                },
                                ""providerName"":
                                {
                                    ""type"": ""string""
                                },
                                ""postCode"":
                                {
                                    ""type"": ""string""
                                },
                                ""radius"":
                                {
                                    ""type"": ""long""
                                },
                                ""ukPrn"":
                                {
                                    ""type"": ""string""
                                },
                                ""venueName"":
                                {
                                    ""type"": ""string""
                                },
                                ""standardsId"":
                                {
                                    ""type"": ""long""
                                },
                                ""locationPoint"":
                                {
                                    ""type"": ""geo_point""
                                },
                                ""location"": 
                                {
                                    ""type"": ""geo_shape""
                                }
                            }
                        }
                    }
                }";
            _client.Raw.IndicesCreatePost(indexName, json);

            var exists = _client.IndexExists(indexName).Exists;
            return exists;
        }

        public bool CreateIndexForBulkData(DateTime scheduledRefreshDateTime)
        {
            var indexName = GetIndexNameAndDateExtension(scheduledRefreshDateTime);

            var indexExistsResponse = _client.IndexExists(indexName);

            // If it already exists and is empty, let's delete it.
            if (indexExistsResponse.Exists)
            {
                Log.Warn("Index already exists, deleting and creating a new one");

                _client.DeleteIndex(indexName);
            }

            // create index
            var json = @"
                {
                    ""mappings"": 
                    {
                        ""provider"": 
                        { 
                            ""properties"": 
                            {
                                ""ukprn"":
                                {
                                    ""type"": ""long""
                                },
                                ""name"":
                                {
                                    ""type"": ""string""
                                },
                                ""standardCode"":
                                {
                                    ""type"": ""long""
                                },
                                ""venueName"":
                                {
                                    ""type"": ""string""
                                },
                                ""marketingInfo"":
                                {
                                    ""type"": ""string""
                                },
                                ""standardInfoUrl"":
                                {
                                    ""type"": ""string""
                                },
                                ""deliveryModes"":
                                {
                                    ""type"": ""string""
                                },
                                ""website"":
                                {
                                    ""type"": ""string""
                                },
                                ""phone"":
                                {
                                    ""type"": ""string""
                                },
                                ""email"":
                                {
                                    ""type"": ""string""
                                },
                                ""contactUsUrl"":
                                {
                                    ""type"": ""string""
                                },
                                ""standardsId"":
                                {
                                    ""type"": ""long""
                                },
                                ""locationPoint"":
                                {
                                    ""type"": ""geo_point""
                                },
                                ""location"": 
                                {
                                    ""type"": ""geo_shape""
                                },
                                ""address"": 
                                {
                                    ""type"": ""nested"",
                                    ""properties"": 
                                    {
                                        ""address1"":       
                                        { 
                                            ""type"": ""string""  
                                        },
                                        ""address2"":       
                                        { 
                                            ""type"": ""string""  
                                        },  
                                        ""town"":       
                                        { 
                                            ""type"": ""string""  
                                        },
                                        ""county"":       
                                        { 
                                            ""type"": ""string""  
                                        },
                                        ""postcode"":       
                                        { 
                                            ""type"": ""string""  
                                        }
                                    }
                                }
                            }
                        }
                    }
                }";
            _client.Raw.IndicesCreatePost(indexName, json);

            var exists = _client.IndexExists(indexName).Exists;
            return exists;
        }

        public async Task IndexEntries(DateTime scheduledRefreshDateTime, ICollection<Provider> entries)
        {
            try
            {
                Log.Debug("Indexing " + entries.Count + " providers");

                var indexName = GetIndexNameAndDateExtension(scheduledRefreshDateTime);
                await IndexProviders(indexName, entries).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Error("Error indexing providerss: " + e.Message);
            }
        }

        public bool IsIndexCorrectlyCreated(DateTime scheduledRefreshDateTime)
        {
            var indexName = GetIndexNameAndDateExtension(scheduledRefreshDateTime);

            var a = _client.Search<Provider>(s => s.Index(indexName).From(0).Size(1000).MatchAll()).Documents;
            return a.Any();
        }

        public void SwapIndexes(DateTime scheduledRefreshDateTime)
        {
            var indexAlias = _settings.IndexesAlias;
            var newIndexName = GetIndexNameAndDateExtension(scheduledRefreshDateTime);

            if (!CheckIfAliasExists(indexAlias))
            {
                Log.Warn("Alias doesn't exists, creating a new one...");

                CreateAlias(newIndexName);
            }

            var existingIndexesOnAlias = _client.GetIndicesPointingToAlias(indexAlias);
            var aliasRequest = new AliasRequest { Actions = new List<IAliasAction>() };

            foreach (var existingIndexOnAlias in existingIndexesOnAlias)
            {
                aliasRequest.Actions.Add(
                    new AliasRemoveAction { Remove = new AliasRemoveOperation { Alias = indexAlias, Index = existingIndexOnAlias } });
            }

            aliasRequest.Actions.Add(new AliasAddAction { Add = new AliasAddOperation { Alias = indexAlias, Index = newIndexName } });
            _client.Alias(aliasRequest);
        }

        public void DeleteOldIndexes(DateTime scheduledRefreshDateTime)
        {
            var indices = _client.IndicesStats().Indices;

            var providerIndexToday = $"{_settings.IndexesAlias}-{scheduledRefreshDateTime.ToUniversalTime().ToString("yyyy-MM-dd")}";
            var providerIndexOneDayAgo = $"{_settings.IndexesAlias}-{scheduledRefreshDateTime.AddDays(-1).ToUniversalTime().ToString("yyyy-MM-dd")}";
            var providerIndexTwoDaysAgo = $"{_settings.IndexesAlias}-{scheduledRefreshDateTime.AddDays(-2).ToUniversalTime().ToString("yyyy-MM-dd")}";
            var standardIndex = "standard";
            var logIndex = "log";
            var kibanaIndex = "kibana";

            foreach (var index in indices.Where(index => !index.Key.Contains(providerIndexToday)
                                                         && !index.Key.Contains(providerIndexOneDayAgo)
                                                         && !index.Key.Contains(providerIndexTwoDaysAgo)
                                                         && !index.Key.Contains(standardIndex)
                                                         && !index.Key.Contains(logIndex)
                                                         && !index.Key.Contains(kibanaIndex)))
            {
                _client.DeleteIndex(index.Key);
            }
        }

        public string GetIndexNameAndDateExtension(DateTime dateTime)
        {
            return
                string.Format("{0}-{1}", _settings.IndexesAlias, dateTime.ToUniversalTime().ToString("yyyy-MM-dd-HH"))
                    .ToLower(CultureInfo.InvariantCulture);
        }

        private string CreateProviderRawFormat(Provider provider)
        {
            var i = 0;
            var standardsId = new StringBuilder();
            foreach (var standardId in provider.StandardsId)
            {
                if (i == 0)
                {
                    standardsId.Append(standardId);
                }
                else
                {
                    standardsId.Append(string.Concat(", ", standardId));
                }

                i++;
            }

            var rawProvider = string.Concat(
                @"{ ""id"": """,
                provider.ProviderId,
                @""", ""providerName"": """,
                provider.ProviderName,
                @""", ""postCode"": """,
                provider.PostCode,
                @""", ""standardsId"": [",
                standardsId,
                @"], ""venueName"": """,
                provider.VenueName,
                @""", ""ukPrn"": """,
                provider.UkPrn,
                @""", ""locationPoint"": [",
                provider.Coordinate.Lon,
                ", ",
                provider.Coordinate.Lat,
                @"],""location"": { ""type"": ""circle"", ""coordinates"": [",
                provider.Coordinate.Lon,
                ", ",
                provider.Coordinate.Lat,
                @"], ""radius"": """,
                provider.Radius,
                @"mi"" }}");
            return rawProvider;
        }

        public List<string> CreateListRawFormat(ProviderBulk provider, StandardProvider standard)
        {
            var list = new List<string>();

            foreach (var standardLocation in standard.Locations)
            {
                var location = provider.Locations.FirstOrDefault(x => x.Id == standardLocation.Id);

                var i = 0;
                var deliveryModes = new StringBuilder();
                foreach (var deliveryMode in standardLocation.DeliveryModes)
                {
                    deliveryModes.Append(i == 0 ? string.Concat(@"""", deliveryMode, @"""") : string.Concat(", ", @"""", deliveryMode, @""""));

                    i++;
                }

                var rawProvider = string.Concat(
                @"{ ""ukprn"": """,
                provider.UkPrn,
                @""", ""name"": """,
                provider.Name ?? "Unspecified",
                @""", ""standardcode"": ",
                standard.StandardCode,
                @", ""locationName"": """,
                location.Name ?? "Unspecified",
                @""", ""marketingInfo"": """,
                standard.MarketingInfo ?? "Unspecified",
                @""", ""phone"": """,
                standard.Contact.Phone ?? "Unspecified",
                @""", ""email"": """,
                standard.Contact.Email ?? "Unspecified",
                @""", ""contactUsUrl"": """,
                standard.Contact.ContactUsUrl ?? "Unspecified",
                @""", ""standardInfoUrl"": """,
                standard.StandardInfoUrl ?? "Unspecified",
                @""", ""deliveryModes"": [",
                deliveryModes,
                @"], ""website"": """,
                location.Website ?? "Unspecified",
                @""", ""address"": {""address1"":""",
                location.Address.Address1 ?? "Unspecified",
                @""", ""address2"": """,
                location.Address.Address2 ?? "Unspecified",
                @""", ""town"": """,
                location.Address.Town ?? "Unspecified",
                @""", ""county"": """,
                location.Address.County ?? "Unspecified",
                @""", ""postcode"": """,
                location.Address.Postcode ?? "Unspecified",
                @"""}, ""locationPoint"": [",
                location.Address.Long,
                ", ",
                location.Address.Lat,
                @"],""location"": { ""type"": ""circle"", ""coordinates"": [",
                location.Address.Long,
                ", ",
                location.Address.Lat,
                @"], ""radius"": """,
                standardLocation.Radius,
                @"mi"" }}");

                list.Add(rawProvider);
            }

            return list;
        }

        private Task IndexProviders(string indexName, IEnumerable<Provider> providers)
        {
            var id = 1;

            // index the items
            foreach (var provider in providers)
            {
                try
                {
                    provider.ProviderId = id;
                    _client.Raw.Index(indexName, "provider", CreateProviderRawFormat(provider));
                    id++;
                }
                catch (Exception e)
                {
                    Log.Error("Error indexing provider: " + e.Message);
                    throw;
                }
            }

            return null;
        }

        private bool CheckIfAliasExists(string aliasName)
        {
            var aliasExistsResponse = _client.AliasExists(aliasName);

            return aliasExistsResponse.Exists;
        }

        private void CreateAlias(string indexName)
        {
            _client.Alias(a => a.Add(add => add.Index(indexName).Alias(_settings.IndexesAlias)));
        }
    }
}