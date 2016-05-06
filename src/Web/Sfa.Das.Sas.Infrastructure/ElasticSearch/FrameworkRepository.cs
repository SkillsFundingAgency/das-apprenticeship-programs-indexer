﻿using System;
using System.Linq;
using Nest;
using Sfa.Das.Sas.ApplicationServices.Models;
using Sfa.Das.Sas.Core.Configuration;
using Sfa.Das.Sas.Core.Domain.Model;
using Sfa.Das.Sas.Core.Domain.Services;
using Sfa.Das.Sas.Core.Logging;
using Sfa.Das.Sas.Infrastructure.Mapping;

namespace Sfa.Das.Sas.Infrastructure.Elasticsearch
{
    using Sfa.Das.Sas.ApplicationServices;

    public sealed class FrameworkRepository : IGetFrameworks
    {
        private readonly IElasticsearchCustomClient _elasticsearchCustomClient;
        private readonly ILog _applicationLogger;
        private readonly IConfigurationSettings _applicationSettings;
        private readonly IFrameworkMapping _frameworkMapping;

        private readonly IProfileAStep _profiler;

        public FrameworkRepository(
            IElasticsearchCustomClient elasticsearchCustomClient,
            ILog applicationLogger,
            IConfigurationSettings applicationSettings,
            IFrameworkMapping frameworkMapping,
            IProfileAStep profiler)
        {
            _elasticsearchCustomClient = elasticsearchCustomClient;
            _applicationLogger = applicationLogger;
            _applicationSettings = applicationSettings;
            _frameworkMapping = frameworkMapping;
            _profiler = profiler;
        }

        public Framework GetFrameworkById(int id)
        {
            using (_profiler.CreateStep($"Get Framework {id} from index"))
            {
                var results =
                    _elasticsearchCustomClient.Search<FrameworkSearchResultsItem>(
                        s =>
                        s.Index(_applicationSettings.ApprenticeshipIndexAlias)
                            .Type(Types.Parse("frameworkdocument"))
                            .From(0)
                            .Size(1)
                            .Query(q => q.QueryString(qs => qs.Fields(fs => fs.Field(e => e.FrameworkId)).Query(id.ToString()))));

                if (results.ApiCall.HttpStatusCode != 200)
                {
                    throw new ApplicationException($"Failed query provider with id {id}");
                }

                var document = results.Documents.Any() ? results.Documents.First() : null;

                return document != null ? _frameworkMapping.MapToFramework(document) : null;
            }
        }
    }
}