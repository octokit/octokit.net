using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewEventPayload : ActivityPayload
    {
        public string Action { get; private set; }
        public PullRequest PullRequest { get; private set; }
        public PullRequestReview Review { get; private set; }
    }
}
