using System;

namespace Octokit
{
    public class PullRequestMerge
    {
        /// <summary>
        /// The sha reference of the commit.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// True if merged successfully, otherwise false.
        /// </summary>
        public bool Merged { get; set; }

        /// <summary>
        /// The message that will be used for the merge commit.
        /// </summary>
        public string Message { get; set; }
    }
}
