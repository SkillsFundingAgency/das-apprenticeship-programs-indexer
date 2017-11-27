using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Metrics;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Models;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using MediatR;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;

    public class FrameworkMetaDataHelper : IRequestHandler<FrameworkMetaDataRequest, FrameworkMetaDataResult>
    {
        private readonly IGetFrameworkMetaData _metaDataFrameworkReader;

        private readonly ILog _log;

        public FrameworkMetaDataHelper(
            ILog log,
            IGetFrameworkMetaData metaDataFrameworkReader)
        {
            _log = log;
            _metaDataFrameworkReader = metaDataFrameworkReader;
        }

        public FrameworkMetaDataResult Handle(FrameworkMetaDataRequest message)
        {
            _log.Debug("Starting to get LARS frameworks and meta data");
            var timing = ExecutionTimer.GetTiming(() => _metaDataFrameworkReader.GetAllFrameworks());

            _log.Debug("MetaDataHelper.GetAllFrameworkMetaData", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return new FrameworkMetaDataResult { Frameworks = timing.Result };
        }
    }
}