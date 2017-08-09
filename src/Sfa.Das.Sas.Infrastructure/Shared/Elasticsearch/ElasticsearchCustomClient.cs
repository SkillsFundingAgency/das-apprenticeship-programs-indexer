using System.Net;
using System.Web;
using Elasticsearch.Net;
using Sfa.Das.Sas.Indexer.Core.Exceptions;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Nest;
    using Sfa.Das.Sas.Indexer.Core.Logging.Models;

    public class ElasticsearchCustomClient : IElasticsearchCustomClient
    {
        private readonly ILog _logger;

        private readonly IElasticClient _client;

        public ElasticsearchCustomClient(IElasticsearchClientFactory elasticsearchClientFactory, ILog logger)
        {
            _client = elasticsearchClientFactory.GetElasticClient();
            _logger = logger;
        }

        public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector, [CallerMemberName] string callerName = "")
            where T : class
        {
            var timer = Stopwatch.StartNew();
            var result = _client.Search(selector);
            ValidateResponse(result);
            SendLog(result.ApiCall, result.Took, timer.ElapsedMilliseconds, $"Elasticsearch.Search.{callerName}");
            return result;
        }

        public IExistsResponse IndexExists(IndexName index, [CallerMemberName] string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.IndexExists(index);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.IndexExists.{callerName}");
            return result;
        }

        public IDeleteIndexResponse DeleteIndex(IndexName index, [CallerMemberName] string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.DeleteIndex(index);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.DeleteIndex.{callerName}");
            return result;
        }

        public IGetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null, [CallerMemberName] string callerName = "")
            where T : class
        {
            var timer = Stopwatch.StartNew();
            var result = _client.GetMapping(selector);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.GetMapping.{callerName}");
            return result;
        }

        public IRefreshResponse Refresh(IRefreshRequest request, [CallerMemberName] string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.Refresh(request);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.Refresh.{callerName}");
            return result;
        }

        public IRefreshResponse Refresh(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector = null, string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.Refresh(indices);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.Refresh.{callerName}");
            return result;
        }

        public IExistsResponse AliasExists(Func<AliasExistsDescriptor, IAliasExistsRequest> selector, string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.AliasExists(selector);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.AliasExists.{callerName}");
            return result;
        }

        public IBulkAliasResponse Alias(Func<BulkAliasDescriptor, IBulkAliasRequest> selector, string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.Alias(selector);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.Alias.{callerName}");
            return result;
        }

        public IBulkAliasResponse Alias(IBulkAliasRequest request, string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.Alias(request);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.Alias.{callerName}");
            return result;
        }

        public IIndicesStatsResponse IndicesStats(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null, string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.IndicesStats(indices, selector);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.IndicesStats.{callerName}");
            return result;
        }

        public IList<string> GetIndicesPointingToAlias(string aliasName, string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.GetIndicesPointingToAlias(aliasName);
            SendLog(null, null, timer.ElapsedMilliseconds, $"Elasticsearch.GetIndicesPointingToAlias.{callerName}");
            return result.ToList();
        }

        public ICreateIndexResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null, string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.CreateIndex(index, selector);
            ValidateResponse(result);
            SendLog(result.ApiCall, null, timer.ElapsedMilliseconds, $"Elasticsearch.CreateIndex.{callerName}");
            return result;
        }

        public virtual Task<IBulkResponse> BulkAsync(IBulkRequest request, string callerName = "")
        {
            var timer = Stopwatch.StartNew();
            var result = _client.BulkAsync(request);
            SendLog(null, null, timer.ElapsedMilliseconds, $"Elasticsearch.BulkAsync.{callerName}");
            return result;
        }

        private void SendLog(IApiCallDetails apiCallDetails, long? took, double networkTime, string identifier)
        {
            string body = string.Empty;
            if (apiCallDetails?.RequestBodyInBytes != null)
            {
                body = System.Text.Encoding.Default.GetString(apiCallDetails.RequestBodyInBytes);
            }

            var logEntry = new ElasticSearchLogEntry
            {
                ReturnCode = apiCallDetails?.HttpStatusCode,
                SearchTime = took,
                NetworkTime = networkTime,
                Url = apiCallDetails?.Uri?.AbsoluteUri,
                Body = body
            };

            _logger.Debug($"ElasticsearchQuery: {identifier}", logEntry);
        }

        private void ValidateResponse(IBodyWithApiCallDetails response)
        {
            var status = response?.ApiCall?.HttpStatusCode;
            if (status == null)
            {
                throw new ConnectionException($"The response to elastic search was not 200", response?.ApiCall?.OriginalException);
            }

            switch (status.Value)
            {
                case (int) HttpStatusCode.OK:
                    return;
                case (int) HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException("The request to elasticsearch was unauthorised", response.ApiCall.OriginalException);
                default:
                    throw new HttpException(status.Value, response.ApiCall.DebugInformation, response.ApiCall.OriginalException);
            }
        }
    }
}