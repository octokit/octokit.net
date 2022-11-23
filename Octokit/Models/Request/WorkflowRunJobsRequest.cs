using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Details to filter a workflow run jobs request, such as by the latest attempt.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRunJobsRequest : RequestParameters
    {
        /// <summary>
        /// Filters jobs by their <c>completed_at</c> timestamp.
        /// </summary>
        public StringEnum<WorkflowRunJobsFilter> Filter { get; set; }

        internal string DebuggerDisplay => string.Format(
            CultureInfo.InvariantCulture,
            "Filter: {0}",
            Filter);
    }
}
