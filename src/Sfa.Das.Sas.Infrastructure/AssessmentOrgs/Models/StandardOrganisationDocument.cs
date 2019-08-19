using System;
using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models
{
    public class StandardOrganisationDocument : AssessmentOrgsDocument
    {
        public StandardOrganisationDocument()
            : base(nameof(StandardOrganisationDocument))
        {
        }

        public string EpaOrganisationIdentifier { get; set; }

        public string StandardCode { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }

        public string EpaOrganisation { get; set; }

        public string OrganisationType { get; set; }

        public string WebsiteLink { get; set; }
    }
}
