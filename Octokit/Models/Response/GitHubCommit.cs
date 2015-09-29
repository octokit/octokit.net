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

        public GitHubCommit(string url, string label, string @ref, string sha, User user, Repository repository, Author author, string commentsUrl, Commit commit, Author committer, string htmlUrl, GitHubCommitStats stats, IReadOnlyList<GitReference> parents, IReadOnlyList<GitHubCommitFile> files)
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

        /// <summary>
        /// Gets the GitHub account information for the commit author. It attempts to match the email
        /// address used in the commit with the email addresses registered with the GitHub account.
        /// If no account corresponds to the commit email, then this property is null.
        /// </summary>
        public Author Author { get; protected set; }

        public string CommentsUrl { get; protected set; }

        public Commit Commit { get; protected set; }

        /// <summary>
        /// Gets the GitHub account information for the commit committer. It attempts to match the email
        /// address used in the commit with the email addresses registered with the GitHub account.
        /// If no account corresponds to the commit email, then this property is null.
        /// </summary>
        public Author Committer { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public GitHubCommitStats Stats { get; protected set; }

        public IReadOnlyList<GitReference> Parents { get; protected set; }

        public IReadOnlyList<GitHubCommitFile> Files { get; protected set; }
    }
}
