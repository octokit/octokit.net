using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public Issue Issue { get; protected set; }
    }
}
