using System;

namespace Octokit
{
    public class PullRequest
    {
        /// <summary>
        /// The URL for this pull request.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The pull request number.
        /// </summary>
        public int Number { get; set; }

        public Uri HtmlUrl { get; set; }
        public Uri DiffUrl { get; set; }
        public Uri PatchUrl { get; set; }
    }
}