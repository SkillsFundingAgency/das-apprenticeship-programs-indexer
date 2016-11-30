using System;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    public class NLogProviderService : NLogService, ILogProvider
    {
        public NLogProviderService(Type loggerType, IInfrastructureSettings settings)
            : base(loggerType, settings)
        {
            ApplicationName = "provider-indexer";
        }
    }
}