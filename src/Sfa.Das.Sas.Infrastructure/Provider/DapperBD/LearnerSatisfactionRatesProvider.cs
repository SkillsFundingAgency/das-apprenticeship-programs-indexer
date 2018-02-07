using System.Collections.Generic;
using System.Linq;

using MediatR;

using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.MetaData;

using SFA.DAS.NLog.Logger;
using Sfa.Das.Sas.Indexer.Core.Models;
using Sfa.Das.Sas.Indexer.Core.Provider.Models;
using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD
{
    public class LearnerSatisfactionRatesProvider : IRequestHandler<LearnerSatisfactionRateRequest, LearnerSatisfactionRateResult>
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly ILog _log;
        private readonly IInfrastructureSettings _settings;

        public LearnerSatisfactionRatesProvider(
            IDatabaseProvider databaseProvider,
            ILog log,
            IInfrastructureSettings settings)
        {
            _databaseProvider = databaseProvider;
            _log = log;
            _settings = settings;
        }

        public LearnerSatisfactionRateResult Handle(LearnerSatisfactionRateRequest message)
        {
            var results = _settings.UseStoredProc
                ? GetDataWithStoredProc()
                : GetData();

            _log.Debug("Retrieved learner satisfaction rates from DB", new Dictionary<string, object> { { "TotalCount", results.Count } });
            return new LearnerSatisfactionRateResult { Rates = results };
        }

        private IList<SatisfactionRateProvider> GetDataWithStoredProc()
        {
            return _databaseProvider.QueryStoredProc<SatisfactionRateProvider>("[dbo].[GetLatestLearnerSatisfaction]").ToList();
        }

        private IList<SatisfactionRateProvider> GetData()
        {
            var query = $@"
                    SELECT  [UKPRN]
                    ,       [Final_Score] AS FinalScore
                    ,       [Learners] AS TotalCount
                    ,       [Responses] AS ResponseCount
                    FROM    {_settings.LearnerSatisfactionRatesTableName}
                    ";

            return _databaseProvider.Query<SatisfactionRateProvider>(query).ToList();
        }
    }
}