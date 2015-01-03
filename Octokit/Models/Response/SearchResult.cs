using System;
using System.Collections.Generic;
using System.Globalization;


namespace Octokit.Internal
{
    public abstract class SearchResult<T>
    {
        /// <summary>
        /// Total number of matching items.
        /// </summary>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// True if the query timed out and it's possible that the results are incomplete.
        /// </summary>
        public bool IncompleteResults { get; protected set; }

        /// <summary>
        /// The found items. Up to 100 per page.
        /// </summary>
        public IReadOnlyList<T> Items { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalCount: {0}", TotalCount);
            }
        }
    }
}
