using System;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility
{
    public interface IRetryWebRequest
    {
        T RetryWeb<T>(Func<T> action);
    }
}