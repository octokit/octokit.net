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
            List<string> output = null;
            if (input == null)
                return output;

            output = new List<string>();

            foreach (var item in input)
            {
                output.Add(new String(item.ToCharArray()));
            }

            return output;
        }

        public static IDictionary<string, Uri> Clone(this IReadOnlyDictionary<string, Uri> input)
        {
            Dictionary<string, Uri> output = null;
            if (input == null)
                return output;

            output = new Dictionary<string, Uri>();
            
            foreach (var item in input)
            {
                output.Add(new String(item.Key.ToCharArray()), new Uri(item.Value.ToString()));
            }

            return output;
        }
    }
}
