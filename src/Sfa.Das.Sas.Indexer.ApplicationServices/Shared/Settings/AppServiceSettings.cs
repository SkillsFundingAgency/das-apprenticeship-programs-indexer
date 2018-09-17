using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings
{
    public class AppServiceSettings : IAppServiceSettings
    {
        private readonly IProvideSettings _settings;

        public AppServiceSettings(IProvideSettings settingsProvider)
        {
            _settings = settingsProvider;
        }

        public string VstsGitStandardsFolderPath => _settings.GetSetting("VstsGitStandardsFolderPath");

        public string CsvFileNameStandards => _settings.GetSetting("CsvFileNameStandards");

        public string CsvFileNameFrameworks => _settings.GetSetting("CsvFileNameFrameworks");

        public string CsvFileNameFrameworksAim => _settings.GetSetting("CsvFileNameFrameworksAim");

        public string CsvFileNameApprenticeshipComponentType => _settings.GetSetting("CsvFileNameApprenticeshipComponentType");

        public string CsvFileNameLearningDelivery => _settings.GetSetting("CsvFileNameLearningDelivery");

        public string CsvFileNameFunding => _settings.GetSetting("CsvFileNameFunding");

        public string CsvFileNameApprenticeshipFunding => _settings.GetSetting("CsvFileNameApprenticeshipFunding");

        public string EnvironmentName => _settings.GetSetting("EnvironmentName");

        public string VstsGitGetFilesUrl => $"{VstsGitBaseUrl}/items?scopePath={VstsGitStandardsFolderPath}&recursionLevel=Full&api-version=2.0";

        public string VstsGitGetFrameworkFilesUrl => $"{VstsGitBaseUrl}/items?scopePath={VstsGitFrameworksFolderPath}&recursionLevel=Full&api-version=2.0";

        public string VstsGitGetFilesUrlFormat => VstsGitBaseUrl + "/items?scopePath={0}&recursionLevel=Full&api-version=2.0";

        public string VstsGitAllCommitsUrl => $"{VstsGitBaseUrl}/commits?api-version=1.0&$top=1";

        public string VstsGitPushUrl => $"{VstsGitBaseUrl}/pushes?api-version=2.0-preview";

        public string VstsAssessmentOrgsUrl => _settings.GetSetting("VstsAssessmentOrgsUrl");

        public string VstsRoatpUrl => _settings.GetSetting("VstsRoatpUrl");

        public string GitUsername => _settings.GetSetting("GitUsername");

        public string GitPassword => _settings.GetSetting("GitPassword");

        public string GitBranch => _settings.GetSetting("GitBranch");

        public string ConnectionString => _settings.GetSetting("StorageConnectionString");

        public string ImServiceBaseUrl => _settings.GetSetting("ImServiceBaseUrl");

        public string ImServiceUrl => _settings.GetSetting("ImServiceUrl");

        public string ImServiceLinkText => _settings.GetSetting("ImServiceLinkText");

        public string GovWebsiteUrl => _settings.GetSetting("GovWebsiteUrl");

        public string MetadataApiUri => CloudConfigurationManager.GetSetting("MetadataApiUri");

        public string ProviderFeedbackApiUri
        {
            get
            {
                var uri = _settings.GetSetting("ProviderFeedbackApiUri");
                return uri.EndsWith("/") ? uri : uri += "/";
            }
        }

        private string VstsGitBaseUrl => _settings.GetSetting("VstsGitBaseUrl");

        public List<string> FrameworksExpiredRequired => GetFrameworksList(_settings.GetNullableSetting("FrameworksExpiredRequired"));

        private List<string> GetFrameworksList(string frameworkIdList)
        {
            return !string.IsNullOrWhiteSpace(frameworkIdList) ? frameworkIdList.Split(',').Select(frameworkId => frameworkId.Trim()).ToList() : new List<string>();
        }

        private string VstsGitFrameworksFolderPath => _settings.GetSetting("VstsGitFrameworksFolderPath");

        public string QueueName(Type type)
        {
            var name = $"{TypeToName(type)}.QueueName";
            return _settings.GetSetting(name).ToLower();
        }

        public string[] MonitoringUrl(Type type)
        {
            var name = $"{TypeToName(type)}.MonitoringUrl";
            var value = _settings.GetNullableSetting(name);
            if (string.IsNullOrEmpty(value))
            {
                return new string[] { };
            }

            return value.Split(';');
        }

        private static string TypeToName(Type type)
        {
            return type.Name.Replace("IMaintain", string.Empty).Replace("Index", string.Empty);
        }
    }
}