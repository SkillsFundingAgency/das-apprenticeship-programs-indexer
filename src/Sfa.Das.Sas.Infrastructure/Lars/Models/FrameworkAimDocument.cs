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

        [Keyword(NullValue = "null")]
        public int FworkCode { get; set; }

        [Keyword(NullValue = "null")]
        public int ProgType { get; set; }

        [Keyword(NullValue = "null")]
        public int PwayCode { get; set; }

        [Keyword(NullValue = "null")]
        public string LearnAimRef { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime EffectiveFrom { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime? EffectiveTo { get; set; }

        [Keyword(NullValue = "null")]
        public int ApprenticeshipComponentType { get; set; }
    }
}
