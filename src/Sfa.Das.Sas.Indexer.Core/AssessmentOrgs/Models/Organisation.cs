namespace Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models
{
    public class Organisation
    {
        public string EpaOrganisationIdentifier { get; set; }

        public string EpaOrganisation { get; set; }

        public string OrganisationType { get; set; }

        public string WebsiteLink { get; set; }

        public Address Address { get; set; }
    }
}
