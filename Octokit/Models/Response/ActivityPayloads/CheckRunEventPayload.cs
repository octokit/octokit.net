using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunEventPayload : ActivityPayload
    {
        public string Action { get; private set; }
        public CheckRun CheckRun { get; private set; }
        public CheckRunRequestedAction RequestedAction { get; private set; }
    }
}
