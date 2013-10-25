using System;

namespace Octokit
{
    public class PullRequest
    {
        public Uri HtmlUrl { get; set; }
        public Uri DiffUrl { get; set; }
        public Uri PatchUrl { get; set; }
    }
}