namespace Sfa.Das.Sas.Indexer.Infrastructure.DapperBD
{
    using System.Linq;
    using Core.Logging;
    using MediatR;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;

    public class AchievementRatesProvider : IRequestHandler<AchievementRateProviderRequest, AchievementRateProviderResult>
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _logger;

        public AchievementRatesProvider(IDatabaseProvider databaseProvider, ILog logger)
        {
            _databaseProvider = databaseProvider;
            _logger = logger;
        }

        public AchievementRateProviderResult Handle(AchievementRateProviderRequest message)
        {
            var query = @"SELECT 
                    [UKPRN], 
                    [Age],
                    [Apprenticeship Level] as ApprenticeshipLevel,
                    [Overall Cohort] as OverallCohort, 
                    [Overall Achivement Rate %] as OverallAchievementRate,
                    [Sector Subject Area Tier 2] as SectorSubjectAreaTier2,
                    [SSA2 Code] as SSA2Code
                    FROM ar_by_provider
                    WHERE [Age] = 'All Age'
                    AND [Sector Subject Area Tier 2] <> 'All SSA T2'
                    AND [Apprenticeship Level] <> 'All'
                    ";
            var achievementRateProviders = _databaseProvider.Query<AchievementRateProvider>(query).ToList();
            _logger.Debug($"Retreived {achievementRateProviders.Count} Provider rates");
            return new AchievementRateProviderResult { Rates = achievementRateProviders };
        }
    }
}