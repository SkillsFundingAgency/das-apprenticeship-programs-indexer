using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    using System;

    public class FrameworkAimDocument : LarsDocument
    {
        public FrameworkAimDocument()
            : base(nameof(FrameworkAimDocument))
        {
        }

        public int FworkCode { get; set; }

        public int ProgType { get; set; }

        public int PwayCode { get; set; }

        public string LearnAimRef { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public int ApprenticeshipComponentType { get; set; }
    }
}
