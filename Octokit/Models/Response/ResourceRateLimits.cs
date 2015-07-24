using System;
using System.Diagnostics;
using System.Globalization;

using Octokit.Helpers;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ResourceRateLimits
    {
        public ResourceRateLimits() {}

        public ResourceRateLimits(ResourceRateLimit core, ResourceRateLimit search)
        {
            Ensure.ArgumentNotNull(core, "core");
            Ensure.ArgumentNotNull(search, "search");

            Core = core;
            Search = search;
        }

        /// <summary>
        /// Rate limits for core API (rate limit for everything except Search API)
        /// </summary>
        public ResourceRateLimit Core { get; private set; }

        /// <summary>
        /// Rate Limits for Search API
        /// </summary>
        public ResourceRateLimit Search { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Core: {0}; Search: {1} ", Core.DebuggerDisplay, Search.DebuggerDisplay);
            }
        }
    }

}
