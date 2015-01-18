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
        public Author Author { get; protected set; }

        public string CommentsUrl { get; protected set; }

        public Commit Commit { get; protected set; }

        public Author Committer { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public GitHubCommitStats Stats { get; protected set; }

        public IReadOnlyList<GitReference> Parents { get; protected set; }

        public IReadOnlyList<GitHubCommitFile> Files { get; protected set; }
    }
}
