using Nest;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    using System;

    public class ApprenticeshipComponentTypeDocument : LarsDocument
    {
        public ApprenticeshipComponentTypeDocument()
            : base(nameof(ApprenticeshipComponentTypeDocument))
        {
        }

        [Keyword(NullValue = "null")]
        public int ApprenticeshipComponentType { get; set; }

        [Keyword(NullValue = "null")]
        public string ApprenticeshipComponentTypeDesc { get; set; }

        [Keyword(NullValue = "null")]
        public string ApprenticeshipComponentTypeDesc2 { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime EffectiveFrom { get; set; }

        [Keyword(NullValue = "null")]
        public DateTime? EffectiveTo { get; set; }
    }
}
