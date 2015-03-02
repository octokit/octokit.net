using System.Diagnostics;

namespace Octokit.Models.Response.ActivityPayloads
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitCommentPayload : ActivityPayload
    {
        public CommitComment Comment { get; protected set; }
    }
}
