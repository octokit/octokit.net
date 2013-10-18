using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    public class RateLimit
#if !NETFX_CORE
        : ISerializable
#endif
    {
        const long _unixEpochTicks = 621355968000000000; // Unix Epoch is January 1, 1970 00:00 -0:00

        public RateLimit(IDictionary<string, string> responseHeaders)
        {
            Ensure.ArgumentNotNull(responseHeaders, "responseHeaders");

            Limit = (int) GetHeaderValueAsInt32Safe(responseHeaders, "X-RateLimit-Limit");
            Remaining = (int) GetHeaderValueAsInt32Safe(responseHeaders, "X-RateLimit-Remaining");
            Reset = FromUnixTime(GetHeaderValueAsInt32Safe(responseHeaders, "X-RateLimit-Reset"));
        }

        /// <summary>
        /// The maximum number of requests that the consumer is permitted to make per hour.
        /// </summary>
        public int Limit { get; private set; }

        /// <summary>
        /// The number of requests remaining in the current rate limit window.
        /// </summary>
        public int Remaining { get; private set; }

        /// <summary>
        /// The date and time at which the current rate limit window resets
        /// </summary>
        public DateTimeOffset Reset { get; private set; }

        static long GetHeaderValueAsInt32Safe(IDictionary<string, string> responseHeaders, string key)
        {
            string value;
            long result;
            return !responseHeaders.TryGetValue(key, out value) || value == null || !long.TryParse(value, out result)
                ? 0
                : result;
        }

        static DateTimeOffset FromUnixTime(long unixTime)
        {
            return new DateTimeOffset(unixTime*TimeSpan.TicksPerSecond + _unixEpochTicks, TimeSpan.Zero);
        }

#if !NETFX_CORE
        protected RateLimit(SerializationInfo info, StreamingContext context)
        {
            Ensure.ArgumentNotNull(info, "info");

            Limit = info.GetInt32("Limit");
            Remaining = info.GetInt32("Remaining");
            Reset = new DateTimeOffset(info.GetInt64("Reset"), TimeSpan.Zero);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Ensure.ArgumentNotNull(info, "info");

            info.AddValue("Limit", Limit);
            info.AddValue("Remaining", Remaining);
            info.AddValue("Reset", Reset.Ticks);
        }
#endif
    }
}
