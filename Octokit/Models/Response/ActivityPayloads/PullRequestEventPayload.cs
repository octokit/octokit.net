using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestEventPayload : ActivityPayload
    {
        public string Action { get; private set; }
        public int Number { get; private set; }

        public PullRequest PullRequest { get; private set; }
    }
}
