using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerResponse
    {
        public RunnerResponse()
        {
        }

        public RunnerResponse(int totalCount, IReadOnlyList<Runner> runners)
        {
            TotalCount = totalCount;
            Runners = runners;
        }

        /// <summary>
        /// The total number of runners.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved runners.
        /// </summary>
        public IReadOnlyList<Runner> Runners { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, Runners: {1}", TotalCount, Runners.Count);
    }
}
