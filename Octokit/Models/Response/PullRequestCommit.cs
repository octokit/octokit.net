using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestCommit
    {
        public SignatureResponse Author { get; protected set; }

        public Uri CommentsUrl { get; protected set; }

        public Commit Commit { get; protected set; }

        public SignatureResponse Committer { get; protected set; }

        public Uri HtmlUrl { get; protected set; }

        public IEnumerable<GitReference> Parents { get; protected set; }

        public string Sha { get; protected set; }

        public Uri Url { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                var name = (Commit != null && Commit.Author != null) ? Commit.Author.Name : "";
                return String.Format(CultureInfo.InvariantCulture, "Sha: {0} Author: {1}", Sha, name);
            }
        }
    }
}
