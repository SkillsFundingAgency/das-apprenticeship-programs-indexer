using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Logging.Metrics;
using Sfa.Das.Sas.Indexer.Core.Logging.Models;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Models.Framework;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services
{
    public class MetaDataHelper : IMetaDataHelper
    {
        private readonly IGetStandardMetaData _metaDataReader;

        private readonly IGenerateStandardMetaData _metaDataWriter;

        private readonly IGetFrameworkMetaData _metaDataFrameworkReader;

        private readonly ILog _log;

        public MetaDataHelper(IGetStandardMetaData metaDataReader, IGenerateStandardMetaData metaDataGenerator, ILog log, IGetFrameworkMetaData metaDataFrameworkReader)
        {
            _metaDataReader = metaDataReader;
            _metaDataWriter = metaDataGenerator;
            _log = log;
            _metaDataFrameworkReader = metaDataFrameworkReader;
        }

        public IEnumerable<StandardMetaData> GetAllStandardsMetaData()
        {
            _log.Debug("Starting to get LARS standards and meta data");
            var timing = ExecutionTimer.GetTiming(() => _metaDataReader.GetStandardsMetaData());

            _log.Debug("MetaDataHelper.GetAllStandardsMetaData", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return timing.Result;
        }

        public void UpdateMetadataRepository()
        {
            var timing = ExecutionTimer.GetTiming(() => _metaDataWriter.GenerateStandardMetadataFiles());

            _log.Debug("MetaDataHelper.UpdateMetadataRepository", new TimingLogEntry { ElaspedMilliseconds = timing.TotalMilliseconds });
        }

        public IEnumerable<FrameworkMetaData> GetAllFrameworkMetaData()
        {
            _log.Debug("Starting to get LARS frameworks and meta data");
            var timing = ExecutionTimer.GetTiming(() => _metaDataFrameworkReader.GetAllFrameworks());

            _log.Debug("MetaDataHelper.GetAllFrameworkMetaData", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return timing.Result;
        }
    }
}