using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch
{
    public sealed class Address
    {
        [Keyword]
        public string Address1 { get; set; }

        [Keyword]
        public string Address2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string PostCode { get; set; }
    }
}
