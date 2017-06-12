using System.Collections.Generic;
using Newtonsoft.Json;

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
            _providerClient.PostRequest = LogResponse;
            _logger = logger;
        }

        public UkrlpProviderResponse Handle(UkrlpProviderRequest request)
        {
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
                    _logger.Warn("UKRLP: " + warning.Value, new Dictionary<string, object> { { "UKPRN", warning.Key } });
                }

                var matchingProviderRecords = response.Providers.ToList();
                _logger.Debug($"Retreived {matchingProviderRecords.Count} Providers in total from UKRLP", new Dictionary<string, object> { { "TotalCount", matchingProviderRecords.Count } });

                return new UkrlpProviderResponse { MatchingProviderRecords = matchingProviderRecords };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem with UKRLP", ex);
            }
        }

        private void LogResponse(SelectionCriteriaStructure criteria, ProviderQueryResponse response)
        {
            _logger.Debug($"UKRLP response", new Dictionary<string, object> { { "TotalCount", response.MatchingProviderRecords.Length }, { "Request", JsonConvert.SerializeObject(criteria.UnitedKingdomProviderReferenceNumberList, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) }, { "Body", string.Join(", ", response.MatchingProviderRecords.Select(x => x.UnitedKingdomProviderReferenceNumber)) } });
        }
    }
}
