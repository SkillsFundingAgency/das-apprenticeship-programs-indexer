using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Models.ElasticSearch
{
    public class ProviderDocument
    {
        public ProviderDocument(string documentType)
        {
            DocumentType = documentType;
        }

        [Keyword]
        public string DocumentType { get; }
    }
}
