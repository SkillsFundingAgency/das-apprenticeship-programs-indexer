namespace Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models.Standard;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

    public class LarsData
    {
        public IEnumerable<LarsStandard> Standards { get; set; }

        public IEnumerable<FrameworkMetaData> Frameworks { get; set; }

        public IEnumerable<FrameworkAimMetaData> FrameworkAimMetaData { get; set; }

        public IEnumerable<FrameworkComponentTypeMetaData> FrameworkComponentTypeMetaData { get; set; }

        public IEnumerable<LearningDeliveryMetaData> LearningDeliveryMetaData { get; set; }

        public IEnumerable<FundingMetaData> FundingMetaData { get; set; }
    }
}