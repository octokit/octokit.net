using System;
using System.Collections.Generic;
using System.Globalization;

namespace Octokit.Internal
{
    public abstract class SearchResult<T>
    {
        protected SearchResult() { }

        protected SearchResult(int totalCount, bool incompleteResults, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            IncompleteResults = incompleteResults;
            Items = items;
        }

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
                return string.Format(CultureInfo.InvariantCulture, "TotalCount: {0}", TotalCount);
            }
        }
    }
}
