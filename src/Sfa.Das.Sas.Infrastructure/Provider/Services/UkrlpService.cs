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
        private readonly int _ukprnRequestUkprnBatchSize;

        public UkrlpService(
            IInfrastructureSettings infrastructureSettings,
            IProviderQueryApiClient providerClient,
            ILog logger)
        {
            _infrastructureSettings = infrastructureSettings;
            _providerClient = providerClient;
            _logger = logger;
            _ukprnRequestUkprnBatchSize = _infrastructureSettings.UkrlpRequestUkprnBatchSize;
        }

        public UkrlpProviderResponse Handle(UkrlpProviderRequest request)
        {
            _logger.Debug("Starting to get providers from UKRLP");
            try
            {
                var providerList = _providerClient.ProviderQuery(new SelectionCriteriaStructure
                {
                    UnitedKingdomProviderReferenceNumberList = request.Providers.Select(x => x.ToString()).ToArray(),
                    CriteriaCondition = QueryCriteriaConditionType.AND,
                    CriteriaConditionSpecified = true,
                    StakeholderId = _infrastructureSettings.UkrlpStakeholderId,
                    ApprovedProvidersOnly = YesNoType.No,
                    ApprovedProvidersOnlySpecified = true,
                    ProviderStatus = _infrastructureSettings.UkrlpProviderStatus
                }).ToList();

                _logger.Debug($"Retreived {providerList.Count} Providers from UKRLP");

                return new UkrlpProviderResponse { MatchingProviderRecords = providerList };
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, ex.Message);
                return new UkrlpProviderResponse();
            }
        }
    }
}
