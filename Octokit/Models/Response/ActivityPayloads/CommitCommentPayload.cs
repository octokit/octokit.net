using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitCommentPayload : ActivityPayload
    {
        public CommitComment Comment { get; private set; }
    }
}
