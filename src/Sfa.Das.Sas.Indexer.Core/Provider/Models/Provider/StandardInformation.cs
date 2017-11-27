using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models.Provider
{
    public sealed class StandardInformation : IApprenticeshipInformation
    {
        public int Code { get; set; }

        public ContactInformation ContactInformation { get; set; }

        public string MarketingInfo { get; set; }

        public string InfoUrl { get; set; }

        public IEnumerable<DeliveryInformation> DeliveryLocations { get; set; }

        public double? OverallAchievementRate { get; set; }

        public string OverallCohort { get; set; }

        public double? NationalOverallAchievementRate { get; set; }
    }
}