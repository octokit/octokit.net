using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to request and filter a list of repositories.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryRequest : RequestParameters
    {
        /// <summary>
        /// Gets or sets the repository type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public RepositoryType Type { get; set; }

        /// <summary>
        /// Gets or sets the sort property.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public RepositorySort Sort { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public SortDirection Direction { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Type: {0}, Sort: {1}, Direction: {2}", Type, Sort, Direction);
            }
        }
    }

    /// <summary>
    /// The properties that repositories can be filtered by.
    /// </summary>
    public enum RepositoryType
    {
        /// <summary>
        /// Return all repositories.
        /// </summary>
        All,

        /// <summary>
        /// Return repositories that the current authenticated user owns.
        /// </summary>
        Owner,

        /// <summary>
        /// Returns public repositoires.
        /// </summary>
        Public,

        /// <summary>
        /// The privateReturn private repositories.
        /// </summary>
        Private,

        /// <summary>
        /// Return repositories for which the current authenticated user is a member of the org or team.
        /// </summary>
        Member
    }

    /// <summary>
    /// The properties that repositories can be sorted by.
    /// </summary>
    public enum RepositorySort
    {
        /// <summary>
        /// Sort by the date the repository was created.
        /// </summary>
        Created,

        /// <summary>
        /// Sort by the date the repository was last updated.
        /// </summary>
        Updated,

        /// <summary>
        /// Sort by the date the repository was last pushed.
        /// </summary>
        Pushed,

        /// <summary>
        /// Sort by the repository name.
        /// </summary>
        [Parameter(Value = "full_name")]
        FullName
    }
}
