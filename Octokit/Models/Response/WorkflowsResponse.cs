using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowsResponse
    {
        public WorkflowsResponse()
        {
        }

        public WorkflowsResponse(int totalCount, IReadOnlyList<Workflow> workflows)
        {
            TotalCount = totalCount;
            Workflows = workflows;
        }

        /// <summary>
        /// The total number of workflows.
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved workflows.
        /// </summary>
        public IReadOnlyList<Workflow> Workflows { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, Workflows: {1}", TotalCount, Workflows.Count);
    }
}
