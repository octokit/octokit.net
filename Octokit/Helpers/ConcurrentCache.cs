using System;
using System.Collections.Immutable;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// This is in lieu of ConcurrentDictionary. PCL runtime, specifically windows phone 8 
    /// does not have access to ConcurrentDictionary.
    /// Source of implementation is here:  
    /// http://stackoverflow.com/questions/18367839/alternative-to-concurrentdictionary-for-portable-class-library
    /// Relies on Microsoft.BCL.Immutable for the ImmutableDictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ConcurrentCache<TKey, TValue>
    {
        IImmutableDictionary<TKey, TValue> _cache = ImmutableDictionary.Create<TKey, TValue>();

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            //cannot be null
            if (valueFactory == null)
                throw new ArgumentNullException("valueFactory");

            TValue newValue = default(TValue);
            bool newValueCreated = false;
            while (true)
            {
                var oldCache = _cache;
                TValue value;
                if (oldCache.TryGetValue(key, out value))
                    return value;

                // Value not found; create it if necessary
                if (!newValueCreated)
                {
                    newValue = valueFactory(key);
                    newValueCreated = true;
                }

                // Add the new value to the cache
                var newCache = oldCache.Add(key, newValue);
                if (Interlocked.CompareExchange(ref _cache, newCache, oldCache) == oldCache)
                {
                    // Cache successfully written
                    return newValue;
                }

                // Failed to write the new cache because another thread
                // already changed it; try again.

            }
        }
    }
}
