using MediatR;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.DapperBD
{
    using System.Linq;
    using Core.Logging;
    using Sfa.Das.Sas.Indexer.Core.Models;

    public class SatisfactionRatesProvider : IRequestHandler<EmployerSatisfactionRateRequest, EmployerSatisfactionRateResult>
    {
        private const string EmployerSatisfactionRatesTableName = "[dbo].[EmployerSatisf_2015_2016]";

        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _log;

        public SatisfactionRatesProvider(IDatabaseProvider databaseProvider, ILog log)
        {
            _databaseProvider = databaseProvider;
            _log = log;
        }

        public EmployerSatisfactionRateResult Handle(EmployerSatisfactionRateRequest message)
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
    }
}