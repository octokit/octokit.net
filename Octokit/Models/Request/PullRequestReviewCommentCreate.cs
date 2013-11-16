using Octokit.Internal;

namespace Octokit
{
    public class PullRequestReviewCommentCreate : RequestParameters
    {
        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The SHA of the commit to comment on.
        /// </summary>
        [Parameter(Key = "commit_id")]
        public string CommitId { get; set; }

        /// <summary>
        /// The relative path of the file to comment on.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The line index in the diff to comment on.
        /// </summary>
        public int Position { get; set; }
    }
}
