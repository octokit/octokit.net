using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Commit : GitReference
    {
        public Commit() { }

        public Commit(string url, string label, string @ref, string sha, User user, Repository repository, string message, SignatureResponse author, SignatureResponse committer, GitReference tree, IEnumerable<GitReference> parents, int commentCount)
            : base(url, label, @ref, sha, user, repository)
        {
            Ensure.ArgumentNotNull(parents, "parents");

            Message = message;
            Author = author;
            Committer = committer;
            Tree = tree;
            Parents = new ReadOnlyCollection<GitReference>(parents.ToList());
            CommentCount = commentCount;
        }

        public string Message { get; protected set; }

        public SignatureResponse Author { get; protected set; }

        public SignatureResponse Committer { get; protected set; }

        public GitReference Tree { get; protected set; }

        public IReadOnlyList<GitReference> Parents { get; protected set; }

        public int CommentCount { get; protected set; }
    }
}
