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

        public int ApprenticeshipComponentType { get; set; }

        public string ApprenticeshipComponentTypeDesc { get; set; }

        public string ApprenticeshipComponentTypeDesc2 { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }
    }
}
