﻿using System;
using Moq;
using Nest;
using NUnit.Framework;

using Sfa.Eds.Das.Indexer.Core.Exceptions;
using Sfa.Eds.Das.Indexer.Core.Services;
using Sfa.Infrastructure.Elasticsearch;

namespace Sfa.Infrastructure.UnitTests.Elasticsearch
{
    [TestFixture]
    public sealed class ElasticsearchProviderIndexMaintainerTests : BaseElasticIndexMaintainerTests
    {
        [Test]
        [ExpectedException(typeof(ConnectionException))]
        public void ShouldThrowAnExceptionIfCantCreateAnIndex()
        {
            var response = new StubResponse(400);

            MockElasticClient.Setup(x => x.CreateIndex(It.IsAny<IndexName>(), It.IsAny<Func<CreateIndexDescriptor, ICreateIndexRequest>>(), It.IsAny<string>())).Returns(response);

            var indexMaintainer = new ElasticsearchProviderIndexMaintainer(MockElasticClient.Object, Mock.Of<IElasticsearchMapper>(), Mock.Of<ILog>());

            indexMaintainer.CreateIndex("testindex");
        }
    }
}