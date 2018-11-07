using System.Collections.Generic;
using System.Linq;
using MediatR;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD
{
    public class AchievementRatesProvider : IRequestHandler<AchievementRateProviderRequest, AchievementRateProviderResult>
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _logger;
        private readonly IInfrastructureSettings _settings;

        public AchievementRatesProvider(
            IDatabaseProvider databaseProvider,
            ILog logger,
            IInfrastructureSettings settings)
        {
            _databaseProvider = databaseProvider;
            _logger = logger;
            _settings = settings;
        }

        public AchievementRateProviderResult Handle(AchievementRateProviderRequest message)
        {
            var results = _settings.UseStoredProc
                ? GetDataWithStoredProc()
                : GetData();

            _logger.Debug($"Retreived {results.Count} Provider rates");
            return new AchievementRateProviderResult { Rates = results };
        }

        private IList<AchievementRateProvider> GetDataWithStoredProc()
        {
            return _databaseProvider.QueryStoredProc<AchievementRateProvider>("[dbo].[GetAchievementRatesProvider]").ToList();
        }

        private IList<AchievementRateProvider> GetData()
        {
            var query = @"SELECT 
                    [UKPRN], 
                    [Age],
                    [Apprenticeship Level] as ApprenticeshipLevel,
                    [Overall Cohort] as OverallCohort, 
                    [Overall Achivement Rate %] as OverallAchievementRate,
                    [Sector Subject Area Tier 2] as SectorSubjectAreaTier2,
                    [SSA2 Code] as SSA2Code
                    FROM AchievementRatesProvider
                    WHERE [Age] = 'All Age'
                    AND [Sector Subject Area Tier 2] <> 'All SSA T2'
                    AND [Apprenticeship Level] <> 'All'
                    AND [HybridYear] = (SELECT MAX([HybridYear]) FROM AchievementRatesProvider)
                    ";
            return _databaseProvider.Query<AchievementRateProvider>(query).ToList();
        }
    }
}