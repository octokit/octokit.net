#if PORTABLE
using System;
using System.Collections.Generic;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// This is in lieu of ConcurrentDictionary. PCL runtime, specifically windows phone 8 
    /// does not have access to ConcurrentDictionary. We just use a lock.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ConcurrentCache<TKey, TValue>
    {
        Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            //cannot be null
            if (valueFactory == null)
                throw new ArgumentNullException("valueFactory");

            lock (_cache) 
            {
                if (_cache.ContainsKey(key)) 
                {
                    return _cache[key];
                }

                var ret = valueFactory(key);
                _cache[key] = ret;
                return ret;
            }
        }
    }
}
#endif