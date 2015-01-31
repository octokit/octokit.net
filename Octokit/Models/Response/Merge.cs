using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Merge : GitReference
    {
        public Merge() { }

        public Merge(Author author, Author committer, Commit commit, IEnumerable<GitReference> parents, string commentsUrl, int commentCount, string htmlUrl)
        {
            Author = author;
            Committer = committer;
            Commit = commit;
            Parents = parents;
            CommentsUrl = commentsUrl;
            CommentCount = commentCount;
            HtmlUrl = htmlUrl;
        }

        public Author Author { get; protected set; }
        public Author Committer { get; protected set; }
        public Commit Commit { get; protected set; }
        public IEnumerable<GitReference> Parents { get; protected set; }
        public string CommentsUrl { get; protected set; } 
        public int CommentCount { get; protected set; }
        public string HtmlUrl { get; protected set; }
    }
}