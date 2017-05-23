﻿namespace Sfa.Das.Sas.Indexer.IntegrationTests.Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Nest;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
    using Sfa.Das.Sas.Indexer.AzureWorkerRole.DependencyResolution;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;
    using Sfa.Das.Sas.Indexer.Core.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;

    [TestFixture]
    public class ApprenticeshipIndexerServiceTests
    {
        private IIndexSettings<IMaintainApprenticeshipIndex> _standardSettings;

        private IGenericIndexerHelper<IMaintainApprenticeshipIndex> _indexerService;

        private IElasticsearchCustomClient _elasticClient;

        private string _indexName;

        [OneTimeSetUp]
        public void BeforeAnyTest()
        {
            var ioc = IoC.Initialize();
            _standardSettings = ioc.GetInstance<IIndexSettings<IMaintainApprenticeshipIndex>>();
            _indexerService = ioc.GetInstance<IGenericIndexerHelper<IMaintainApprenticeshipIndex>>();

            var settings = ioc.GetInstance<IIndexSettings<IMaintainApprenticeshipIndex>>();

            var maintanSearchIndex = ioc.GetInstance<IMaintainApprenticeshipIndex>();

            var moqMetaDataHelper = new Mock<IMetaDataHelper>();
            var moqMediator = new Mock<IMediator>();
            moqMetaDataHelper.Setup(m => m.UpdateMetadataRepository());
            moqMediator.Setup(m => m.Send(It.IsAny<StandardMetaDataRequest>())).Returns(GetStandardsTest());
            moqMediator.Setup(m => m.Send(It.IsAny<FrameworkMetaDataRequest>())).Returns(GetFrameworksTest());

            var moqLog = new Mock<ILog>();

            _indexerService = new ApprenticeshipIndexer(settings, moqMediator.Object, maintanSearchIndex, moqMetaDataHelper.Object, moqLog.Object);

            var elasticCustomClient = ioc.GetInstance<IElasticsearchCustomClient>();
            _elasticClient = elasticCustomClient;

            _indexName = $"{_standardSettings.IndexesAlias}-{new DateTime(2000, 1, 1).ToUniversalTime().ToString("yyyy-MM-dd-HH")}".ToLower(CultureInfo.InvariantCulture);

            DeleteIndexIfExists(_indexName);

            if (!_elasticClient.IndexExists(Indices.Index(_indexName)).Exists)
            {
                _indexerService.CreateIndex(_indexName);
                var indexTask = _indexerService.IndexEntries(_indexName);
                Task.WaitAll(indexTask);
                Thread.Sleep(1000);
                _elasticClient.Refresh(Indices.Index(_indexName));
            }
        }

        [SetUp]
        public void BeforeEachTest()
        {
        }

        [OneTimeTearDown]
        public void AfterAllTestAreRun()
        {
            _elasticClient.DeleteIndex(Indices.Index(_indexName));
            _elasticClient.IndexExists(Indices.Index(_indexName)).Exists.Should().BeFalse();
        }

        [Test]
        [Category("Integration")]
        [Ignore("Waiting for ElasticSearch service")]
        public void ShouldCreateScheduledIndexAndMappingForStandardsAndFrameworks()
        {
            var mappingStandards = _elasticClient.GetMapping<StandardDocument>(i => i.Index(_indexName));
            mappingStandards.Mappings.Count.Should().NotBe(0);

            var mappingFrameworks = _elasticClient.GetMapping<FrameworkDocument>(i => i.Index(_indexName));
            mappingFrameworks.Mappings.Count.Should().NotBe(0);
        }

        [Test]
        [Category("Integration")]
        [Ignore("Waiting for ElasticSearch service")]
        public void ShouldRetrieveStandardSearchingForTitle()
        {
            var expectedStandardResult = new StandardMetaData
            {
                Id = 61,
                Title = "Dental Nurse",
                NotionalEndLevel = 3,
                StandardPdfUrl = "https://www.gov.uk/government/uploads/system/uploads/attachment_data/file/411720/DENTAL_HEALTH_-_Dental_Nurse.pdf",
                TypicalLength = new TypicalLength() { From = 12, Unit = "m"}
            };

            var retrievedResult = _elasticClient.Search<StandardDocument>(p => p.Index(_indexName).Query(q => q.QueryString(qs => qs.Query(expectedStandardResult.Title))));

            var amountRetrieved = retrievedResult.Documents.Count();
            var retrievedStandard = retrievedResult.Documents.FirstOrDefault();

            Assert.AreEqual(1, amountRetrieved);
            Debug.Assert(retrievedStandard != null, "retrievedStandard != null");
            Assert.AreEqual(expectedStandardResult.Title, retrievedStandard.Title);
            Assert.AreEqual(expectedStandardResult.NotionalEndLevel, retrievedStandard.Level);
            Assert.AreEqual(expectedStandardResult.Id, retrievedStandard.StandardId);
            Assert.AreEqual(12, retrievedStandard.TypicalLength.From);
        }

        [Test]
        [Category("Integration")]
        [Ignore("Waiting for ElasticSearch service")]
        public void ShouldRetrieveFrameworksSearchingForAll()
        {
            // Assert
            var retrievedResultFrameworks = _elasticClient.Search<FrameworkDocument>(p => p.Index(_indexName).Query(q => q.QueryString(qs => qs.Query("*"))));

            var frameworkDocuments = retrievedResultFrameworks.Documents.Count();
            var frameworkDocument = retrievedResultFrameworks.Documents.Single(m => m.FrameworkCode.Equals(423));

            Assert.AreEqual(3, frameworkDocuments);
            Assert.AreEqual("Fashion and Textiles", frameworkDocument.FrameworkName);
            Assert.AreEqual("Footwear", frameworkDocument.PathwayName);
            Assert.AreEqual(4, frameworkDocument.PathwayCode);
        }

        [Test]
        [Category("Integration")]
        [Ignore("Waiting for ElasticSearch service")]
        public void ShouldRetrieveStandardsWhenSearchingOnWordRootForm()
        {
            var retrievedResult = _elasticClient.Search<StandardDocument>(p => p
                .Index(_indexName)
                .Query(q => q
                    .QueryString(qs => qs
                            .Fields(fs => fs
                                .Field(fi => fi.Title)
                                .Field(fi => fi.JobRoles))
                            .Query("develop"))));

            var amountRetrieved = retrievedResult.Documents.Count();
            var retrievedStandard = retrievedResult.Documents.FirstOrDefault();

            Assert.AreEqual(1, amountRetrieved);
            Debug.Assert(retrievedStandard != null, "retrievedStandard != null");
            Assert.AreEqual("Software Developer", retrievedStandard.Title);
            Assert.AreEqual(0, retrievedStandard.Level);
            Assert.AreEqual(2, retrievedStandard.StandardId);
        }

        [TestCase("textile", 1, "Fashion and Textiles")]
        [TestCase("brew", 1, "Food and Drink")]
        [Category("Integration")]
        [Ignore("Waiting for ElasticSearch service")]
        public void ShouldRetrieveFrameworksWhenSearchingWithRootForm(string query, int expectedResultCount, string exectedFrameworkTitle)
        {
            var retrievedResultTextile = _elasticClient.Search<FrameworkDocument>(p => p
                .Index(_indexName)
                .Query(q => q
                    .QueryString(qs => qs
                            .Fields(fs => fs
                                .Field(fi => fi.FrameworkName)
                                .Field(fi => fi.PathwayName))
                            .Query(query))));

            Assert.AreEqual(expectedResultCount, retrievedResultTextile.Documents.Count());
            Assert.AreEqual(exectedFrameworkTitle, retrievedResultTextile.Documents.FirstOrDefault()?.FrameworkName);
        }

        [TestCase("and", 0)]
        [Category("Integration")]
        [Ignore("Waiting for ElasticSearch service")]
        public void ShouldNotRetrieveFrameworksWhenSearchingOnStopWords(string query, int expectedResultCount)
        {
            var retrievedResultTextile = _elasticClient.Search<FrameworkDocument>(p => p
                .Index(_indexName)
                .Query(q => q
                    .QueryString(qs => qs
                            .Fields(fs => fs
                                .Field(fi => fi.FrameworkName)
                                .Field(fi => fi.PathwayName))
                            .Query(query))));

            Assert.AreEqual(expectedResultCount, retrievedResultTextile.Documents.Count());
        }

        private void DeleteIndexIfExists(string indexName)
        {
            var exists = _elasticClient.IndexExists(Indices.Index(indexName));
            if (exists.Exists)
            {
                _elasticClient.DeleteIndex(Indices.Index(indexName));
            }
        }

        private StandardMetaDataResult GetStandardsTest()
        {
            return new StandardMetaDataResult
            {
                Standards = new List<StandardMetaData>
                {
                    new StandardMetaData
                    {
                        Id = 1,
                        Title = "Network Engineer",
                        StandardPdfUrl =
                            "https://www.gov.uk/government/uploads/system/uploads/attachment_data/file/370682/DI_-_Network_engineer_standard.ashx.pdf"
                    },
                    new StandardMetaData
                    {
                        Id = 2,
                        Title = "Software Developer",
                        StandardPdfUrl =
                            "https://www.gov.uk/government/uploads/system/uploads/attachment_data/file/371867/Digital_Industries_-_Software_Developer.pdf"
                    },
                    new StandardMetaData
                    {
                        Id = 61,
                        Title = "Dental Nurse",
                        NotionalEndLevel = 3,
                        StandardPdfUrl = "https://www.gov.uk/government/uploads/system/uploads/attachment_data/file/411720/DENTAL_HEALTH_-_Dental_Nurse.pdf",
                        TypicalLength = new TypicalLength() {From = 12, Unit = "m"}
                    }
                }
            };
        }

        private FrameworkMetaDataResult GetFrameworksTest()
        {
            return new FrameworkMetaDataResult
            {
                Frameworks = new List<FrameworkMetaData>
                {
                    new FrameworkMetaData
                    {
                        FworkCode = 403,
                        ProgType = 0,
                        PwayCode = 2,
                        PathwayName = "Baking Industry Skills",
                        NasTitle = "Food and Drink"
                    },
                    new FrameworkMetaData
                    {
                        FworkCode = 403,
                        ProgType = 3,
                        PwayCode = 7,
                        PathwayName = "Brewing Industry Skills",
                        NasTitle = "Food and Drink"
                    },
                    new FrameworkMetaData
                    {
                        FworkCode = 423,
                        ProgType = 2,
                        PwayCode = 4,
                        PathwayName = "Footwear",
                        NasTitle = "Fashion and Textiles"
                    }
                }
            };
        }
    }
}