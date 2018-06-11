using MediatR;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Metrics;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Models;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    public sealed class StandardsMetaDataHelper : IRequestHandler<StandardMetaDataRequest, StandardMetaDataResult>
    {
        private readonly IGetStandardMetaData _metaDataReader;

        private readonly ILog _log;

        public StandardsMetaDataHelper(
            ILog log,
            IGetStandardMetaData metaDataReader)
        {
            _log = log;
            _metaDataReader = metaDataReader;
        }

        public StandardMetaDataResult Handle(StandardMetaDataRequest request)
        {
            _log.Debug("Starting to get LARS standards and meta data");
            var timing = ExecutionTimer.GetTiming(() => _metaDataReader.GetStandardsMetaData());

            _log.Debug("MetaDataHelper.GetAllStandardsMetaData", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return new StandardMetaDataResult { Standards = timing.Result };
        }
    }
}