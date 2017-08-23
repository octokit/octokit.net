using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Used to submit a pending pull request review
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewSubmit : RequestParameters
    {
        public PullRequestReviewSubmit()
        {
        }

        /// <summary>
        /// The body of the review message
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The review event - Approve, Request Changes, Comment
        /// </summary>
        public PullRequestReviewEvent Event { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Event: {0} ", Event);
            }
        }
    }

    public enum PullRequestReviewEvent
    {
        /// <summary>
        /// The review is approved
        /// </summary>
        [Parameter(Value = "APPROVE")]
        Approve,

        /// <summary>
        /// The review requests changes that must be addressed before merging
        /// </summary>
        [Parameter(Value = "REQUEST_CHANGES")]
        RequestChanges,

        /// <summary>
        /// The review provides comment without explicit approval
        /// </summary>
        [Parameter(Value = "COMMENT")]
        Comment
    }
}
