using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Used to create pull request reviews
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewCreate : RequestParameters
    {
        public PullRequestReviewCreate()
        {
            Comments = new List<PullRequestReviewCommentCreate>();
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
        /// The review event (APPROVE, REQUEST CHANGES, or COMMENT). Default is PENDING
        /// </summary>
        public PullRequestReviewRequestEvents Event { get; set; }

        /// <summary>
        /// The comment drafts to send with this review
        /// </summary>
        public List<PullRequestReviewCommentCreate> Comments { get; set; }
        
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "CommitId: {0}, Body: {1} ", CommitId, Body);
            }
        }
    }
}
