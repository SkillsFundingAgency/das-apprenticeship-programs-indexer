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

    public class MetaDataHelper : IMetaDataHelper
    {
        private readonly IGetStandardMetaData _metaDataReader;

        private readonly IGenerateStandardMetaData _metaDataWriter;

        private readonly IGetFrameworkMetaData _metaDataFrameworkReader;

        private readonly IGetLarsMetadata _larsApprenticeshipReader;

        private readonly ILog _log;

        public MetaDataHelper(
            IGetStandardMetaData metaDataReader,
            IGenerateStandardMetaData metaDataGenerator,
            ILog log,
            IGetFrameworkMetaData metaDataFrameworkReader,
            IGetLarsMetadata getLarsMetadata)
        {
            _metaDataReader = metaDataReader;
            _metaDataWriter = metaDataGenerator;
            _log = log;
            _metaDataFrameworkReader = metaDataFrameworkReader;
            _larsApprenticeshipReader = getLarsMetadata;
        }

        public void UpdateMetadataRepository()
        {
            var timing = ExecutionTimer.GetTiming(() => _metaDataWriter.GenerateStandardMetadataFiles());

            _log.Debug("MetaDataHelper.UpdateMetadataRepository", new TimingLogEntry { ElaspedMilliseconds = timing.TotalMilliseconds });
        }

        public LarsData GetAllApprenticeshipLarsMetaData()
        {
            _log.Debug("Starting to get LARS apprenticeship meta data");
            var timing = ExecutionTimer.GetTiming(() => _larsApprenticeshipReader.GetLarsData());

            _log.Debug("MetaDataHelper.GetAllApprenticeshipLarsMetaData", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return timing.Result;
        }
    }
}