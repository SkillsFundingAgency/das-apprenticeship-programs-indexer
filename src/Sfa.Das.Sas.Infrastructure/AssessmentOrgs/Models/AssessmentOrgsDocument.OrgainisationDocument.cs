using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models
{
    public partial class AssessmentOrgsDocument
    {
        public long? Ukprn { get; set; }

        [Keyword(NullValue = "null")]
        public string EpaOrganisationIdentifierKeyword { get; set; }
    }
}
