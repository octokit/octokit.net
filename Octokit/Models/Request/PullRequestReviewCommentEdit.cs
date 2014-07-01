using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewCommentEdit : RequestParameters
    {
        /// <summary>
        /// Creates an edit to a comment
        /// </summary>
        /// <param name="body">The text of the comment</param>
        public PullRequestReviewCommentEdit(string body)
        {
            Ensure.ArgumentNotNullOrEmptyString(body, "body");

            Body = body;
        }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; private set; }
    }
}
