﻿using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings
{
    using System;

    public interface IAppServiceSettings
    {
        string CsvFileNameStandards { get; }

        string CsvFileNameFrameworks { get; }

        string CsvFileNameFrameworksAim { get; }

        string CsvFileNameApprenticeshipComponentType { get; }

        string CsvFileNameLearningDelivery { get; }

        string CsvFileNameApprenticeshipFunding { get; }

        string CsvFileNameFunding { get; }

        string GitUsername { get; }

        string GitPassword { get; }

        string GitBranch { get; }

        string VstsGitStandardsFolderPath { get; }

        string VstsGitGetFilesUrl { get; }

        string VstsGitGetFrameworkFilesUrl { get; }

        string VstsGitGetFilesUrlFormat { get; }

        string VstsGitAllCommitsUrl { get; }

        string VstsGitPushUrl { get; }

        string VstsAssessmentOrgsUrl { get; }

        string VstsRoatpUrl { get; }

        string EnvironmentName { get; }

        string ConnectionString { get; }

        string ImServiceBaseUrl { get; }

        string ImServiceUrl { get; }
        string ImServiceLinkText { get; }

        string GovWebsiteUrl { get; }

        string MetadataApiUri { get; }

        string ProviderFeedbackApiUri { get; }

        List<string> FrameworksExpiredRequired { get; }

        string RoatpApiClientBaseUrl { get; }
        string RoatpApiAuthenticationInstance { get; }
        string RoatpApiAuthenticationTenantId { get; }
        string RoatpApiAuthenticationClientId { get; }
        string RoatpApiAuthenticationClientSecret { get; }
        string RoatpApiAuthenticationResourceId { get; }
        string RoatpApiAuthenticationApiBaseAddress { get; }

        string QueueName(Type type);
        string[] MonitoringUrl(Type type);
    }
}
