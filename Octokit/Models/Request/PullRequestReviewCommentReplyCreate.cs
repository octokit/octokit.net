using Octokit.Internal;

namespace Octokit
{
    public class PullRequestReviewCommentReplyCreate : RequestParameters
    {
        private readonly string _body;
        private readonly int _inReplyTo;

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get { return _body; } }

        /// <summary>
        /// The comment Id to reply to.
        /// </summary>
        public int InReplyTo { get { return _inReplyTo; } }

        /// <summary>
        /// Creates a comment that is replying to another comment.
        /// </summary>
        /// <param name="body">The text of the comment</param>
        /// <param name="inReplyTo">The comment Id to reply to</param>
        public PullRequestReviewCommentReplyCreate(string body, int inReplyTo)
        {
            Ensure.ArgumentNotNullOrEmptyString(body, "body");

            _body = body;
            _inReplyTo = inReplyTo;
        }
    }
}
