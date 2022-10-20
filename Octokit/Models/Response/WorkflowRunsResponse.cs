using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRunsResponse
    {
        public WorkflowRunsResponse()
        {
        }

        public WorkflowRunsResponse(int totalCount, IReadOnlyList<WorkflowRun> workflowRuns)
        {
            TotalCount = totalCount;
            WorkflowRuns = workflowRuns;
        }

        /// <summary>
        /// The total number of workflow runs.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved workflow runs.
        /// </summary>
        public IReadOnlyList<WorkflowRun> WorkflowRuns { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, WorkflowRuns: {1}", TotalCount, WorkflowRuns.Count);
    }
}
