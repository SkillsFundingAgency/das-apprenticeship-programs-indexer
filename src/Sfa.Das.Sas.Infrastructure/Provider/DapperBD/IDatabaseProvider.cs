using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.Infrastructure.Provider.DapperBD
{
    public interface IDatabaseProvider
    {
        IEnumerable<T> Query<T>(string query, object param = null);

        T ExecuteScalar<T>(string query);
    }
}