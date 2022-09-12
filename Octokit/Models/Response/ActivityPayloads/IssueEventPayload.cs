using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueEventPayload : ActivityPayload
    {
        public string Action { get; private set; }
        public Issue Issue { get; private set; }
    }
}
