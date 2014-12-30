using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Commit : GitReference
    {
        public string Message { get; set; }
        public SignatureResponse Author { get; set; }
        public SignatureResponse Committer { get; set; }
        public GitReference Tree { get; set; }
        public IEnumerable<GitReference> Parents { get; set; }
        public int CommentCount { get; set; }
    }
}