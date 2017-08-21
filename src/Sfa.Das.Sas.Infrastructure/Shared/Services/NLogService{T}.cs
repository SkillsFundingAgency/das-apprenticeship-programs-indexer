namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;

    public class NLogService<T> : NLogLogger
    {
        private readonly string _postfix = System.Configuration.ConfigurationManager.AppSettings["LoggingNamePostfix"];

        public NLogService()
            : base(null, null, GetProperties())
        {
            if (typeof(T) == typeof(IMaintainApprenticeshipIndex))
            {
                ApplicationName = "das-apprenticeship-programmes-indexer" + _postfix;
            }

            if (typeof(T) == typeof(IMaintainLarsIndex))
            {
                ApplicationName = "das-lars-indexer" + _postfix;
            }

            if (typeof(T) == typeof(IMaintainProviderIndex))
            {
                ApplicationName = "das-provider-indexer" + _postfix;
            }

            if (typeof(T) == typeof(IMaintainAssessmentOrgsIndex))
            {
                ApplicationName = "das-epao-indexer" + _postfix;
            }
        }

        private static IDictionary<string, object> GetProperties()
        {
            var properties = new Dictionary<string, object>();
            properties.Add("Version", GetVersion());
            return properties;
        }

        private static string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}
