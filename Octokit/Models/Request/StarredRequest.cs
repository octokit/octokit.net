using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to retrieve and filter lists of stars.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class StarredRequest : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StarredRequest"/> class.
        /// </summary>
        public StarredRequest()
        {
            SortProperty = StarredSort.Created;
            SortDirection = SortDirection.Ascending;
        }

        /// <summary>
        /// Gets or sets the sort property.
        /// </summary>
        /// <value>
        /// The sort property.
        /// </value>
        [Parameter(Key = "sort")]
        public StarredSort SortProperty { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        [Parameter(Key = "direction")]
        public SortDirection SortDirection { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "SortProperty: {0} SortDirection: {1}", SortProperty, SortDirection);
            }
        }
    }

    /// <summary>
    /// Property to sort stars by.
    /// </summary>
    public enum StarredSort
    {
        /// <summary>
        /// Sort y the date the star was created.
        /// </summary>
        Created,

        /// <summary>
        /// Sort by the date the star was last updated.
        /// </summary>
        Updated
    }
}
