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

        public WorkflowJobsResponse(int totalCount, IReadOnlyList<WorkflowJob> workflowJobs)
        {
            TotalCount = totalCount;
            WorkflowJobs = workflowJobs;
        }

        public int TotalCount { get; protected set; }

        public IReadOnlyList<WorkflowJob> WorkflowJobs { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, WorkflowJobs: {1}", TotalCount, WorkflowJobs.Count);
    }
}
