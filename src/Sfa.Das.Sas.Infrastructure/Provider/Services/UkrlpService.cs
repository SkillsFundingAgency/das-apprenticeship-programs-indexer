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
            _providerClient.PreRequest = LogRequest;
            _logger = logger;
        }

        private void LogRequest(SelectionCriteriaStructure request)
        {
            _logger.Debug("UKRLP Request", new Dictionary<string, object> { { "Body", JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) } });
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
                    _logger.Warn(warning.Value, new Dictionary<string, object> { { "UKPRN", warning.Key } });
                }

                _logger.Debug($"Retreived {response.Providers.Count()} Providers in total from UKRLP", new Dictionary<string, object> { { "TotalCount", response.Providers.Count() } });

                return new UkrlpProviderResponse { MatchingProviderRecords = response.Providers };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem with UKRLP", ex);
            }
        }

        private void LogResponse(ProviderQueryResponse response)
        {
            _logger.Debug($"UKRLP response", new Dictionary<string, object> { { "TotalCount", response.MatchingProviderRecords.Length }, { "Body", string.Join(", ", response.MatchingProviderRecords.Select(x => x.UnitedKingdomProviderReferenceNumber)) } });
        }
    }
}
