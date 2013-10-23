using System.Collections.Generic;
using System.Linq;

namespace Octokit
{
    internal static class CollectionExtensions
    {
        public static TValue SafeGet<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            Ensure.ArgumentNotNull(dictionary, "dictionary");

            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }
    }
}
