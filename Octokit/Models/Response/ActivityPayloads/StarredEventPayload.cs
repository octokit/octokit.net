using System.Diagnostics;

namespace Octokit.Models.Response.ActivityPayloads
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class StarredEventPayload : ActivityPayload
    {
        public string Action { get; protected set; }
    }
}
