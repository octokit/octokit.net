using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InstallationsResponse
    {
        public InstallationsResponse()
        {
        }

        public InstallationsResponse(int totalCount, IReadOnlyList<Installation> installations)
        {
            TotalCount = totalCount;
            Installations = installations;
        }

        /// <summary>
        /// The total number of check suites that match the request filter
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved check suites
        /// </summary>
        public IReadOnlyList<Installation> Installations { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, Installations: {1}", TotalCount, Installations.Count);
    }
}
