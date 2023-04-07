using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerApplicationResponse
    {
        public RunnerApplicationResponse()
        {
        }

        public RunnerApplicationResponse(int totalCount, IReadOnlyList<RunnerApplication> runnerApplications)
        {
            TotalCount = totalCount;
            RunnerApplications = runnerApplications;
        }

        /// <summary>
        /// The total number of runners.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved runners.
        /// </summary>
        public IReadOnlyList<RunnerApplication> RunnerApplications { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, Runner Applications: {1}", TotalCount, RunnerApplications.Count);
    }
}
