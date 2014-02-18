using System;
using System.Collections.Generic;

namespace Octokit
{
    public class PullRequestCommit
    {
        public Signature Author { get; set; }
        public Uri CommentsUrl { get; set; }
        public Commit Commit { get; set; }
        public Signature Committer { get; set; }
        public Uri HtmlUrl { get; set; }
        public IEnumerable<GitReference> Parents { get; set; }
        public string Sha { get; set; }
        public Uri Url { get; set; }
    }
}