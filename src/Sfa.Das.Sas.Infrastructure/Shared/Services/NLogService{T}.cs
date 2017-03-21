namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services
{
    using System;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.AssessmentOrgs.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

    public class NLogService<T> : NLogService
    {
        public NLogService(Type loggerType, IInfrastructureSettings settings)
            : base(loggerType, settings)
        {
            if (typeof(T) == typeof(IMaintainApprenticeshipIndex))
            {
                ApplicationName = "das-apprenticeship-programmes-indexer";
            }

            if (typeof(T) == typeof(IMaintainLarsIndex))
            {
                ApplicationName = "das-lars-programmes-indexer";
            }

            if (typeof(T) == typeof(IMaintainProviderIndex))
            {
                ApplicationName = "das-provider-indexer";
            }

            if (typeof(T) == typeof(IMaintainAssessmentOrgsIndex))
            {
                ApplicationName = "das-assessment-orgs-indexer";
            }
        }
    }
}
