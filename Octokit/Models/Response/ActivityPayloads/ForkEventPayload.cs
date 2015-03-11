using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ForkEventPayload : ActivityPayload
    {
        public Repository Forkee { get; protected set; }
    }
}
