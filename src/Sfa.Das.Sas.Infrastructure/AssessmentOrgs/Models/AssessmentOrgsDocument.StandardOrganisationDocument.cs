using System;

namespace Sfa.Das.Sas.Indexer.Infrastructure.AssessmentOrgs.Models
{
    public partial class AssessmentOrgsDocument
    {
        public string StandardCode { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}
