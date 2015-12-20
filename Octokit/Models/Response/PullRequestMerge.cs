using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents the response from an attempt to merge a pull request.
    /// </summary>
    /// <remarks>
    /// Note the request to merge is represented by <see cref="MergePullRequest"/>
    /// API: https://developer.github.com/v3/pulls/#merge-a-pull-request-merge-button
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestMerge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PullRequestMerge"/> class.
        /// </summary>
        public PullRequestMerge() { }

        public PullRequestMerge(string sha, bool merged, string message)
        {
            Sha = sha;
            Merged = merged;
            Message = message;
        }

        /// <summary>
        /// The sha reference of the commit.
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// True if merged successfully, otherwise false.
        /// </summary>
        public bool Merged { get; protected set; }

        /// <summary>
        /// The message that will be used for the merge commit.
        /// </summary>
        public string Message { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0} Message: {1}", Sha, Message);
            }
        }
    }
}
