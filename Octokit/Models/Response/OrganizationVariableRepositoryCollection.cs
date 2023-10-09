using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents response of the repositories for a variable in an organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationVariableRepositoryCollection
    {
        public OrganizationVariableRepositoryCollection()
        {
        }

        public OrganizationVariableRepositoryCollection(int count, IReadOnlyList<Repository> repositories)
        {
            Count = count;
            Repositories = repositories;
        }

        /// <summary>
        /// The total count of repositories with visibility to the variable in the organization
        /// </summary>
        [Parameter(Key = "total_count")]
        public int Count { get; private set; }

        /// <summary>
        /// The list of repositories with visibility to the variable in the organization
        /// </summary>
        [Parameter(Key = "repositories")]
        public IReadOnlyList<Repository> Repositories { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "OrganizationVariableRepositoryCollection: Count: {0}", Count);
    }
}
