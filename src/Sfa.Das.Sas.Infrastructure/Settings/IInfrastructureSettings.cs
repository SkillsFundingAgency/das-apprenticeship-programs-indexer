﻿using System;
using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Settings
{
    public interface IInfrastructureSettings
    {
        string CourseDirectoryUri { get; }

        string EnvironmentName { get; }

        IEnumerable<Uri> ElasticServerUrls { get; }

        string ApplicationName { get; }

        string AchievementRateDataBaseConnectionString { get; }

        string FrameworkIdFormat { get; }

        string UkrlpStakeholderId { get; }

        string UkrlpProviderStatus { get; }

        string UkrlpQueryId { get; }

        int UkrlpRequestUkprnBatchSize { get; }
        double HttpClientTimeout { get; }
    }
}