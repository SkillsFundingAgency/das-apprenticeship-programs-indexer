namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Nest;

    public class BulkProviderClient
    {
        private readonly List<Task<BulkResponse>> _tasks;
        private readonly string _indexName;
        private readonly IElasticsearchCustomClient _client;
        private int _count;
        private BulkDescriptor _bulkDescriptor;

        public BulkProviderClient(string indexName, IElasticsearchCustomClient client)
        {
            _bulkDescriptor = CreateBulkDescriptor(indexName);
            _tasks = new List<Task<BulkResponse>>();
            _indexName = indexName;
            _client = client;
        }

        public BulkDescriptor Create<T>(Func<BulkCreateDescriptor<T>, IBulkCreateOperation<T>> bulkCreateSelector)
            where T : class
        {
            _bulkDescriptor.Create(bulkCreateSelector);
            _count++;
            if (HaveReachedBatchLimit(_count))
            {
                _tasks.Add(_client.BulkAsync(_bulkDescriptor));

                _bulkDescriptor = CreateBulkDescriptor(_indexName);

                _count = 0;
            }

            return _bulkDescriptor;
        }

        public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, IBulkIndexOperation<T>> bulkCreateSelector)
            where T : class
        {
            _bulkDescriptor.Index(bulkCreateSelector);
            _count++;
            if (HaveReachedBatchLimit(_count))
            {
                _tasks.Add(_client.BulkAsync(_bulkDescriptor));

                _bulkDescriptor = CreateBulkDescriptor(_indexName);

                _count = 0;
            }

            return _bulkDescriptor;
        }

        public List<Task<BulkResponse>> GetTasks()
        {
            if (_count > 0)
            {
                _tasks.Add(_client.BulkAsync(_bulkDescriptor));
            }

            return _tasks;
        }

        private static BulkDescriptor CreateBulkDescriptor(string indexName)
        {
            var bulkDescriptor = new BulkDescriptor();
            bulkDescriptor.Index(indexName);

            return bulkDescriptor;
        }

        private static bool HaveReachedBatchLimit(int count)
        {
            const int BatchSize = 1000;

            return count >= BatchSize;
        }
    }
}
