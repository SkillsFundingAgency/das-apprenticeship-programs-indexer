using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    public class LarsDocument
    {
        public LarsDocument(string documentType)
        {
            DocumentType = documentType;
        }

        [Keyword]
        public string DocumentType { get; }
    }
}
