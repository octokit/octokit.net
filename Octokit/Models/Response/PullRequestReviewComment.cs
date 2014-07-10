using System;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewComment
    {
        /// <summary>
        /// URL of the comment via the API.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The comment Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The diff hunk the comment is about.
        /// </summary>
        public string DiffHunk { get; set; }

        /// <summary>
        /// The relative path of the file the comment is about.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The line index in the diff.
        /// </summary>
        public int? Position { get; set; }

        /// <summary>
        /// The comment original position.
        /// </summary>
        public int? OriginalPosition { get; set; }

        /// <summary>
        /// The commit Id the comment is associated with.
        /// </summary>
        public string CommitId { get; set; }

        /// <summary>
        /// The original commit Id the comment is associated with.
        /// </summary>
        public string OriginalCommitId { get; set; }

        /// <summary>
        /// The user that created the comment.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The date the comment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date the comment was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The URL for this comment on Github.com
        /// </summary>
        public Uri HtmlUrl { get; set; }

        /// <summary>
        /// The URL for the pull request via the API.
        /// </summary>
        public Uri PullRequestUrl { get; set; }
    }

    public enum PullRequestReviewCommentSort
    {
        /// <summary>
        /// Sort by create date (default)
        /// </summary>
        Created,

        /// <summary>
        /// Sort by the date of the last update
        /// </summary>
        Updated,
    }
}
