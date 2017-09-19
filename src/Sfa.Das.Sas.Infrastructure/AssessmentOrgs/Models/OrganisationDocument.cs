using Nest;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models
{
    public class OrganisationDocument
    {
        public string EpaOrganisationIdentifier { get; set; }

        public string EpaOrganisation { get; set; }

        public string OrganisationType { get; set; }

        public string WebsiteLink { get; set; }

        public Address Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [Keyword(NullValue = "null")]
        public string EpaOrganisationIdentifierKeyword => EpaOrganisationIdentifier;
    }
}
