using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Commit : GitReference
    {
        public string Message { get; protected set; }
        public SignatureResponse Author { get; protected set; }
        public SignatureResponse Committer { get; protected set; }
        public GitReference Tree { get; protected set; }
        public IEnumerable<GitReference> Parents { get; protected set; }
        public int CommentCount { get; protected set; }
    }
}