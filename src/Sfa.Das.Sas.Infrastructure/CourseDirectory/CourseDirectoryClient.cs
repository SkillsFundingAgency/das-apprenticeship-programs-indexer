﻿using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.CourseDirectory;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Logging.Models;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

    public sealed class CourseDirectoryClient : IGetCourseDirectoryProviders
    {
        private readonly IInfrastructureSettings _settings;
        private readonly ICourseDirectoryProviderDataService _courseDirectoryProviderDataService;

        private readonly ILog _logger;

        public CourseDirectoryClient(
            IInfrastructureSettings settings,
            ICourseDirectoryProviderDataService courseDirectoryProviderDataService,
            ILog logger)
        {
            _settings = settings;
            _courseDirectoryProviderDataService = courseDirectoryProviderDataService;
            _logger = logger;
        }

        public async Task<IEnumerable<Provider>> GetApprenticeshipProvidersAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            _courseDirectoryProviderDataService.BaseUri = new Uri(_settings.CourseDirectoryUri);
            var responseAsync = await _courseDirectoryProviderDataService.BulkprovidersWithOperationResponseAsync();

            _logger.Debug(
    "CourseDirectory.GetApprenticeshipProvidersAsync",
    new TimingLogEntry { ElaspedMilliseconds = stopwatch.Elapsed.TotalMilliseconds });

            _courseDirectoryProviderDataService.Dispose();

            return responseAsync.Body;
        }
    }
}