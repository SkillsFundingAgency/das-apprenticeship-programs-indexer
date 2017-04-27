using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services
{
    using System;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;

    public class NLogService<T> : NLogLogger
    {
        public NLogService(Type loggerType)
            : base(loggerType, null, GetProperties())
        {
            if (typeof(T) == typeof(IMaintainApprenticeshipIndex))
            {
                ApplicationName = "das-apprenticeship-programmes-indexer";
            }

            if (typeof(T) == typeof(IMaintainLarsIndex))
            {
                ApplicationName = "das-lars-indexer";
            }

            if (typeof(T) == typeof(IMaintainProviderIndex))
            {
                ApplicationName = "das-provider-indexer";
            }

            if (typeof(T) == typeof(IMaintainAssessmentOrgsIndex))
            {
                ApplicationName = "das-epao-indexer";
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
