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

        public Commit(string nodeId, string url, string label, string @ref, string sha, User user, Repository repository, string message, Committer author, Committer committer, GitReference tree, IEnumerable<GitReference> parents, int commentCount, Verification verification)
            : base(nodeId, url, label, @ref, sha, user, repository)
        {
            Ensure.ArgumentNotNull(parents, nameof(parents));

            Message = message;
            Author = author;
            Committer = committer;
            Tree = tree;
            Parents = new ReadOnlyCollection<GitReference>(parents.ToList());
            CommentCount = commentCount;
            Verification = verification;
        }

        public string Message { get; private set; }

        public Committer Author { get; private set; }

        public Committer Committer { get; private set; }

        public GitReference Tree { get; private set; }

        public IReadOnlyList<GitReference> Parents { get; private set; }

        public int CommentCount { get; private set; }

        public Verification Verification { get; private set; }
    }
}
