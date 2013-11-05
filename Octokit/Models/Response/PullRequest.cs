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

        /// <summary>
        /// The URL for the pull request page.
        /// </summary>
        public Uri HtmlUrl { get; set; }

        /// <summary>
        /// The URL for the pull request's diff (.diff) file.
        /// </summary>
        public Uri DiffUrl { get; set; }

        /// <summary>
        /// The URL for the pull request's patch (.patch) file.
        /// </summary>
        public Uri PatchUrl { get; set; }
    }
}