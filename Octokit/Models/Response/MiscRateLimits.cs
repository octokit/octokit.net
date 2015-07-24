using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MiscRateLimits
    {
        public MiscRateLimits() {}

        public MiscRateLimits(ResourceRateLimits resources, ResourceRateLimit rate)
        {
            Ensure.ArgumentNotNull(resources, "resource");
            Ensure.ArgumentNotNull(rate, "rate");

            Resources = resources;
            Rate = rate;
        }

        /// <summary>
        /// Object of resources rate limits
        /// </summary>
        public ResourceRateLimits Resources { get; private set; }

        /// <summary>
        /// Legacy rate limit - to be depreciated - https://developer.github.com/v3/rate_limit/#deprecation-notice
        /// </summary>
        public ResourceRateLimit Rate { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return Resources == null ? "No rates found" : String.Format(CultureInfo.InvariantCulture, Resources.DebuggerDisplay);
            }
        }
    }
}
