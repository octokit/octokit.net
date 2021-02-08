using System.Diagnostics;

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
        public string Actor { get; set; }

        /// <summary>
        /// Filters workflow runs by branch name
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Filters workflow runs by event type (e.g., 'push', 'pull_request', or 'issue')
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Returns workflow runs associated with the check run 'status' or 'conclusion' specified (e.g., 'success' or 'completed')
        /// </summary>
        public string Status { get; set; }

        internal string DebuggerDisplay
            => $"Actor: {Actor}, Branch: {Branch}, Event: {Event}, Status: {Status}";
    }
}
