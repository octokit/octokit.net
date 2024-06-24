namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Dependency Graph API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/dependency-graph">Git Dependency Graph API documentation</a> for more information.
    /// </remarks>
    public class ObservableDependencyGraphClient : IObservableDependencyGraphClient
    {
        public ObservableDependencyGraphClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            //DependencyReview = new ObservableDependencyReviewClient(client);
            DependencySubmission = new ObservableDependencySubmissionClient(client);
        }

        /// <summary>
        /// Client for getting a dependency comparison between two commits.
        /// </summary>
        public IObservableDependencyReviewClient DependencyReview { get; private set; }

        /// <summary>
        /// Client for submitting dependency snapshots.
        /// </summary>
        public IObservableDependencySubmissionClient DependencySubmission { get; private set; }
    }
}
