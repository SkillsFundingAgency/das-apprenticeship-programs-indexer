﻿using System.Linq;
using MediatR;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.EmployerProvider;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    public sealed class EmployerProviderVstsClient : IRequestHandler<EmployerProviderRequest, EmployerProviderResult>
    {
        private readonly IConvertFromCsv _convertFromCsv;
        private readonly IVstsClient _vstsClient;
        private readonly IAppServiceSettings _appServiceSettings;
        private readonly ILog _logger;

        public EmployerProviderVstsClient(
            IConvertFromCsv convertFromCsv,
            IVstsClient vstsClient,
            IAppServiceSettings appServiceSettings,
            ILog logger)
        {
            _convertFromCsv = convertFromCsv;
            _vstsClient = vstsClient;
            _appServiceSettings = appServiceSettings;
            _logger = logger;
        }

        public EmployerProviderResult Handle(EmployerProviderRequest request)
        {
            var records = _convertFromCsv.CsvTo<EmployerProviderCsvRecord>(LoadEmployerProvidersFromVsts());
            _logger.Debug($"Retreived {records.Count} Employer providers");

            return new EmployerProviderResult { Providers = records.Select(employerProviderCsvRecord => employerProviderCsvRecord.UkPrn.ToString()).ToList() };
        }

        private string LoadEmployerProvidersFromVsts()
        {
            return _vstsClient.GetFileContent($"employerProviders/{_appServiceSettings.EnvironmentName}/employerProviders.csv");
        }
    }
}