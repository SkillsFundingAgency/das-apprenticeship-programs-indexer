using Nest;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models
{
    public partial class AssessmentOrgsDocument
    {
        public AssessmentOrgsDocument(string documentType)
        {
            DocumentType = documentType;
        }

        public string EpaOrganisation { get; set; }

        public string OrganisationType { get; set; }

        public string WebsiteLink { get; set; }

        [Keyword]
        public string DocumentType { get; }

        public string EpaOrganisationIdentifier { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }
    }
}
