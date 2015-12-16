using System;
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

        public PullRequestCommit(Committer author, Uri commentsUrl, Commit commit, Committer committer, Uri htmlUrl, IEnumerable<GitReference> parents, string sha, Uri url)
        {
            Ensure.ArgumentNotNull(parents, "parents");

            Author = author;
            CommentsUrl = commentsUrl;
            Commit = commit;
            Committer = committer;
            HtmlUrl = htmlUrl;
            Parents = new ReadOnlyCollection<GitReference>(parents.ToList());
            Sha = sha;
            Url = url;
        }

        public Committer Author { get; protected set; }

        public Uri CommentsUrl { get; protected set; }

        public Commit Commit { get; protected set; }

        public Committer Committer { get; protected set; }

        public Uri HtmlUrl { get; protected set; }

        public IReadOnlyList<GitReference> Parents { get; protected set; }

        public string Sha { get; protected set; }

        public Uri Url { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                var name = (Commit != null && Commit.Author != null) ? Commit.Author.Name : "";
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0} Author: {1}", Sha, name);
            }
        }
    }
}
