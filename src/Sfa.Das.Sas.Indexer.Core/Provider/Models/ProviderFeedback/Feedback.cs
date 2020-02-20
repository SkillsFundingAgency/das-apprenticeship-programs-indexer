using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models.ProviderFeedback
{
    public class Feedback
    {
        public ICollection<ProviderAttribute> Strengths { get; set; } = new List<ProviderAttribute>();
        public ICollection<ProviderAttribute> Weaknesses { get; set; } = new List<ProviderAttribute>();
        public int ExcellentFeedbackCount { get; set; }
        public int GoodFeedbackCount { get; set; }
        public int PoorFeedbackCount { get; set; }
        public int VeryPoorFeedbackCount { get; set; }
    }
}
