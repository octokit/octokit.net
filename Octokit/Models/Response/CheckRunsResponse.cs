using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunsResponse
    {
        public CheckRunsResponse()
        {
        }

        public CheckRunsResponse(int totalCount, IReadOnlyList<CheckRun> checkRuns)
        {
            TotalCount = totalCount;
            CheckRuns = checkRuns;
        }

        /// <summary>
        /// The total number of check runs that match the request filter
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved check runs
        /// </summary>
        public IReadOnlyList<CheckRun> CheckRuns { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, CheckRuns: {1}", TotalCount, CheckRuns.Count);
    }
}
