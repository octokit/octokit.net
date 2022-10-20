using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRunBillableTiming
    {
        public WorkflowRunBillableTiming() { }

        public WorkflowRunBillableTiming(long totalMs, long jobs, IReadOnlyList<WorkflowRunTiming> jobRuns)
        {
            TotalMs = totalMs;
            Jobs = jobs;
            JobRuns = jobRuns;
        }

        /// <summary>
        /// The total billable milliseconds.
        /// </summary>
        public long TotalMs { get; private set; }

        /// <summary>
        /// The total number of jobs.
        /// </summary>
        public long Jobs { get; private set; }

        /// <summary>
        /// The billable job runs.
        /// </summary>
        public IReadOnlyList<WorkflowRunTiming> JobRuns { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "TotalMs: {0}, Jobs: {1}", TotalMs, Jobs);
            }
        }
    }
}
