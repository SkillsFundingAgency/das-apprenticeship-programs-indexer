namespace Sfa.Das.Sas.Indexer.Infrastructure.DapperBD
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Services;

    public class SatisfactionRatesProvider : ISatisfactionRatesProvider
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _log;
        private const string LearnerSatisfactionRatesTableName = "[dbo].[LearnerSatisf_2015_2016]";
        private const string EmployerSatisfactionRatesTableName = "[dbo].[EmployerSatisf_2015_2016]";

        public SatisfactionRatesProvider(IDatabaseProvider databaseProvider, ILog log)
        {
            _databaseProvider = databaseProvider;
            _log = log;
        }

        public IEnumerable<SatisfactionRateProvider> GetAllEmployerSatisfactionByProvider()
        {
            var query = $@"
                    SELECT  [UKPRN]
                    ,       [Final_Score] AS FinalScore
                    ,       [Employers] AS TotalCount
                    ,       [Responses] AS ResponseCount
                    FROM    {EmployerSatisfactionRatesTableName}
                    ";

            var results = _databaseProvider.Query<SatisfactionRateProvider>(query);
            _log.Debug($"Retrieved {results.Count()} employer satisfaction rates");
             return results;
        }

        public IEnumerable<SatisfactionRateProvider> GetAllLearnerSatisfactionByProvider()
        {
            var query = $@"
                    SELECT  [UKPRN]
                    ,       [Final_Score] AS FinalScore
                    ,       [Learners] AS TotalCount
                    ,       [Responses] AS ResponseCount
                    FROM    {LearnerSatisfactionRatesTableName}
                    ";

            var results = _databaseProvider.Query<SatisfactionRateProvider>(query);
            _log.Debug($"Retrieved {results.Count()} leaner satisfaction rates");
            return results;
        }
    }
}