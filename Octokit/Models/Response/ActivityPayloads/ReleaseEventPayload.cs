using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }

        public Release Release { get; protected set; }
    }
}
