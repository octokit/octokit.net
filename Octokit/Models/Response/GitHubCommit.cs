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

        public GitHubCommit(string nodeId, string url, string label, string @ref, string sha, User user, Repository repository, Author author, string commentsUrl, Commit commit, Author committer, string htmlUrl, GitHubCommitStats stats, IReadOnlyList<GitReference> parents, IReadOnlyList<GitHubCommitFile> files)
            : base(nodeId, url, label, @ref, sha, user, repository)
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

        /// <summary>
        /// Gets the GitHub account information for the commit author. It attempts to match the email
        /// address used in the commit with the email addresses registered with the GitHub account.
        /// If no account corresponds to the commit email, then this property is null.
        /// </summary>
        public Author Author { get; private set; }

        public string CommentsUrl { get; private set; }

        public Commit Commit { get; private set; }

        /// <summary>
        /// Gets the GitHub account information for the commit committer. It attempts to match the email
        /// address used in the commit with the email addresses registered with the GitHub account.
        /// If no account corresponds to the commit email, then this property is null.
        /// </summary>
        public Author Committer { get; private set; }

        public string HtmlUrl { get; private set; }

        public GitHubCommitStats Stats { get; private set; }

        public IReadOnlyList<GitReference> Parents { get; private set; }

        public IReadOnlyList<GitHubCommitFile> Files { get; private set; }
    }
}
