using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerGroupResponse
    {
        public RunnerGroupResponse()
        {
        }

        public RunnerGroupResponse(int totalCount, IReadOnlyList<RunnerGroup> runnerGroups)
        {
            TotalCount = totalCount;
            RunnerGroups = runnerGroups;
        }

        /// <summary>
        /// The total number of runner groups.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved runner groups.
        /// </summary>
        public IReadOnlyList<RunnerGroup> RunnerGroups { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, Runner Groups: {1}", TotalCount, RunnerGroups.Count);
    }
}
