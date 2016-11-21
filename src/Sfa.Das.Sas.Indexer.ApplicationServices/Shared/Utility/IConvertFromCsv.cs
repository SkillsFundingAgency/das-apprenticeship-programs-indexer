using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Utility
{
    public interface IConvertFromCsv
    {
        ICollection<T> CsvTo<T>(string result)
            where T : class, new();
    }
}