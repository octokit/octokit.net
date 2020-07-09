using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestCommit
    {
        public PullRequestCommit() { }

        public PullRequestCommit(string nodeId, User author, string commentsUrl, Commit commit, User committer, string htmlUrl, IEnumerable<GitReference> parents, string sha, string url)
        {
            Ensure.ArgumentNotNull(parents, nameof(parents));

            NodeId = nodeId;
            Author = author;
            CommentsUrl = commentsUrl;
            Commit = commit;
            Committer = committer;
            HtmlUrl = htmlUrl;
            Parents = new ReadOnlyCollection<GitReference>(parents.ToList());
            Sha = sha;
            Url = url;
        }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        public User Author { get; protected set; }

        public string CommentsUrl { get; protected set; }

        public Commit Commit { get; protected set; }

        public User Committer { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public IReadOnlyList<GitReference> Parents { get; protected set; }

        public string Sha { get; protected set; }

        public string Url { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                var name = Commit != null && Commit.Author != null ? Commit.Author.Name : "";
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0} Author: {1}", Sha, name);
            }
        }
    }
}
