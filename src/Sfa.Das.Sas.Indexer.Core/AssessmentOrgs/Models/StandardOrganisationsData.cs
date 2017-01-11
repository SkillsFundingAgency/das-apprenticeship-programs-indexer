using System;
using System.Runtime.InteropServices.ComTypes;

namespace Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models
{
    public class StandardOrganisationsData
    {
        public string EpaOrganisationIdentifier { get; set; }

        public string StandardCode { get; set; }

        public DateTime EffectiveFrom { get; set; }
    }
}
