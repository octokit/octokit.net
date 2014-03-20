using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Commit : GitReference
    {
        public string Message { get; set; }
        public Signature Author { get; set; }
        public Signature Committer { get; set; }
        public GitReference Tree { get; set; }
        public IEnumerable<GitReference> Parents { get; set; }
    }
}