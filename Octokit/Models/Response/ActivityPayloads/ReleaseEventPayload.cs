using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseEventPayload : ActivityPayload
    {
        public string Action { get; private set; }

        public Release Release { get; private set; }
    }
}
