using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Azure;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Settings
{
    public class InfrastructureSettings : IInfrastructureSettings
    {
        private readonly IProvideSettings _settingsProvider;

        public InfrastructureSettings(IProvideSettings settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public string ElasticsearchUsername => _settingsProvider.GetSetting("ElasticsearchUsername");

        public string ElasticsearchPassword => _settingsProvider.GetSetting("ElasticsearchPassword");

        public string FrameworkIdFormat => CloudConfigurationManager.GetSetting("FrameworkIdFormat");

        public string UkrlpStakeholderId => CloudConfigurationManager.GetSetting("UkrlpStakeholderId");

        public string UkrlpProviderStatus => CloudConfigurationManager.GetSetting("UkrlpProviderStatus");

        public string UkrlpServiceEndpointUrl => _settingsProvider.GetSetting("UKRLP_EndpointUri");

        public string CourseDirectoryUri => CloudConfigurationManager.GetSetting("CourseDirectoryUri");

        public string UkrlpEndpointName => CloudConfigurationManager.GetSetting("UkrlpEndpointName");

        public string EnvironmentName => CloudConfigurationManager.GetSetting("EnvironmentName");

        public string ApplicationName => CloudConfigurationManager.GetSetting("ApplicationName");

        public double HttpClientTimeout => Convert.ToDouble(ConfigurationManager.AppSettings["HttpClient.Timeout"]);

        public string EstablishmentUsername => _settingsProvider.GetNullableSetting("EdubaseUsername");

        public string EstablishmentPassword => _settingsProvider.GetNullableSetting("EdubasePassword");

        public string EmployerSatisfactionRatesTableName => _settingsProvider.GetSetting("EmployerSatisfactionRatesTableName");

        public string LearnerSatisfactionRatesTableName => _settingsProvider.GetSetting("LearnerSatisfactionRatesTableName");

        public string AchievementRateDataBaseConnectionString => _settingsProvider.GetSetting("AchievementRateDataBaseConnectionString");

        public IEnumerable<Uri> ElasticServerUrls => GetElasticIPs("ElasticServerUrls");

        public bool UseStoredProc => Convert.ToBoolean(_settingsProvider.GetSetting("FEChoicesUseStoredProc"));

        public IEnumerable<Uri> GetElasticIPs(string appSetting)
        {
            var urlsString = _settingsProvider.GetSetting(appSetting).Split(',');

            return urlsString.Select(url => new Uri(url));
        }
    }
}