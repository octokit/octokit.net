namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Dependency Graph API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/dependency-graph">Git Dependency Graph API documentation</a> for more information.
    /// </remarks>
    public interface IDependencyGraphClient
    {
        /// <summary>
        /// Client for getting a dependency comparison between two commits.
        /// </summary>
        IDependencyReviewClient DependencyReview { get; }

        /// <summary>
        /// Client for submitting dependency snapshots.
        /// </summary>
        IDependencySubmissionClient DependencySubmission { get; }
    }
}
