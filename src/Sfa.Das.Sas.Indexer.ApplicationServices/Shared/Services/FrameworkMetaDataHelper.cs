using MediatR;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    using System.Collections.Generic;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Logging.Metrics;
    using Sfa.Das.Sas.Indexer.Core.Logging.Models;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Models.Framework;

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