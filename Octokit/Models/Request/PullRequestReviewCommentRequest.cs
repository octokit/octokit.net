using System;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewCommentRequest : RequestParameters
    {
        public PullRequestReviewCommentRequest()
        {
            // Default arguments
            Sort = PullRequestReviewCommentSort.Created;
            Direction = SortDirection.Ascending;
            Since = null;
        }

        /// <summary>
        /// Can be either created or updated. Default: created.
        /// </summary>
        public PullRequestReviewCommentSort Sort { get; set; }

        /// <summary>
        /// Can be either asc or desc. Default: asc.
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        /// Only comments updated at or after this time are returned. This is a timestamp in ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ.
        /// </summary>
        public DateTimeOffset? Since { get; set; }
    }
}
