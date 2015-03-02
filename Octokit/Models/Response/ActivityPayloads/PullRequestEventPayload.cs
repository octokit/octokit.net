using System.Diagnostics;

namespace Octokit.Models.Response.ActivityPayloads
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public int Number { get; protected set; }

        public PullRequest PullRequest { get; protected set; }
    }
}
