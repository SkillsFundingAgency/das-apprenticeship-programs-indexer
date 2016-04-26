﻿namespace Sfa.Das.Sas.Indexer.Infrastructure.Elasticsearch.Models
{
    public sealed class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
    }
}
