using System;
using System.Runtime.InteropServices.ComTypes;

namespace Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models
{
    public class StandardOrganisationsData
    {
        public string EpaOrganisationIdentifier { get; set; }

        public string StandardCode { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }

        public string EpaOrganisation { get; set; }

        public string OrganisationType { get; set; }

        public string WebsiteLink { get; set; }
    }
}
