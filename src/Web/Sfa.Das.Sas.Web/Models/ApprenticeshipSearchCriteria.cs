﻿namespace Sfa.Das.Sas.Web.Models
{
    using System.Collections.Generic;

    public sealed class ApprenticeshipSearchCriteria
    {
        public string Keywords { get; set; }

        public int Page { get; set; }

        public int Take { get; set; }

        public int Order { get; set; }

        public List<int> SelectedLevels { get; set; }
    }
}