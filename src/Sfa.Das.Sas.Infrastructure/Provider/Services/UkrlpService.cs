﻿using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Services
{
    using System;
    using System.Linq;
    using MediatR;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using Ukrlp.SoapApi.Client;
    using Ukrlp.SoapApi.Client.ProviderQueryServiceV4;

    public class UkrlpService : IRequestHandler<UkrlpProviderRequest, UkrlpProviderResponse>
    {
        private readonly IInfrastructureSettings _infrastructureSettings;
        private readonly IProviderQueryApiClient _providerClient;
        private readonly ILog _logger;

        public UkrlpService(
            IInfrastructureSettings infrastructureSettings,
            IProviderQueryApiClient providerClient,
            ILog logger)
        {
            _infrastructureSettings = infrastructureSettings;
            _providerClient = providerClient;
            _logger = logger;
        }

        public UkrlpProviderResponse Handle(UkrlpProviderRequest request)
        {
            _logger.Debug("Starting to get providers from UKRLP");
            try
            {
                var response = _providerClient.ProviderQuery(new SelectionCriteriaStructure
                {
                    UnitedKingdomProviderReferenceNumberList = request.Providers.Select(x => x.ToString()).ToArray(),
                    CriteriaCondition = QueryCriteriaConditionType.AND,
                    CriteriaConditionSpecified = true,
                    StakeholderId = _infrastructureSettings.UkrlpStakeholderId,
                    ApprovedProvidersOnly = YesNoType.No,
                    ApprovedProvidersOnlySpecified = true,
                    ProviderStatus = _infrastructureSettings.UkrlpProviderStatus
                });

                foreach (var warning in response.Warnings)
                {
                    _logger.Warn(warning.Value, new Dictionary<string, object> { { "UKPRN", warning.Key } });
                }

                _logger.Debug($"Retreived {response.Providers.Count()} Providers from UKRLP");

                return new UkrlpProviderResponse { MatchingProviderRecords = response.Providers };
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, ex.Message);
                return new UkrlpProviderResponse();
            }
        }
    }
}
