using System.Collections.Generic;
using System.Linq;
using MediatR;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD
{
    public sealed class AchievementRatesNational : IRequestHandler<AchievementRateNationalRequest, AchievementRateNationalResult>
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _logger;
        private readonly IInfrastructureSettings _settings;

        public AchievementRatesNational(
            IDatabaseProvider databaseProvider,
            ILog logger,
            IInfrastructureSettings settings)
        {
            _databaseProvider = databaseProvider;
            _logger = logger;
            _settings = settings;
        }

        public AchievementRateNationalResult Handle(AchievementRateNationalRequest message)
        {
            var results = _settings.UseStoredProc
                ? GetDataWithStoredProc()
                : GetData();

            _logger.Debug($"Retreived {results.Count} national provider rates");
            return new AchievementRateNationalResult { Rates = results };
        }

        private IList<AchievementRateNational> GetDataWithStoredProc()
        {
            return _databaseProvider.QueryStoredProc<AchievementRateNational>("[dbo].[GetAchievementRatesNational]").ToList();
        }

        private IList<AchievementRateNational> GetData()
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

            return _databaseProvider.Query<AchievementRateNational>(query).ToList();
        }
    }
}