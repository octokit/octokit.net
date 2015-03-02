using System.Diagnostics;

namespace Octokit.Models.Response.ActivityPayloads
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestCommentPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public PullRequest PullRequest { get; protected set; }
        public PullRequestReviewComment Comment { get; protected set; }
    }
}
