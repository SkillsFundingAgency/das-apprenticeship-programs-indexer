using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models
{
    public class AssessmentOrgsDocument
    {
        public AssessmentOrgsDocument(string documentType)
        {
            DocumentType = documentType;
        }

        [Keyword]
        public string DocumentType { get; }
    }
}
