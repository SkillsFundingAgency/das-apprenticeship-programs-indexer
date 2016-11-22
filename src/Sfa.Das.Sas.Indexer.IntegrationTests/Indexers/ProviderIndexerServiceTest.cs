//using System;
//using System.Globalization;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using FluentAssertions;
//using Moq;
//using Nest;
//using NUnit.Framework;
//using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
//using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory;
//using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
//using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
//using Sfa.Das.Sas.Indexer.AzureWorkerRole.DependencyResolution;
//using Sfa.Das.Sas.Indexer.Core.Logging;
//using Sfa.Das.Sas.Indexer.Core.Models.Framework;
//using Sfa.Das.Sas.Indexer.Core.Models.Provider;
//using Sfa.Das.Sas.Indexer.Core.Services;
//using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
//using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models;
//using StructureMap;
//using CourseDirectoryProvider=Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory.Provider;
//using CourseLocation = Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory.Location;
//using CourseAddress = Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory.Address;
//using Location = Sfa.Das.Sas.Indexer.Core.Models.Provider.Location;
//using Provider = Sfa.Das.Sas.Indexer.Core.Models.Provider.Provider;

//namespace Sfa.Das.Sas.Indexer.IntegrationTests.Indexers
//{
//    using System.Collections.Generic;

//    using Sfa.Das.Sas.Indexer.Core.Models;

//    using Address = Sfa.Das.Sas.Indexer.Core.Models.Provider.Address;

//    [TestFixture]
//    public class ProviderIndexerServiceTest
//    {
//        private IContainer _ioc;

//        private IGenericIndexerHelper<IMaintainProviderIndex> _providerIndexer;

//        private IElasticsearchCustomClient _elasticClient;

//        private IIndexSettings<IMaintainProviderIndex> _providerSettings;

//        private string _indexName;

//        [OneTimeSetUp]
//        public void SetUp()
//        {
//            _ioc = IoC.Initialize();

//            _providerSettings = _ioc.GetInstance<IIndexSettings<IMaintainProviderIndex>>();

//            var providerDataService = new Mock<IProviderDataService>();
//            providerDataService.Setup(m => m.LoadDatasetsAsync()).Returns(Task.FromResult(GetProvidersTest()));
//            var logger = new Mock<ILog>();

//            _providerIndexer = new ProviderIndexer(
//                _providerSettings,
//                _ioc.GetInstance<ICourseDirectoryProviderMapper>(),
//                _ioc.GetInstance<IMaintainProviderIndex>(),
//                providerDataService.Object,
//                logger.Object);

//            _elasticClient = _ioc.GetInstance<IElasticsearchCustomClient>();

//            _indexName = $"{_providerSettings.IndexesAlias}-{new DateTime(2000, 1, 1).ToUniversalTime().ToString("yyyy-MM-dd-HH-mm")}".ToLower(CultureInfo.InvariantCulture);

//            DeleteIndexIfExists(_indexName);

//            if (!_elasticClient.IndexExists(Indices.Index(_indexName)).Exists)
//            {
//                _providerIndexer.CreateIndex(_indexName);
//                var indexTask = _providerIndexer.IndexEntries(_indexName);
//                Task.WaitAll(indexTask);
//                Thread.Sleep(1000);
//            }
//        }

//        [OneTimeTearDown]
//        public void AfterAllTestAreRun()
//        {
//            _elasticClient.DeleteIndex(Indices.Index(_indexName));
//            _elasticClient.IndexExists(Indices.Index(_indexName)).Exists.Should().BeFalse();
//        }

//        [Test]
//        [Category("Integration")]
//        public void ShouldCreateIndexCorrectly()
//        {
//            _providerIndexer.IsIndexCorrectlyCreated(_indexName).Should().BeTrue();
//        }

//        [Test]
//        [Category("Integration")]
//        public void ShouldCreateScheduledIndexAndMappingForProviders()
//        {
//            var mappingFramework = _elasticClient.GetMapping<FrameworkProvider>(
//                i => i.Index(_indexName));
//            mappingFramework.Mappings.Count.Should().Be(1);

//            var mappingStandard = _elasticClient.GetMapping<StandardProvider>(i => i.Index(_indexName));
//            mappingStandard.Mappings.Count.Should().Be(1);

//            var mappingDummy = _elasticClient.GetMapping<DummyTestClass>(i => i.Index(_indexName));
//            mappingDummy.Mappings.Count.Should().Be(0);
//        }

//        [Test]
//        [Category("Integration")]
//        public void ShouldRetrieveProvidersSearchingForPostCode()
//        {
//            var expectedProviderResult = new Provider
//            {
//                Ukprn = 10002387,
//                Name = "F1 COMPUTER SERVICES & TRAINING LIMITED",
//                MarketingInfo = "Provider Marketing Information for F1 COMPUTER SERVICES & TRAINING LIMITED",
//                ContactDetails = new ContactInformation { Email = "test1@example.com", Website = "http://www.f1training.org.uk", Phone = "01449 770911" }
//            };

//            var retrievedResult = _elasticClient.Search<FrameworkProvider>(p => p.Index(_indexName).Query(q => q.QueryString(qs => qs.Query("MK40 2SG"))));
//            var amountRetrieved = retrievedResult.Documents.Count();
//            var retrievedProvider = retrievedResult.Documents.FirstOrDefault();

//            Assert.AreEqual(1, amountRetrieved);
//            Assert.AreEqual(expectedProviderResult.Name, retrievedProvider?.ProviderName);
//        }

//        [Test]
//        [Category("Integration")]
//        public void ShouldNotIndexProvidersWithoutGeoPoint()
//        {
//            var providersCaseFramework = _elasticClient.Search<Provider>(s => s
//               .Index(_indexName)
//               .Type(_providerSettings.FrameworkProviderDocumentType)
//               .Query(q => q
//                   .Term("frameworkCode", 45)));

//            var providersCaseStandard = _elasticClient.Search<Provider>(s => s
//               .Index(_indexName)
//               .Type(_providerSettings.StandardProviderDocumentType)
//               .Query(q => q
//                   .Term("standardCode", 45)));

//            providersCaseFramework.Hits.Count().Should().Be(1);
//            providersCaseStandard.Hits.Count().Should().Be(1);
//        }

//        [Test]
//        [Category("Integration")]
//        public void ShouldRetrieveProvidersSearchingForStandardId()
//        {
//            var providersCase1 = _elasticClient.Search<Provider>(s => s
//                .Index(_indexName)
//                .Type(_providerSettings.StandardProviderDocumentType)
//                .Query(q => q
//                    .Term("standardCode", 17)));

//            var providersCase2 = _elasticClient.Search<Provider>(s => s
//                .Index(_indexName)
//                .Type(_providerSettings.StandardProviderDocumentType)
//                .Query(q => q
//                    .Term("standardCode", 45)));

//            var providersCase3 = _elasticClient.Search<Provider>(s => s
//                .Index(_indexName)
//                .Type(_providerSettings.StandardProviderDocumentType)
//                .Query(q => q
//                    .Term("standardCode", 1234567890)));

//            var providersCase4 = _elasticClient.Search<Provider>(s => s
//                .Index(_indexName)
//                .Type(_providerSettings.FrameworkProviderDocumentType)
//                .Query(q => q
//                    .Term("frameworkCode", 45)));

//            Assert.AreEqual(1, providersCase1.Documents.Count());
//            Assert.AreEqual(1, providersCase2.Documents.Count());
//            Assert.AreEqual(0, providersCase3.Documents.Count());
//            Assert.AreEqual(1, providersCase4.Documents.Count());

//            _elasticClient.DeleteIndex(Indices.Index(_indexName));
//            _elasticClient.IndexExists(Indices.Index(_indexName)).Exists.Should().BeFalse();
//        }

//        private void DeleteIndexIfExists(string indexName)
//        {
//            var exists = _elasticClient.IndexExists(Indices.Index(indexName));
//            if (exists.Exists)
//            {
//                _elasticClient.DeleteIndex(Indices.Index(indexName));
//            }
//        }

//        private ProviderSourceDto GetProvidersTest()
//        {
//            return new ProviderSourceDto
//            {
//                AchievementRateNationals = new List<AchievementRateNational>(),
//                AchievementRateProviders = new List<AchievementRateProvider>(),
//                ActiveProviders = GetActiveProviders(),
//                CourseDirectoryProviders = GetCourseDirectoryProviders(),
//                CourseDirectoryUkPrns = GetActiveProviders(),
//                EmployerProviders = new List<string>(),
//                EmployerSatisfactionRates = new List<SatisfactionRateProvider>(),
//                Frameworks = GetFrameworks(),
//                HeiProviders = new List<string>(),
//                LearnerSatisfactionRates = new List<SatisfactionRateProvider>(),
//                Standards = GetStandards()
//            };
//        }

//        private IEnumerable<int> GetActiveProviders()
//        {
//            return GetCourseDirectoryProviders().Select(x => x.Ukprn);
//        }

//        private IEnumerable<StandardMetaData> GetStandards()
//        {
//            yield return new StandardMetaData {Id = 17, Title = "title"};
//            yield return new StandardMetaData {Id = 45, Title = "title"};
//        }

//        private IEnumerable<FrameworkMetaData> GetFrameworks()
//        {
//            yield return new FrameworkMetaData
//            {
//                FworkCode = 45,
//                PwayCode = 7,
//                ProgType = 5
//            };
//        }

//        private IEnumerable<CourseDirectoryProvider> GetCourseDirectoryProviders()
//        {
//            var locations = GetLocations();
//            return new List<CourseDirectoryProvider>
//                       {
//                           new CourseDirectoryProvider
//                               {
//                                   Id = 304107,
//                                   Ukprn = 10002387,
//                                   Name = "F1 COMPUTER SERVICES & TRAINING LIMITED",
//                                   MarketingInfo = "Provider Marketing Information for F1 COMPUTER SERVICES & TRAINING LIMITED",
//                                   Phone = "01449 770911",
//                                   Email = "test1@example.com",
//                                   Website = "http://www.f1training.org.uk",
//                                   LearnerSatisfaction = null,
//                                   EmployerSatisfaction = null,
//                                   Standards =
//                                       new List<Standard>
//                                           {
//                                               new Standard
//                                                   {
//                                                       StandardCode = 17,
//                                                       MarketingInfo = "Provider 304107 marketing into for standard code 17",
//                                                       StandardInfoUrl = "www.Provider304107Standard17StandardInfoURL.com",
//                                                       Contact = 
//                                                           new Contact
//                                                               {
//                                                                   Phone = "Provider304107Standard17Tel",
//                                                                   Email =
//                                                                       "Provider304107@Standard17ContactEmail.com",
//                                                                   ContactUsUrl = 
//                                                                       "www.Provider304107Standard17ContactURL.com"
//                                                               },
//                                                       Locations =
//                                                           new List<LocationRef>
//                                                               {
//                                                                   new LocationRef
//                                                                       {
//                                                                           ID = 115643,
//                                                                           DeliveryModes = new[] { "BlockRelease" },
//                                                                           Radius = 80
//                                                                       }
//                                                               }
//                                                   },
//                                               new Standard
//                                                   {
//                                                       StandardCode = 45,
//                                                       MarketingInfo = "Provider 304107 marketing into for standard code 45",
//                                                       StandardInfoUrl = "www.Provider304107Standard45StandardInfoURL.com",
//                                                       Contact = 
//                                                           new Contact
//                                                               {
//                                                                   Phone = "Provider304107Standard45Tel",
//                                                                   Email =
//                                                                       "Provider304107@Standard45ContactEmail.com",
//                                                                   ContactUsUrl =
//                                                                       "www.Provider304107Standard45ContactURL.com"
//                                                               },
//                                                       Locations =
//                                                           new List<LocationRef>
//                                                               {
//                                                                   new LocationRef
//                                                                    {
//                                                                        ID
//                                                                            =115641,
//                                                                        DeliveryModes
//                                                                            =
//                                                                            new string[]
//                                                                                {
//                                                                                    "100PercentEmploye"
//                                                                                },
//                                                                        Radius = 80
//                                                                    },
//                                                                   new LocationRef
//                                                                    {
//                                                                        ID = 115640,
//                                                                        DeliveryModes
//                                                                            =
//                                                                            new string[]
//                                                                                {
//                                                                                    "100PercentEmployer"
//                                                                                },
//                                                                        Radius = 80
//                                                                    }
//                                                               }
//                                                   }
//                                           },
//                                   Frameworks = new List<Framework>
//                                                    {
//                                                        new Framework
//                                                   {
//                                                       FrameworkCode = 45,
//                                                       PathwayCode = 7,
//                                                       ProgType = 5,
//                                                       MarketingInfo = "Provider 304107 marketing into for standard code 45",
//                                                       FrameworkInfoUrl = "www.Provider304107Standard45StandardInfoURL.com",
//                                                            Contact =
//                                                           new Contact
//                                                               {
//                                                                   Phone = "Provider304107Standard45Tel",
//                                                                   Email =
//                                                                       "Provider304107@Standard45ContactEmail.com",
//                                                                   ContactUsUrl = 
//                                                                       "www.Provider304107Standard45ContactURL.com"
//                                                               },
//                                                            Locations = 
//                                                           new List<LocationRef>
//                                                               {
//                                                                   new LocationRef
//                                                                    {
//                                                                        ID = 115641,
//                                                                        DeliveryModes = new string[]
//                                                                        {
//                                                                            "100PercentEmployer"
//                                                                        },
//                                                                        Radius = 80
//                                                                    },
//                                                                   new LocationRef
//                                                                       {
//                                                                           ID = 115640,
//                                                                           DeliveryModes = new[] { "BlockRelease" },
//                                                                           Radius = 80
//                                                                       }
//                                                               }
//                                                   }
//                                                    },
//                                   Locations = locations.ToList()
//                               }
//                       };
//        }

//        private static IEnumerable<CourseLocation> GetLocations()
//        {
//            yield return new CourseLocation
//            {
//                ID = 115640,
//                Name = "Null GeoPoint",
//                Address =
//                    new CourseAddress
//                    {
//                        Address1 = "Enterprise House 2",
//                        Address2 = "2-6 Union Street",
//                        Town = "Bedford",
//                        County = null,
//                        Postcode = "MK40 2SG",
//                    },
//                Website = "http://testsite.com",
//                Email = "test@test.com",
//                Phone = "0111222222"
//            };

//            yield return new CourseLocation
//            {
//                ID = 115641,
//                Name = "F1 TRAINING LTD - BEDFORD LEARNING CENTRE",
//                Address =
//                    new CourseAddress
//                    {
//                        Address1 = "Enterprise House",
//                        Address2 = "2-6 Union Street",
//                        Town = "Bedford",
//                        County = null,
//                        Postcode = "MK40 2SG",
//                        Latitude = 52.139922,
//                        Longitude = -0.475378
//                    },
//                Website = "http://testsite.com", Email = "test@test.com", Phone = "0111222222"
//            };

//            yield return new CourseLocation
//            {
//                ID = 115643,
//                Name = "F1 TRAINING LTD - GT YARMOUTH LEARNING CENTRE",
//                Address =
//                    new CourseAddress
//                    {
//                        Address1 = "Catalyst - Business Acceleration Centre",
//                        Address2 = "The Conge",
//                        Town = "Great Yarmouth",
//                        County = null,
//                        Postcode = "NR30 1NA",
//                        Latitude = 52.609776,
//                        Longitude = 1.725685
//                    },
//                Website = "http://testsite2.com",
//                Email = "test2@test.com",
//                Phone = "033444555"
//            };
//        }

//        internal class DummyTestClass
//        {
//        }
//    }
//}