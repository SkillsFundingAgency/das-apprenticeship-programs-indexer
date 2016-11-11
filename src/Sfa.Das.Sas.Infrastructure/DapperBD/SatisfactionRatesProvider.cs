using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

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
        private const string LearnerSatisfactionRatesTableName = "[dbo].[LearnerSatisf_2015_2016]";
        private const string EmployerSatisfactionRatesTableName = "[dbo].[EmployerSatisf_2015_2016]";

        public SatisfactionRatesProvider(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public IEnumerable<SatisfactionRateProvider> GetAllEmployerSatisfactionByProvider()
        {
            var latestHybridYear = GetLatestNationalHybridEndYear(EmployerSatisfactionRatesTableName);

            var query = $@"
                    SELECT  [UKPRN]
                    ,       [Final_Score] AS FinalScore
                    ,       [Employers] AS TotalCount
                    ,       [Responses] AS ResponseCount
                    FROM    {EmployerSatisfactionRatesTableName}
                    WHERE   [Hybrid_End_Year] = @date
                    ";

            var results = _databaseProvider.Query<SatisfactionRateProvider>(query, new { date = latestHybridYear });

            return results;
        }

        public IEnumerable<SatisfactionRateProvider> GetAllLearnerSatisfactionByProvider()
        {
            var latestHybridYear = GetLatestNationalHybridEndYear(LearnerSatisfactionRatesTableName);

            var query = $@"
                    SELECT  [UKPRN]
                    ,       [Final_Score] AS FinalScore
                    ,       [Learners] AS TotalCount
                    ,       [Responses] AS ResponseCount
                    FROM    {LearnerSatisfactionRatesTableName}
                    WHERE   [Hybrid_End_Year] = @date
                    ";

            var results = _databaseProvider.Query<SatisfactionRateProvider>(query, new { date = latestHybridYear });

            return results;
        }

        private string GetLatestNationalHybridEndYear(string tableName)
        {
            var query = $"SELECT MAX([Hybrid_End_Year]) FROM {tableName}";

            return _databaseProvider.ExecuteScalar<string>(query);
        }
    }
}