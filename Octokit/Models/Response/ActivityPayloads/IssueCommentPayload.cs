using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueCommentPayload : ActivityPayload
    {
        // should always be "created" according to github api docs
        public string Action { get; private set; }
        public Issue Issue { get; private set; }
        public IssueComment Comment { get; private set; }
    }
}
