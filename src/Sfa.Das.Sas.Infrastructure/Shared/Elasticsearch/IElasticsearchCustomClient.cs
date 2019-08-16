using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Nest;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    public interface IElasticsearchCustomClient
    {
        ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> selector, [CallerMemberName] string callerName = "")
            where T : class;

        ExistsResponse IndexExists(IndexName index, [CallerMemberName] string callerName = "");

        DeleteIndexResponse DeleteIndex(IndexName index, [CallerMemberName] string callerName = "");

        GetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, IGetMappingRequest> selector = null, [CallerMemberName] string callerName = "")
            where T : class;

        RefreshResponse Refresh(IRefreshRequest request, [CallerMemberName] string callerName = "");

        RefreshResponse Refresh(Indices indices, Func<RefreshDescriptor, IRefreshRequest> selector = null, [CallerMemberName] string callerName = "");

        ExistsResponse AliasExists(string aliasName, [CallerMemberName] string callerName = "");

        BulkAliasResponse Alias(Func<BulkAliasDescriptor, IBulkAliasRequest> selector, [CallerMemberName] string callerName = "");

        BulkAliasResponse Alias(IBulkAliasRequest request, [CallerMemberName] string callerName = "");

        IndicesStatsResponse IndicesStats(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null, [CallerMemberName] string callerName = "");

        IList<string> GetIndicesPointingToAlias(string aliasName, [CallerMemberName] string callerName = "");

        CreateIndexResponse CreateIndex(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest> selector = null, [CallerMemberName] string callerName = "");

        Task<BulkResponse> BulkAsync(IBulkRequest request, [CallerMemberName] string callerName = "");

        void BulkAllGeneric<T>(List<T> elementList, string indexName)
            where T : class;
		void IndexMany<T>(List<T> entries, string indexName)
			where T : class;

	}
}