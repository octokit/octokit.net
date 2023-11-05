using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ResourceRateLimit
    {
        public ResourceRateLimit() { }

        public ResourceRateLimit(RateLimit core, RateLimit search, RateLimit graphQL)
        {
            Ensure.ArgumentNotNull(core, nameof(core));
            Ensure.ArgumentNotNull(search, nameof(search));
            Ensure.ArgumentNotNull(graphQL, nameof(graphQL));

            Core = core;
            Search = search;
            Graphql = graphQL;
        }


        /// <summary>
        /// Rate limits for core API
        /// </summary>
        public RateLimit Core { get; private set; }

        /// <summary>
        /// Rate Limits for Search API
        /// </summary>
        public RateLimit Search { get; private set; }

        /// <summary>
        /// Rate Limits for GraphQL API
        /// </summary>
        public RateLimit Graphql { get; private set; }



        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Core: {0}; Search: {1}; GraphQL: {2} ", 
                    Core.DebuggerDisplay, Search.DebuggerDisplay, Graphql.DebuggerDisplay);
            }
        }
    }
}
