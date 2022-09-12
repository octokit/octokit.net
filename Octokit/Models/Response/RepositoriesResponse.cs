using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoriesResponse
    {
        public RepositoriesResponse()
        {
        }

        public RepositoriesResponse(int totalCount, IReadOnlyList<Repository> repositories)
        {
            TotalCount = totalCount;
            Repositories = repositories;
        }

        /// <summary>
        /// The total number of check suites that match the request filter
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved check suites
        /// </summary>
        public IReadOnlyList<Repository> Repositories { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, Repositories: {1}", TotalCount, Repositories.Count);
    }
}
