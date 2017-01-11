using System;

namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models
{
    public class StandardOrganisationDocument
    {
        public string EpaOrganisationIdentifier { get; set; }

        public string StandardCode { get; set; }

        public DateTime EffectiveFrom { get; set; }
    }
}
