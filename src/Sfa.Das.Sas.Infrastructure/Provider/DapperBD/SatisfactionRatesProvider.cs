using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DapperBD
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Services;

    public class SatisfactionRatesProvider : ISatisfactionRatesProvider
    {
        private const string LearnerSatisfactionRatesTableName = "[dbo].[LearnerSatisf_2015_2016]";
        private const string EmployerSatisfactionRatesTableName = "[dbo].[EmployerSatisf_2015_2016]";

        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _log;

        public SatisfactionRatesProvider(IDatabaseProvider databaseProvider, ILog log)
        {
            _databaseProvider = databaseProvider;
            _log = log;
        }

        public EmployerSatisfactionRateResult GetAllEmployerSatisfactionByProvider()
        {
            var query = $@"
                    SELECT  [UKPRN]
                    ,       [Final_Score] AS FinalScore
                    ,       [Employers] AS TotalCount
                    ,       [Responses] AS ResponseCount
                    FROM    {EmployerSatisfactionRatesTableName}
                    ";

            var results = _databaseProvider.Query<SatisfactionRateProvider>(query).ToList();
            _log.Debug($"Retrieved {results.Count} employer satisfaction rates");
            return new EmployerSatisfactionRateResult { Rates = results };
        }

        public LearnerSatisfactionRateResult GetAllLearnerSatisfactionByProvider()
        {
            var query = $@"
                    SELECT  [UKPRN]
                    ,       [Final_Score] AS FinalScore
                    ,       [Learners] AS TotalCount
                    ,       [Responses] AS ResponseCount
                    FROM    {LearnerSatisfactionRatesTableName}
                    ";

            var results = _databaseProvider.Query<SatisfactionRateProvider>(query).ToList();
            _log.Debug($"Retrieved {results.Count} leaner satisfaction rates");
            return new LearnerSatisfactionRateResult { Rates = results };
        }
    }
}