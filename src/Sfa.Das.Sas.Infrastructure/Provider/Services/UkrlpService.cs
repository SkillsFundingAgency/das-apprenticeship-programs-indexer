using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
using Sfa.Das.Sas.Indexer.Core.Logging;
using Sfa.Das.Sas.Indexer.Infrastructure.Provider.Services.Wrappers;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
using Sfa.Das.Sas.Indexer.Infrastructure.Ukrlp;
using SFA.DAS.NLog.Logger;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.Services
{
    using Provider = ApplicationServices.Provider.Models.UkRlp.Provider;

    public class UkrlpService : IRequestHandler<UkrlpProviderRequest, UkrlpProviderResponse>
    {
        private readonly IInfrastructureSettings _infrastructureSettings;
        private readonly IUkrlpClient _providerClient;
        private readonly ILog _logger;
        private readonly int _ukprnRequestUkprnBatchSize;

        public UkrlpService(
            IInfrastructureSettings infrastructureSettings,
            IUkrlpClient providerClient,
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
                var providerList = new List<Provider>();
                var noOfUkprnsProcessed = 0;

                var ukprnsListSize = request.Providers.Count();

                do
                {
                    var numberOfUkprnsUnprocessed = ukprnsListSize - noOfUkprnsProcessed;
                    var numberOfUkprnsToSend = numberOfUkprnsUnprocessed > _ukprnRequestUkprnBatchSize ? _ukprnRequestUkprnBatchSize : numberOfUkprnsUnprocessed;

                    var ukprnToRequest = request.Providers.Skip(noOfUkprnsProcessed).Take(numberOfUkprnsToSend);

                    var response = _providerClient.RetrieveAllProviders(new ProviderQueryStructure
                    {
                        QueryId = _infrastructureSettings.UkrlpQueryId,
                        SchemaVersion = "?",
                        SelectionCriteria = new SelectionCriteriaStructure
                        {
                            UnitedKingdomProviderReferenceNumberList = ukprnToRequest.Select(x => x.ToString()).ToArray(),
                            CriteriaCondition = QueryCriteriaConditionType.AND,
                            CriteriaConditionSpecified = true,
                            StakeholderId = _infrastructureSettings.UkrlpStakeholderId,
                            ApprovedProvidersOnly = YesNoType.No,
                            ApprovedProvidersOnlySpecified = true,
                            ProviderStatus = _infrastructureSettings.UkrlpProviderStatus
                        }
                    });

                    if (response != null)
                    {
                        providerList.AddRange(response);
                    }

                    noOfUkprnsProcessed += numberOfUkprnsToSend;
                }
                while (noOfUkprnsProcessed < ukprnsListSize);

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
