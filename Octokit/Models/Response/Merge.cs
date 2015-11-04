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

        public Merge(Author author, Author committer, Commit commit, IEnumerable<GitReference> parents, string commentsUrl, int commentCount, string htmlUrl)
        {
            Ensure.ArgumentNotNull(parents, "parents");

            Author = author;
            Committer = committer;
            Commit = commit;
            Parents = new ReadOnlyCollection<GitReference>(parents.ToList());
            CommentsUrl = commentsUrl;
            CommentCount = commentCount;
            HtmlUrl = htmlUrl;
        }

        public Author Author { get; protected set; }
        public Author Committer { get; protected set; }
        public Commit Commit { get; protected set; }
        public IReadOnlyList<GitReference> Parents { get; protected set; }
        public string CommentsUrl { get; protected set; }
        public int CommentCount { get; protected set; }
        public string HtmlUrl { get; protected set; }
    }
}