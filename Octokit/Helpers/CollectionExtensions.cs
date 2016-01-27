using System;
using System.Linq;
using System.Collections.Generic;

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

        public static IList<string> Clone(this IReadOnlyList<string> input)
        {
            if (input == null)
                return null;

            return input.Select(item => new string(item.ToCharArray())).ToList();
        }

        public static IDictionary<string, Uri> Clone(this IReadOnlyDictionary<string, Uri> input)
        {
            if (input == null)
                return null;

            return input.ToDictionary(item => new string(item.Key.ToCharArray()), item => new Uri(item.Value.ToString()));
        }
    }
}
