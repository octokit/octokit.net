using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Merge : GitReference
    {
        public Merge() { }

        public Merge(string nodeId, string url, string label, string @ref, string sha, User user, Repository repository, Author author, Author committer, Commit commit, IEnumerable<GitReference> parents, string commentsUrl, int commentCount, string htmlUrl)
            : base(nodeId, url, label, @ref, sha, user, repository)
        {
            Ensure.ArgumentNotNull(parents, nameof(parents));

            Author = author;
            Committer = committer;
            Commit = commit;
            Parents = new ReadOnlyCollection<GitReference>(parents.ToList());
            CommentsUrl = commentsUrl;
            CommentCount = commentCount;
            HtmlUrl = htmlUrl;
        }

        public Author Author { get; private set; }
        public Author Committer { get; private set; }
        public Commit Commit { get; private set; }
        public IReadOnlyList<GitReference> Parents { get; private set; }
        public string CommentsUrl { get; private set; }
        public int CommentCount { get; private set; }
        public string HtmlUrl { get; private set; }
    }
}
