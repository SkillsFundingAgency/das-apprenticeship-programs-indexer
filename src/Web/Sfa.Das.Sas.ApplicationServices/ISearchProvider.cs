﻿using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Domain.Model;

namespace Sfa.Das.Sas.ApplicationServices
{
    using System.Collections.Generic;

    public interface ISearchProvider
    {
        ApprenticeshipSearchResults SearchByKeyword(string keywords, int page, int take, int order, List<int> selectedLevels);

        SearchResult<StandardProviderSearchResultsItem> SearchByStandardLocation(int standardId, Coordinate testCoordinates, int page, int take, IEnumerable<string> deliveryModes);

        SearchResult<FrameworkProviderSearchResultsItem> SearchByFrameworkLocation(int standardId, Coordinate testCoordinates, int page, int take, IEnumerable<string> deliveryModes);
    }
}