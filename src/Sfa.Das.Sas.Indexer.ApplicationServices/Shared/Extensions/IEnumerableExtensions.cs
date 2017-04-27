using System;
using System.Collections.Generic;

namespace Sfa.Das.Sas.Indexer.ApplicationServices.Shared.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<string> seenKeys = new HashSet<string>();
            foreach (TSource element in source)
            {
                var key = keySelector(element).ToString().ToLower();
                if (seenKeys.Add(key))
                {
                    yield return element;
                }
            }
        }
    }
}