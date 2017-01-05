using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models
{
    public class AssessmentOrganisationsDTO
    {
        public List<Organisation> Organisations { get; set; }

        public List<StandardOrganisationsData> StandardOrganisationsData { get; set; }
    }
}
