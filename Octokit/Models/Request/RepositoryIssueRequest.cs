using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryIssueRequest : IssueRequest
    {
        /// <summary>
        /// Identifies a filter for the milestone. Use "*" for issues with any milestone.
        /// Use the milestone number for a specific milestone. Use the value "none" for issues with any milestones.
        /// </summary>
        public string Milestone { get; set; }

        /// <summary>
        /// Filter on the user assigned for the request
        /// </summary>
        /// <remarks>
        /// Specify "none" for issues with no assigned user
        /// </remarks>
        public string Assignee { get; set; }

        /// <summary>
        /// The user that created the issue
        /// </summary>
        /// <remarks>
        /// Specify "none" for issues with no assigned user
        /// </remarks>
        public string Creator { get; set; }

        /// <summary>
        /// A user that’s mentioned in the issue
        /// </summary>
        public string Mentioned { get; set; }
    }
}
