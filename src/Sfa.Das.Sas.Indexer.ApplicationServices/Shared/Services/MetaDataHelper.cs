namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Services
{
    using System.Collections.Generic;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.EmployerProvider;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Fsc;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Metrics;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Models;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
    using Sfa.Das.Sas.Indexer.Core.Apprenticeship.Models;
    using Sfa.Das.Sas.Indexer.Core.AssessmentOrgs.Models;

    public class MetaDataHelper : IMetaDataHelper
    {
        private readonly IGetLarsMetadata _larsApprenticeshipReader;
        private readonly IGetAssessmentOrgsData _assessmentOrgsData;
        private readonly IFcsActiveProvidersService _fcsActiveProvidersService;
        private readonly IEmployerProvidersService _employerProvidersService;

        private readonly ILog _log;

        public MetaDataHelper(
            IGetLarsMetadata getLarsMetadata,
            IGetAssessmentOrgsData assessmentOrgsData,
            IFcsActiveProvidersService fcsActiveProvidersService,
            IEmployerProvidersService employerProvidersService,
            ILog log)
        {
            _log = log;
            _larsApprenticeshipReader = getLarsMetadata;
            _assessmentOrgsData = assessmentOrgsData;
            _fcsActiveProvidersService = fcsActiveProvidersService;
            _employerProvidersService = employerProvidersService;
        }

        public LarsData GetAllApprenticeshipLarsMetaData()
        {
            _log.Debug("Starting to get LARS apprenticeship meta data");
            var timing = ExecutionTimer.GetTiming(() => _larsApprenticeshipReader.GetLarsData());

            _log.Debug("MetaDataHelper.GetAllApprenticeshipLarsMetaData", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return timing.Result;
        }

        public AssessmentOrganisationsDTO GetAssessmentOrganisationsData()
        {
            _log.Debug("Starting to get Assessment Organisations data");
            var timing = ExecutionTimer.GetTiming(() => _assessmentOrgsData.GetAssessmentOrganisationsData());

            _log.Debug("MetaDataHelper.GetAssessmentOrganisationsData", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return timing.Result;
        }

        public ICollection<ActiveProviderCsvRecord> GetFcsData()
        {
            _log.Debug("Starting to get FCS data");
            var timing = ExecutionTimer.GetTiming(() => _fcsActiveProvidersService.GetFcsData());

            _log.Debug("MetaDataHelper.GetFcsData", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return timing.Result;
        }

        public ICollection<EmployerProviderCsvRecord> GetEmployerProviders()
        {
            _log.Debug("Starting to get Employer Providers data");
            var timing = ExecutionTimer.GetTiming(() => _employerProvidersService.GetEmployerProviders());

            _log.Debug("MetaDataHelper.GetEmployerProviders", new TimingLogEntry { ElaspedMilliseconds = timing.ElaspedMilliseconds });

            return timing.Result;
        }
    }
}