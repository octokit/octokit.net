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
        public GitHubCommit() { }

        public GitHubCommit(string url, string label, string @ref, string sha, User user, Repository repository, CommitEntity author, string commentsUrl, Commit commit, CommitEntity committer, string htmlUrl, GitHubCommitStats stats, IReadOnlyList<GitReference> parents, IReadOnlyList<GitHubCommitFile> files)
            : base(url, label, @ref, sha, user, repository)
        {
            Author = author;
            CommentsUrl = commentsUrl;
            Commit = commit;
            Committer = committer;
            HtmlUrl = htmlUrl;
            Stats = stats;
            Parents = parents;
            Files = files;
        }

        public CommitEntity Author { get; protected set; }

        public string CommentsUrl { get; protected set; }

        public Commit Commit { get; protected set; }

        public CommitEntity Committer { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public GitHubCommitStats Stats { get; protected set; }

        public IReadOnlyList<GitReference> Parents { get; protected set; }

        public IReadOnlyList<GitHubCommitFile> Files { get; protected set; }
    }
}
