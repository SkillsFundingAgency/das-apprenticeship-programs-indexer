using System.Collections.Generic;
using System.Linq;
using MediatR;
using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD
{
    public class SatisfactionRatesProvider : IRequestHandler<EmployerSatisfactionRateRequest, EmployerSatisfactionRateResult>
    { 
        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _log;
        private readonly IInfrastructureSettings _settings;

        public SatisfactionRatesProvider(
            IDatabaseProvider databaseProvider,
            ILog log,
            IInfrastructureSettings settings)
        {
            _databaseProvider = databaseProvider;
            _log = log;
            _settings = settings;
        }

        public EmployerSatisfactionRateResult Handle(EmployerSatisfactionRateRequest message)
        {
            var results = _settings.UseStoredProc
                ? GetDataWithStoredProc()
                : GetData();

            _log.Debug($"Retrieved {results.Count} employer satisfaction rates");
            return new EmployerSatisfactionRateResult { Rates = results };
        }

        private IList<SatisfactionRateProvider> GetDataWithStoredProc()
        {
            return _databaseProvider.QueryStoredProc<SatisfactionRateProvider>("[dbo].[GetLatestEmployerSatisfaction]").ToList();
        }

        private IList<SatisfactionRateProvider> GetData()
        {
            var query = $@"
                    SELECT  [UKPRN]
                    ,       [Final_Score] AS FinalScore
                    ,       [Employers] AS TotalCount
                    ,       [Responses] AS ResponseCount
                    FROM    {_settings.EmployerSatisfactionRatesTableName}
                    ";

            return _databaseProvider.Query<SatisfactionRateProvider>(query).ToList();
        }
    }
}