using Octokit.Internal;

namespace Octokit
{
    public class PullRequestReviewCommentReplyCreate : RequestParameters
    {
        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The comment Id to reply to.
        /// </summary>
        [Parameter(Key = "in_reply_to")]
        public int InReplyTo { get; set; }
    }
}
