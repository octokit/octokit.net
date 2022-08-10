using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using Octokit.Internal;

namespace Octokit
{
    [Serializable]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RateLimit : ISerializable
    {
        public RateLimit() { }

        public RateLimit(IDictionary<string, string> responseHeaders)
        {
            Ensure.ArgumentNotNull(responseHeaders, nameof(responseHeaders));

            Limit = (int)GetHeaderValueAsInt32Safe(responseHeaders, "X-RateLimit-Limit");
            Remaining = (int)GetHeaderValueAsInt32Safe(responseHeaders, "X-RateLimit-Remaining");
            ResetAsUtcEpochSeconds = GetHeaderValueAsInt32Safe(responseHeaders, "X-RateLimit-Reset");
        }

        public RateLimit(int limit, int remaining, long resetAsUtcEpochSeconds)
        {
            Ensure.ArgumentNotNull(limit, nameof(limit));
            Ensure.ArgumentNotNull(remaining, nameof(remaining));
            Ensure.ArgumentNotNull(resetAsUtcEpochSeconds, nameof(resetAsUtcEpochSeconds));

            Limit = limit;
            Remaining = remaining;
            ResetAsUtcEpochSeconds = resetAsUtcEpochSeconds;
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
        [Parameter(Key = "ignoreThisField")]
        public DateTimeOffset Reset => DateTimeOffset.FromUnixTimeSeconds(ResetAsUtcEpochSeconds);

        /// <summary>
        /// The date and time at which the current rate limit window resets - in UTC epoch seconds
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Parameter(Key = "reset")]
        public long ResetAsUtcEpochSeconds { get; private set; }

        static KeyValuePair<string, string> LookupHeader(IDictionary<string, string> headers, string key)
        {
            return headers.FirstOrDefault(h => string.Equals(h.Key, key, StringComparison.OrdinalIgnoreCase));
        }

        static bool Exists(KeyValuePair<string, string> kvp)
        {
            return !kvp.Equals(default(KeyValuePair<string, string>));
        }

        static long GetHeaderValueAsInt32Safe(IDictionary<string, string> responseHeaders, string key)
        {
            long result;

            var foundKey = LookupHeader(responseHeaders, key);
            if (!Exists(foundKey))
            {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(foundKey.Value))
            {
                return 0;
            }

            return !long.TryParse(foundKey.Value, out result)
                ? 0
                : result;
        }

        protected RateLimit(SerializationInfo info, StreamingContext context)
        {
            Ensure.ArgumentNotNull(info, nameof(info));

            Limit = info.GetInt32("Limit");
            Remaining = info.GetInt32("Remaining");
            ResetAsUtcEpochSeconds = info.GetInt64("ResetAsUtcEpochSeconds");
        }

        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Ensure.ArgumentNotNull(info, nameof(info));

            info.AddValue("Limit", Limit);
            info.AddValue("Remaining", Remaining);
            info.AddValue("ResetAsUtcEpochSeconds", ResetAsUtcEpochSeconds);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Limit {0}, Remaining {1}, Reset {2} ", Limit, Remaining, Reset);
            }
        }

        /// <summary>
        /// Allows you to clone RateLimit
        /// </summary>
        /// <returns>A clone of <seealso cref="RateLimit"/></returns>
        public RateLimit Clone()
        {
            return new RateLimit
            {
                Limit = Limit,
                Remaining = Remaining,
                ResetAsUtcEpochSeconds = ResetAsUtcEpochSeconds
            };
        }
    }
}
