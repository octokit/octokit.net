namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Dependency Graph API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/dependency-graph">Git Dependency Graph API documentation</a> for more information.
    /// </remarks>
    public interface IObservableDependencyGraphClient
    {
        /// <summary>
        /// Client for getting a dependency comparison between two commits.
        /// </summary>
        IObservableDependencyReviewClient DependencyReview { get; }

        /// <summary>
        /// Client for submitting dependency snapshots.
        /// </summary>
        IObservableDependencySubmissionClient DependencySubmission { get; }
    }
}

