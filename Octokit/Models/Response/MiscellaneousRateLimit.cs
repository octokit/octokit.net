using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MiscellaneousRateLimit
    {
        public MiscellaneousRateLimit() { }

        public MiscellaneousRateLimit(ResourceRateLimit resources, RateLimit rate)
        {
            Ensure.ArgumentNotNull(resources, "resource");
            Ensure.ArgumentNotNull(rate, "rate");

            Resources = resources;
            Rate = rate;
        }

        /// <summary>
        /// Object of resources rate limits
        /// </summary>
        public ResourceRateLimit Resources { get; private set; }

        /// <summary>
        /// Legacy rate limit - to be depreciated - https://developer.github.com/v3/rate_limit/#deprecation-notice
        /// </summary>
        public RateLimit Rate { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return Resources == null ? "No rates found" : string.Format(CultureInfo.InvariantCulture, Resources.DebuggerDisplay);
            }
        }
    }
}
