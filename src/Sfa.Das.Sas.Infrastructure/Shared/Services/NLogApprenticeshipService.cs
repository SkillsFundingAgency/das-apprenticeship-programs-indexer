using System;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    public class NLogApprenticeshipService : NLogService, ILogApprenticeships
    {
        public NLogApprenticeshipService(Type loggerType, IInfrastructureSettings settings)
            : base(loggerType, settings)
        {
            ApplicationName = "apprenticeship-programmes-indexer";
        }
    }
}