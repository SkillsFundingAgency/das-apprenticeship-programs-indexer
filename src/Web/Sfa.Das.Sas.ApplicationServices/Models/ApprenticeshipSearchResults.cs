﻿using System.Collections.Generic;

namespace Sfa.Das.Sas.ApplicationServices.Models
{
    public sealed class ApprenticeshipSearchResults
    {
        public double TotalResults { get; set; }

        public int ResultsToTake { get; set; }

        public int ActualPage { get; set; }

        public int LastPage { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<ApprenticeshipSearchResultsItem> Results { get; set; }

        public bool HasError { get; set; }
    }
}