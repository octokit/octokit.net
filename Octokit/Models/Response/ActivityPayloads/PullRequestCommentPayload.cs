using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestCommentPayload : ActivityPayload
    {
        public string Action { get; private set; }
        public PullRequest PullRequest { get; private set; }
        public PullRequestReviewComment Comment { get; private set; }
    }
}
