
namespace Octokit
{
    public class PullRequestReviewCommentEdit : RequestParameters
    {
        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; set; }
    }
}
