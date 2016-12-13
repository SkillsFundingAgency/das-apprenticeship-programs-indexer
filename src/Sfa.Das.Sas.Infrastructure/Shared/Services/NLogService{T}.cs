namespace Sfa.Das.Sas.Indexer.Infrastructure.Shared.Services
{
    using System;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Lars.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Infrastructure.Services;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

    public class NLogService<T> : NLogService, ILog
    {
        public NLogService(Type loggerType, IInfrastructureSettings settings)
            : base(loggerType, settings)
        {
            if (typeof(T) == typeof(IMaintainApprenticeshipIndex))
            {
                ApplicationName = "apprenticeship-programmes-indexer";
            }

            if (typeof(T) == typeof(IMaintainLarsIndex))
            {
                ApplicationName = "lars-programmes-indexer";
            }

            if (typeof(T) == typeof(IMaintainProviderIndex))
            {
                ApplicationName = "provider-indexer";
            }
        }
    }
}
