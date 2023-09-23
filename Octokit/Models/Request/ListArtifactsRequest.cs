using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to filter requests for lists of artifacts.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ListArtifactsRequest : RequestParameters
    {
        /// <summary>
        /// Filter artifacts by name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// How many results to return per page (maximum 100).
        /// </summary>
        [Parameter(Key = "per_page")]
        public int PerPage { get; set; } = 30;

        /// <summary>
        /// What page to retrieve.
        /// </summary>
        [Parameter(Key = "page")]
        public int Page { get; set; } = 1;

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Page: {0}, PerPage: {1} ", Page, PerPage);
    }
}
