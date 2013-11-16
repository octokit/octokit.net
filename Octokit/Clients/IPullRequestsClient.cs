
namespace Octokit
{
    public interface IPullRequestsClient
    {
        /// <summary>
        /// Client for managing comments.
        /// </summary>
        IPullRequestReviewCommentsClient Comment { get; }
    }
}
