namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Dependency Graph API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/dependency-graph">Git Dependency Graph API documentation</a> for more information.
    /// </remarks>
    public class DependencyGraphClient : IDependencyGraphClient
    {
        /// <summary>
        /// Instantiates a new GitHub Dependency Graph API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public DependencyGraphClient(IApiConnection apiConnection)
        {
            DependencyReview = new DependencyReviewClient(apiConnection);
            DependencySubmission = new DependencySubmissionClient(apiConnection);
        }

        /// <summary>
        /// Client for getting a dependency comparison between two commits.
        /// </summary>
        public IDependencyReviewClient DependencyReview { get; private set; }

        /// <summary>
        /// Client for submitting dependency snapshots.
        /// </summary>
        public IDependencySubmissionClient DependencySubmission { get; private set; }
    }
}