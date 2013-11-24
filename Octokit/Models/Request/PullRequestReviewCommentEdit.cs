
namespace Octokit
{
    public class PullRequestReviewCommentEdit : RequestParameters
    {
        private readonly string _body;

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get { return _body; }}

        /// <summary>
        /// Creates an edit to a comment
        /// </summary>
        /// <param name="body">The text of the comment</param>
        public PullRequestReviewCommentEdit(string body)
        {
            Ensure.ArgumentNotNullOrEmptyString(body, "body");

            _body = body;
        }
    }
}
