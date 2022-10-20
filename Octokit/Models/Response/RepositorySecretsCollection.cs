using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents response of secrets for a repository.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositorySecretsCollection
    {
        public RepositorySecretsCollection()
        {
        }

        public RepositorySecretsCollection(int totalCount, IReadOnlyList<RepositorySecret> secrets)
        {
            TotalCount = totalCount;
            Secrets = secrets;
        }

        /// <summary>
        /// The total count of secrets for the repository
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The list of secrets for the repository
        /// </summary>
        public IReadOnlyList<RepositorySecret> Secrets { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "RepositorySecretsCollection: Count: {0}", TotalCount);
    }
}
