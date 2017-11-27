﻿using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.Core.Models.Provider;

namespace Sfa.Das.Sas.Indexer.Core.Provider.Models.Provider
{
    public sealed class FrameworkInformation : IApprenticeshipInformation
    {
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the ProgType.
        /// </summary>
        /// <remarks>The level can be mapped from this value.</remarks>
        public int ProgType { get; set; }

        public int PathwayCode { get; set; }

        public string InfoUrl { get; set; }

        public ContactInformation ContactInformation { get; set; }

        public string MarketingInfo { get; set; }

        public IEnumerable<DeliveryInformation> DeliveryLocations { get; set; }

        public double? OverallAchievementRate { get; set; }

        public double? NationalOverallAchievementRate { get; set; }

        public string OverallCohort { get; set; }
    }
}