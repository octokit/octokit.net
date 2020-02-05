using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Details to filter a workflow runs request
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowRunsRequest : RequestParameters
    {
        /// <summary>
        /// Filters workflow runs by user login
        /// </summary>
        [Parameter(Key = "actor")]
        public string Actor { get; set; }

        /// <summary>
        /// Filters workflow runs by branch name
        /// </summary>
        [Parameter(Key = "branch")]
        public string Branch { get; set; }

        /// <summary>
        /// Filters workflow runs by event type (e.g., 'push', 'pull_request', or 'issue')
        /// </summary>
        [Parameter(Key = "event")]
        public string Event { get; set; }

        /// <summary>
        /// Returns workflow runs associated with the check run 'status' or 'conclusion' specified (e.g., 'success' or 'completed')
        /// </summary>
        [Parameter(Key = "status")]
        public string Status { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Actor: {0}, Branch: {1}, Event: {2}, Status: {3}", Actor, Branch, Event, Status);
    }
}
