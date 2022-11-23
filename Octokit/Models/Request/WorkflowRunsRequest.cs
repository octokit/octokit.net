using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Details to filter a workflow runs request, such as by branch or check suite Id.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRunsRequest : RequestParameters
    {
        /// <summary>
        /// Returns someone's workflow runs.
        /// </summary>
        public string Actor { get; set; }

        /// <summary>
        /// Returns workflow runs associated with a branch.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Returns workflow run triggered by the event you specify.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Only returns workflow runs that are associated with the specified head SHA.
        /// </summary>
        [Parameter(Key = "head_sha")]
        public string HeadSha { get; set; }

        /// <summary>
        /// Returns workflow runs with the check run status or conclusion that you specify.
        /// </summary>
        public StringEnum<CheckRunStatusFilter> Status { get; set; }

        /// <summary>
        /// Returns workflow runs created within the given date-time range.
        /// </summary>
        public string Created { get; set; }

        /// <summary>
        /// If true pull requests are omitted from the response.
        /// </summary>
        [Parameter(Key = "exclude_pull_requests")]
        public bool? ExcludePullRequests { get; set; }

        /// <summary>
        /// Returns workflow runs with the check_suite_id that you specify.
        /// </summary>
        [Parameter(Key = "check_suite_id")]
        public long? CheckSuiteId { get; set; }

        internal string DebuggerDisplay => string.Format(
            CultureInfo.InvariantCulture,
            "Actor: {0}, Branch: {1}, Event: {2}, Status: {3}, Created: {4}, ExcludePullRequests: {5}, CheckSuiteId: {6}, HeadSha: {7}",
            Actor,
            Branch,
            Event,
            Status,
            Created,
            ExcludePullRequests,
            CheckSuiteId,
            HeadSha);
    }
}
