using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowJobsResponse
    {
        public WorkflowJobsResponse()
        {
        }

        public WorkflowJobsResponse(int totalCount, IReadOnlyList<WorkflowJob> jobs)
        {
            TotalCount = totalCount;
            Jobs = jobs;
        }

        /// <summary>
        /// The total number of workflow runs.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved workflow runs.
        /// </summary>
        public IReadOnlyList<WorkflowJob> Jobs { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, Jobs: {1}", TotalCount, Jobs.Count);
    }
}
