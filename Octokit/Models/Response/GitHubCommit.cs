using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// An enhanced git commit containing links to additional resources
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitHubCommit : GitReference
    {
        public Author Author { get; set; }
        public string CommentsUrl { get; set; }
        public Commit Commit { get; set; }
        public Author Committer { get; set; }
        public string HtmlUrl { get; set; }
        public IReadOnlyList<GitReference> Parents { get; set; }
    }
}
