using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Logging.Models;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using Dapper;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.Infrastructure.Settings;

    public class DatabaseProvider : IDatabaseProvider
    {
        private const string ErrorMsg = "Missing connectionstring for achievementrates database";
        private readonly IInfrastructureSettings _infrastructureSettings;

        private readonly ILog _logger;

        public DatabaseProvider(IInfrastructureSettings infrastructureSettings, ILog logger)
        {
            _infrastructureSettings = infrastructureSettings;
            _logger = logger;
        }

        public IEnumerable<T> Query<T>(string query, object param = null)
        {
            return ExecuteQuery<T>(query, param);
        }

        public IEnumerable<T> QueryStoredProc<T>(string query, object param = null)
        {
            return ExecuteQuery<T>(query, param, CommandType.StoredProcedure);
        }

        private IEnumerable<T> ExecuteQuery<T>(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            if (string.IsNullOrEmpty(_infrastructureSettings.AchievementRateDataBaseConnectionString))
            {
                _logger.Error(new NullReferenceException(ErrorMsg), ErrorMsg);
                return default(IEnumerable<T>);
            }

            using (IDbConnection dataConnection = new SqlConnection(_infrastructureSettings.AchievementRateDataBaseConnectionString))
            {
                var timer = Stopwatch.StartNew();
                var data = dataConnection.Query<T>(query, param, commandType: commandType);
                LogDependency(timer.Elapsed.TotalMilliseconds);

                return data;
            }
        }

        private void LogDependency(double elaspedMilliseconds)
        {
            var logEntry = new DependencyLogEntry
            {
                Identifier = "DatabaseQuery",
                ResponseTime = elaspedMilliseconds,
                Url = string.Empty
            };
            _logger.Debug("Database query", logEntry);
        }
    }
}
