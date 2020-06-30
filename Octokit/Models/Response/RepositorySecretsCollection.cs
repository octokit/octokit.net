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

        public RepositorySecretsCollection(int count, IReadOnlyList<RepositorySecret> secrets)
        {
            Count = count;
            Secrets = secrets;
        }

        /// <summary>
        /// The total count of secrets for the repository
        /// </summary>
        [Parameter(Key = "total_count")]
        public int Count { get; }

        /// <summary>
        /// The list of secrets for the repository
        /// </summary>
        [Parameter(Key = "secrets")]
        public IReadOnlyList<RepositorySecret> Secrets { get; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "RepositorySecretsCollection: Count: {0}", Count);
    }
}
