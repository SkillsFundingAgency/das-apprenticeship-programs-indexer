﻿namespace Sfa.Das.Sas.Indexer.Infrastructure.Services
{
    using System;
    using System.Net;
    using Polly;
    using SFA.DAS.NLog.Logger;
    using Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility;

    public class RetryService : IRetryWebRequest
    {
        private readonly ILog _log;

        public RetryService(ILog log)
        {
            _log = log;
        }

        public T RetryWeb<T>(Func<T> action)
        {
            var policy = Policy.Handle<WebException>(e => e.Status != WebExceptionStatus.Success)
                .WaitAndRetry(
                    2,
                    retrytime => TimeSpan.FromSeconds(Math.Pow(2, retrytime)),
                    (exception, timespan) => { _log.Error(exception, "Failed to connect to site"); });
            return policy.Execute(action);
        }
    }
}