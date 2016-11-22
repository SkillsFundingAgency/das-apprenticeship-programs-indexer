using Sfa.Das.Sas.Indexer.Core.Models.Provider;
using Sfa.Das.Sas.Indexer.Core.Services;
using Sfa.Das.Sas.Indexer.Infrastructure.Mapping;
using Sfa.Das.Sas.Indexer.Infrastructure.Services.Wrappers;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Sfa.Das.Sas.Indexer.Core.Logging;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;
    using Ukrlp;

    public class UkrlpService : IUkrlpService
    {
        private readonly IInfrastructureSettings _infrastructureSettings;
        private readonly IProviderQueryPortTypeClientWrapper _providerClientWrapper;
        private readonly IUkrlpProviderResponseMapper _providerResponseMapper;
        private readonly ILog _logger;

        public UkrlpService(
            IInfrastructureSettings infrastructureSettings,
            IProviderQueryPortTypeClientWrapper providerClientWrapper,
            IUkrlpProviderResponseMapper providerResponseMapper,
            ILog logger)
        {
            _infrastructureSettings = infrastructureSettings;
            _providerClientWrapper = providerClientWrapper;
            _providerResponseMapper = providerResponseMapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Provider>> GetLearnerProviderInformationAsync(string[] ukprns)
        {
            try
            {
                var response = await _providerClientWrapper.RetrieveAllProvidersAsync(new ProviderQueryStructure
                {
                    QueryId = _infrastructureSettings.UkrlpQueryId,
                    SchemaVersion = "?",
                    SelectionCriteria = new SelectionCriteriaStructure
                    {
                        UnitedKingdomProviderReferenceNumberList = ukprns,
                        CriteriaCondition = QueryCriteriaConditionType.AND,
                        CriteriaConditionSpecified = true,
                        StakeholderId = _infrastructureSettings.UkrlpStakeholderId,
                        ApprovedProvidersOnly = YesNoType.No,
                        ApprovedProvidersOnlySpecified = true,
                        ProviderStatus = _infrastructureSettings.UkrlpProviderStatus
                    }
                });

                return response
                        .ProviderQueryResponse
                        .MatchingProviderRecords
                        .Select(providerRecordStructure => _providerResponseMapper.MapFromUkrlpProviderRecord(providerRecordStructure));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }
    }
}
