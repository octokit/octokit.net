using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public int Number { get; protected set; }

        public PullRequest PullRequest { get; protected set; }

        public Installation Installation { get; protected set; }
    }
}
