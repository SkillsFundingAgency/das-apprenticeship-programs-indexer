namespace Sfa.Das.Sas.Indexer.Infrastructure.Lars.Models
{
    using System;

    public class LearningDeliveryMetaDataDocument
    {
        public string LearnAimRef { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public string LearnAimRefTitle { get; set; }

        public int LearnAimRefType { get; set; }
    }
}
