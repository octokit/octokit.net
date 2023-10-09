using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents response of variables for a repository.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryVariablesCollection
    {
        public RepositoryVariablesCollection()
        {
        }

        public RepositoryVariablesCollection(int totalCount, IReadOnlyList<RepositoryVariable> variables)
        {
            TotalCount = totalCount;
            Variables = variables;
        }

        /// <summary>
        /// The total count of variables for the repository
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The list of variables for the repository
        /// </summary>
        public IReadOnlyList<RepositoryVariable> Variables { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "RepositoryVariablesCollection: Count: {0}", TotalCount);
    }
}
