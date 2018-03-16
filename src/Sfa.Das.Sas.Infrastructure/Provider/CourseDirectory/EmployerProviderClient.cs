using System.Collections.Generic;
using Sfa.Das.Sas.Indexer.ApplicationServices.Apprenticeship.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    using System.Linq;
    using MediatR;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;

    public sealed class EmployerProviderClient : IRequestHandler<EmployerProviderRequest, EmployerProviderResult>
    {
        private readonly IMetaDataHelper _metaDataHelper;
        private readonly ILog _logger;

        public EmployerProviderClient(
            IMetaDataHelper metaDataHelper,
            ILog logger)
        {
            _metaDataHelper = metaDataHelper;
            _logger = logger;
        }

        public EmployerProviderResult Handle(EmployerProviderRequest request)
        {
            var records = _metaDataHelper.GetEmployerProviders();
            _logger.Debug($"Retreived {records.Count} Employer providers", new Dictionary<string, object> { { "TotalCount", records.Count } });

            return new EmployerProviderResult { Providers = records.Select(employerProviderCsvRecord => employerProviderCsvRecord.UkPrn.ToString()).ToList() };
        }
    }
}
