using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to request and filter a list of repository forks.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryForksListRequest : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryForksListRequest"/> class.
        /// </summary>
        public RepositoryForksListRequest()
        {
            Sort = Sort.Newest; // Default in accordance with the documentation
        }

        /// <summary>
        /// Gets or sets the sort property.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public Sort Sort { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sort: {0}", Sort);
            }
        }
    }

    /// <summary>
    /// The sort order.
    /// </summary>
    public enum Sort
    {
        /// <summary>
        /// Sort by date and show the newest first.
        /// </summary>
        Newest,

        /// <summary>
        /// Sort by date and show the oldest first.
        /// </summary>
        Oldest,

        /// <summary>
        /// Sort by the number of stargazers.
        /// </summary>
        Stargazers
    }
}
