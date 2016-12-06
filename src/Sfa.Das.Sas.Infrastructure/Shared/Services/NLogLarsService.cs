namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    using System;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

    public class NLogLarsService : NLogService, ILogLars
    {
        public NLogLarsService(Type loggerType, IInfrastructureSettings settings)
            : base(loggerType, settings)
        {
            ApplicationName = "lars-programmes-indexer";
        }
    }
}