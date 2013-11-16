
namespace Octokit
{
    public class PullRequestsClient : ApiClient, IPullRequestsClient
    {
        public PullRequestsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Comment = new PullRequestReviewCommentsClient(apiConnection);
        }

        /// <summary>
        /// Client for managing comments.
        /// </summary>
        public IPullRequestReviewCommentsClient Comment { get; private set; }
    }
}
