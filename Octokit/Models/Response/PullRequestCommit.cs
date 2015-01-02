using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestCommit
    {
        public SignatureResponse Author { get; set; }
        public Uri CommentsUrl { get; set; }
        public Commit Commit { get; set; }
        public SignatureResponse Committer { get; set; }
        public Uri HtmlUrl { get; set; }
        public IEnumerable<GitReference> Parents { get; set; }
        public string Sha { get; set; }
        public Uri Url { get; set; }

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