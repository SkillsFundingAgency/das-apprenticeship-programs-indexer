﻿using System.Collections.Generic;
using System.Linq;
using MediatR;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.EmployerProvider;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.Hei;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Settings;
using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Core.Services;

namespace Sfa.Das.Sas.Indexer.Infrastructure.CourseDirectory
{
    public sealed class ProviderVstsClient : IGetApprenticeshipProviders, IRequestHandler<HeiProvidersRequest, HeiProvidersResult>
    {
        private readonly IConvertFromCsv _convertFromCsv;
        private readonly IVstsClient _vstsClient;
        private readonly IAppServiceSettings _appServiceSettings;
        private readonly ILog _logger;

        public ProviderVstsClient(
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

        public EmployerProviderResult GetEmployerProviders()
        {
            var records = _convertFromCsv.CsvTo<EmployerProviderCsvRecord>(LoadEmployerProvidersFromVsts());
            _logger.Debug($"Retreived {records.Count} Employer providers");

            return new EmployerProviderResult { Providers = records.Select(employerProviderCsvRecord => employerProviderCsvRecord.UkPrn.ToString()).ToList() };
        }

        public HeiProvidersResult Handle(HeiProvidersRequest message)
        {
            return GetHeiProviders();
        }

        public HeiProvidersResult GetHeiProviders()
        {
            var records = _convertFromCsv.CsvTo<HeiProviderCsvRecord>(LoadHeiProvidersFromVsts());

            var providers = (from heiProviderCsvRecord in records where heiProviderCsvRecord.UkPrn != null && heiProviderCsvRecord.OrgType == "Higher Education Organisation" select heiProviderCsvRecord.UkPrn).Distinct().ToList();
            return new HeiProvidersResult { Providers = providers };
        }

        private string LoadEmployerProvidersFromVsts()
        {
            return _vstsClient.GetFileContent($"employerProviders/{_appServiceSettings.EnvironmentName}/employerProviders.csv");
        }

        private string LoadHeiProvidersFromVsts()
        {
            return _vstsClient.GetFileContent($"hei/{_appServiceSettings.EnvironmentName}/hei.csv");
        }
    }
}