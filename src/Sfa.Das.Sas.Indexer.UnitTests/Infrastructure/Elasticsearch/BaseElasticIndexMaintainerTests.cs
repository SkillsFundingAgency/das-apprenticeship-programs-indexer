﻿using System;
using Elasticsearch.Net;
using Moq;
using Nest;
using NUnit.Framework;
using Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch;

namespace Sfa.Das.Sas.Indexer.UnitTests.Infrastructure.Elasticsearch
{
    public class BaseElasticIndexMaintainerTests
    {
        protected Mock<IElasticsearchCustomClient> MockElasticClient { get; private set; }

        [SetUp]
        public virtual void Setup()
        {
            MockElasticClient = new Mock<IElasticsearchCustomClient>();
        }

        internal class StubResponse : ICreateIndexResponse
        {
            private readonly IApiCallDetails _apiCallDetails;

            public StubResponse(int statusCode = 200)
            {
                var mockApiCallDetails = new Mock<IApiCallDetails>();
                mockApiCallDetails.SetupGet(x => x.HttpStatusCode).Returns(statusCode);
                _apiCallDetails = mockApiCallDetails.Object;
            }

            public bool Acknowledged
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public IApiCallDetails ApiCall
            {
                get
                {
                    return _apiCallDetails;
                }
                set { throw new NotImplementedException(); }
            }

            public IApiCallDetails CallDetails
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public string DebugInformation
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public bool IsValid
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public Exception OriginalException
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public ServerError ServerError
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            //IApiCallDetails IBodyWithApiCallDetails.ApiCall { get; set; }
            public bool ShardsAcknowledged { get; }
        }
    }
}
