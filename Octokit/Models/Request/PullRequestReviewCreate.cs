using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Used to create a pull request review
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewCreate : RequestParameters
    {
        public PullRequestReviewCreate()
        {
            Comments = new List<DraftPullRequestReviewComment>();
        }

        /// <summary>
        /// The Commit ID which the review is being created for. Default is the latest.
        /// </summary>
        public string CommitId { get; set; }

        /// <summary>
        /// The body of the review message
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The review event - Approve, Request Changes, Comment or leave blank (null) for Pending.
        /// </summary>
        public PullRequestReviewEvent? Event { get; set; }

        /// <summary>
        /// List of comments to include with this review
        /// </summary>
        public List<DraftPullRequestReviewComment> Comments { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "CommitId: {0}, Body: {1} ", CommitId, Body);
            }
        }
    }
}
