using System.Diagnostics;

namespace Octokit.Models.Response.ActivityPayloads
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
        public Issue Issue { get; protected set; }
        public User Assignee { get; protected set; }
        public Label Label { get; protected set; }
    }
}
