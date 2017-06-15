namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD
{
    using System.Linq;
    using MediatR;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Provider.Models;

    public sealed class AchievementRatesNational : IRequestHandler<AchievementRateNationalRequest, AchievementRateNationalResult>
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _logger;

        public AchievementRatesNational(IDatabaseProvider databaseProvider, ILog logger)
        {
            _databaseProvider = databaseProvider;
            _logger = logger;
        }

        public AchievementRateNationalResult Handle(AchievementRateNationalRequest message)
        {
            var query = @"
                    SELECT 
                        [Institution Type] as InstitutionType,
                        [Hybrid End Year] as HybridEndYear,
                        [Age],
                        [Sector Subject Area Tier 1] as SectorSubjectAreaTier1,
                        [Sector Subject Area Tier 2] as SectorSubjectAreaTier2,
                        [Apprenticeship Level] as ApprenticeshipLevel,
                        [Overall Achievement Rate %] as OverallAchievementRate,
                        [SSA2] as SSA2Code
                    FROM ar_national
                    WHERE [Institution Type] = 'All Institution Type'
                    AND [Age] = 'All Age'
                    AND [Sector Subject Area Tier 2] <> 'All SSA T2'
                    AND [Apprenticeship Level] <> 'All'
                    AND [Hybrid End Year] = (SELECT MAX([Hybrid End Year]) FROM ar_national)
                    ";

            var results = _databaseProvider.Query<AchievementRateNational>(query).ToList();
            _logger.Debug($"Retreived {results.Count} national provider rates");
            return new AchievementRateNationalResult { Rates = results };
        }
    }
}