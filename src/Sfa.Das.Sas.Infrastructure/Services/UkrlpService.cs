namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Services;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using Ukrlp;
    using Provider = Sfa.Das.Sas.Indexer.ApplicationServices.Provider.Models.UkRlp.Provider;

    public class UkrlpService : IUkrlpService
    {
        private readonly IInfrastructureSettings _infrastructureSettings;
        private readonly IUkrlpClient _providerClient;
        private readonly ILogProvider _logger;
        private readonly int _ukprnRequestUkprnBatchSize;

        public UkrlpService(
            IInfrastructureSettings infrastructureSettings,
            IUkrlpClient providerClient,
            ILogProvider logger)
        {
            _infrastructureSettings = infrastructureSettings;
            _providerClient = providerClient;
            _logger = logger;
            _ukprnRequestUkprnBatchSize = _infrastructureSettings.UkrlpRequestUkprnBatchSize;
        }

        public UkrlpProviderResponse GetProviders(IEnumerable<int> ukprns)
        {
            try
            {
                var providerList = new List<Provider>();
                var noOfUkprnsProcessed = 0;

                var ukprnsListSize = ukprns.Count();

                do
                {
                    var numberOfUkprnsUnprocessed = ukprnsListSize - noOfUkprnsProcessed;
                    var numberOfUkprnsToSend = numberOfUkprnsUnprocessed > _ukprnRequestUkprnBatchSize ? _ukprnRequestUkprnBatchSize : numberOfUkprnsUnprocessed;

                    var ukprnToRequest = ukprns.Skip(noOfUkprnsProcessed).Take(numberOfUkprnsToSend);

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

                    providerList.AddRange(response);

                    noOfUkprnsProcessed += numberOfUkprnsToSend;
                } while (noOfUkprnsProcessed < ukprnsListSize);

                _logger.Debug($"Retreived {providerList.Count} Providers from UKRLP");

                return new UkrlpProviderResponse { MatchingProviderRecords = providerList};
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }
    }
}
