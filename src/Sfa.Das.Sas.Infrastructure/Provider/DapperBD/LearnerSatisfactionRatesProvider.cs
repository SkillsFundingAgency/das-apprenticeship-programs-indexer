using MediatR;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DapperBD
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;
    using Sfa.Das.Sas.Indexer.Core.Services;

    public class LearnerSatisfactionRatesProvider : IRequestHandler<LearnerSatisfactionRateRequest, LearnerSatisfactionRateResult>
    {
        private const string LearnerSatisfactionRatesTableName = "[dbo].[LearnerSatisf_2015_2016]";

        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _log;

        public LearnerSatisfactionRatesProvider(IDatabaseProvider databaseProvider, ILog log)
        {
            _databaseProvider = databaseProvider;
            _log = log;
        }

        public LearnerSatisfactionRateResult Handle(LearnerSatisfactionRateRequest message)
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