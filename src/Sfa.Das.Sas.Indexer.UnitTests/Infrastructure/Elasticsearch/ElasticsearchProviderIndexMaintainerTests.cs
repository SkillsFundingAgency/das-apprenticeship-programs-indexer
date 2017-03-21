namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Elasticsearch
{
    using System;
    using Moq;
    using Nest;
    using NUnit.Framework;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Core.Exceptions;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;
    using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Configuration;
    using Sfa.Das.Sas.Indexer.Infrastructure.Provider.ElasticSearch;

    [TestFixture]
    public sealed class ElasticsearchProviderIndexMaintainerTests : BaseElasticIndexMaintainerTests
    {
        [Test]
        public void ShouldThrowAnExceptionIfCantCreateAnIndex()
        {
            var response = new StubResponse(400);

            MockElasticClient.Setup(x => x.CreateIndex(It.IsAny<IndexName>(), It.IsAny<Func<CreateIndexDescriptor, ICreateIndexRequest>>(), It.IsAny<string>())).Returns(response);

            var indexMaintainer = new ElasticsearchProviderIndexMaintainer(MockElasticClient.Object, Mock.Of<IElasticsearchMapper>(), Mock.Of<ILog>(), Mock.Of<IElasticsearchConfiguration>());

            Assert.Throws<ConnectionException>(() => indexMaintainer.CreateIndex("testindex"));
        }
    }
}
