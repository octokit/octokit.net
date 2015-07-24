using System;
using System.Diagnostics;
using System.Globalization;

using Octokit.Helpers;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ResourceRateLimit
    {
        public ResourceRateLimit() { }

        public ResourceRateLimit(int limit, int remaining, long reset)
        {
            Ensure.ArgumentNotNull(limit, "limit");
            Ensure.ArgumentNotNull(remaining, "remaining");
            Ensure.ArgumentNotNull(reset, "reset");

            Limit = limit;
            Remaining = remaining;
            Reset = reset;
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
        /// The date and time at which the current rate limit window resets - in UTC epoch seconds
        /// </summary>
        public long Reset { get; private set; }

        /// <summary>
        /// The date and time at which the current rate limit window resets - as DateTimeOffset
        /// </summary>
        public DateTimeOffset ResetAsDateTimeOffset { get { return Reset.FromUnixTime(); } }


        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Limit {0}, Remaining {1}, Reset {2} ", Limit, Remaining, Reset);
            }
        }
    }
}
