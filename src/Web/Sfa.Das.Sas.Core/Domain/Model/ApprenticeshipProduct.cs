﻿using System.Collections.Generic;

namespace Sfa.Das.Sas.Core.Domain.Model
{
    public class ApprenticeshipProduct
    {
        public ApprenticeshipBasic Apprenticeship { get; set; }

        public double? EmployerSatisfaction { get; set; }

        public double? LearnerSatisfaction { get; set; }

        public string ProviderMarketingInfo { get; set; }

        public List<string> DeliveryModes { get; set; }
    }
}