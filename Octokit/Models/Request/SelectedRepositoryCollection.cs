using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Represents request to set the repositories with visibility to the secrets in an organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SelectedRepositoryCollection
    {
        public SelectedRepositoryCollection()
        {
        }

        public SelectedRepositoryCollection(IEnumerable<long> selectedRepositoryIds)
        {
            SelectedRepositoryIds = selectedRepositoryIds;
        }

        /// <summary>
        /// List of repository Ids that should have visibility to the repository
        /// </summary>
        [Parameter(Key = "selected_repository_ids")]
        public IEnumerable<long> SelectedRepositoryIds { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "SelectedRepositoryCollection: Count: {0}", SelectedRepositoryIds.Count());
    }
}
