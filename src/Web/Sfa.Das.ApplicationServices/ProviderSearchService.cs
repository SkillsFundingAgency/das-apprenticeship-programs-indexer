﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Sfa.Das.ApplicationServices.Exceptions;
using Sfa.Das.ApplicationServices.Models;
using Sfa.Eds.Das.Core.Domain.Services;
using Sfa.Eds.Das.Core.Logging;

namespace Sfa.Das.ApplicationServices
{
    using System.Collections.Generic;

    public sealed class ProviderSearchService : IProviderSearchService
    {
        private readonly ISearchProvider _searchProvider;
        private readonly IGetStandards _getStandards;
        private readonly IGetFrameworks _getFrameworks;
        private readonly ILookupLocations _postCodeLookup;
        private readonly ILog _logger;

        public ProviderSearchService(
            ISearchProvider searchProvider,
            IGetStandards getStandards,
            IGetFrameworks getFrameworks,
            ILookupLocations postcodeLookup,
            ILog logger)
        {
            _searchProvider = searchProvider;
            _getStandards = getStandards;
            _getFrameworks = getFrameworks;
            _postCodeLookup = postcodeLookup;
            _logger = logger;
        }

        public async Task<ProviderStandardSearchResults> SearchByStandardPostCode(int standardId, string postCode)
        {
            if (string.IsNullOrEmpty(postCode))
            {
                return new ProviderStandardSearchResults { StandardId = standardId, PostCodeMissing = true };
            }

            string standardName = string.Empty;

            try
            {
                standardName = _getStandards.GetStandardById(standardId)?.Title;

                var coordinates = await _postCodeLookup.GetLatLongFromPostCode(postCode);

                if (coordinates == null)
                {
                    return new ProviderStandardSearchResults
                    {
                        TotalResults = 0,
                        StandardId = standardId,
                        StandardName = standardName,
                        PostCode = postCode,
                        Hits = new IApprenticeshipProviderSearchResultsItem[0],
                        HasError = false
                    };
                }
                else
                {
                    _logger.Info($"Provider Location Search: {postCode}, {coordinates}", new Dictionary<string, object> { { "postCode", postCode }, { "coordinates", new double[] { coordinates.Lon, coordinates.Lat } } });

                    var searchResults = _searchProvider.SearchByStandardLocation(standardId, coordinates);

                    var result = new ProviderStandardSearchResults
                    {
                        TotalResults = searchResults.Total,
                        StandardId = standardId,
                        StandardName = standardName,
                        PostCode = postCode,
                        Hits = searchResults.Hits
                    };

                    return result;
                }
            }
            catch (SearchException ex)
            {
                _logger.Error(ex, "Search for Provider failed.");

                return new ProviderStandardSearchResults
                {
                    TotalResults = 0,
                    StandardId = standardId,
                    StandardName = standardName,
                    PostCode = postCode,
                    Hits = new IApprenticeshipProviderSearchResultsItem[0],
                    HasError = true
                };
            }
        }

        public async Task<ProviderFrameworkSearchResults> SearchByFrameworkPostCode(int frameworkId, string postCode)
        {
            if (string.IsNullOrEmpty(postCode))
            {
                return new ProviderFrameworkSearchResults { FrameworkId = frameworkId, PostCodeMissing = true };
            }

            try
            {
                var framework = _getFrameworks.GetFrameworkById(frameworkId);

                var coordinates = await _postCodeLookup.GetLatLongFromPostCode(postCode);

                IEnumerable<IApprenticeshipProviderSearchResultsItem> hits;
                var total = 0L;

                if (coordinates != null)
                {
                    var searchResults = _searchProvider.SearchByFrameworkLocation(frameworkId, coordinates);
                    hits = searchResults.Hits;
                    total = searchResults.Total;
                }
                else
                {
                    hits = new IApprenticeshipProviderSearchResultsItem[0];
                }

                return new ProviderFrameworkSearchResults
                {
                    Title = framework?.Title,
                    TotalResults = total,
                    FrameworkId = frameworkId,
                    FrameworkCode = framework?.FrameworkCode ?? 0,
                    FrameworkName = framework?.FrameworkName,
                    PathwayName = framework?.PathwayName,
                    FrameworkLevel = framework?.Level ?? 0,
                    PostCode = postCode,
                    Hits = hits
                };
            }
            catch (SearchException ex)
            {
                _logger.Error(ex, "Search for Provider failed.");

                return new ProviderFrameworkSearchResults
                {
                    TotalResults = 0,
                    FrameworkId = frameworkId,
                    FrameworkName = string.Empty,
                    PathwayName = string.Empty,
                    PostCode = postCode,
                    Hits = new IApprenticeshipProviderSearchResultsItem[0],
                    HasError = true
                };
            }
        }
    }
}
