using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD
{
    public interface IDatabaseProvider
    {
        IEnumerable<T> Query<T>(string query, object param = null);

        IEnumerable<T> QueryStoredProc<T>(string query, object param = null);
    }
}