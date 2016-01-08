﻿using System;
using System.Configuration;
using Nest;
using Sfa.Eds.Das.Web.Models;

namespace Sfa.Eds.Das.Web.Services
{
    public class SearchService : ISearchForStandards
    {
        public SearchResults Search(string keywords)
        {
            var searchHost = ConfigurationManager.AppSettings["SearchHost"];
            var node = new Uri(searchHost);

            var settings = new ConnectionSettings(
                node,
                defaultIndex: "elasticsearchmapperattachments-test");

            settings.MapDefaultTypeNames(d => d.Add(typeof(SearchResultsItem), "mydocument"));

            var client = new ElasticClient(settings);

            var results = client.Search<SearchResultsItem>(s => s
            .From(0)
            .Size(1000)
            .QueryString(keywords));

            return new SearchResults
            {
                TotalResults = results.Total,
                Results = results.Documents
            };
        }
    }
}