using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class StarredEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
    }
}
