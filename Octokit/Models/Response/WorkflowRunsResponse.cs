using System.Collections.Generic;
using System.Diagnostics;

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

        public int TotalCount { get; protected set; }

        public IReadOnlyList<WorkflowRun> WorkflowRuns { get; protected set; }

        internal string DebuggerDisplay
            => $"TotalCount: {TotalCount}, WorkflowRuns: {WorkflowRuns.Count}";
    }
}
