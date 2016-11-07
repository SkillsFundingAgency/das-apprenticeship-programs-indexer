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

        public SatisfactionRatesProvider(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public IEnumerable<SatisfactionRateProvider> GetAllByProvider()
        {
            var latestHybridYear = GetLatestNationalHybridEndYear();

            var query = @"
                    SELECT [UKPRN], 
                    [Final_Score],
                    [Learners],
                    [Responses],
                    FROM [dbo].[sr_learner]
                    WHERE [Hybrid_End_Year] = @date
                    ";

            var results = _databaseProvider.Query<SatisfactionRateProvider>(query, new { date = latestHybridYear });

            return results;
        }

        private string GetLatestNationalHybridEndYear()
        {
            var query = @"SELECT MAX([Hybrid_End_Year]) FROM [dbo].[sr_learner]";

            return _databaseProvider.ExecuteScalar<string>(query);
        }
    }
}