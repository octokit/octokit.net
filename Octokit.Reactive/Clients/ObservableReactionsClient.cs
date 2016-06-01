namespace Octokit.Reactive
{
    public class ObservableReactionsClient : IObservableReactionsClient
    {
        public ObservableReactionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            CommitComment = new ObservableCommitCommentReactionsClient(client);
            Issue = new ObservableIssueReactionsClient(client);
            IssueComment = new ObservableIssueCommentReactionsClient(client);
            PullRequestReviewComment = new ObservablePullRequestReviewCommentReactionsClient(client);
        }

        /// <summary>
        /// Access GitHub's Reactions API for Commit Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public IObservableCommitCommentReactionsClient CommitComment { get; private set; }

        /// <summary>
        /// Access GitHub's Reactions API for Issues.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public IObservableIssueReactionsClient Issue { get; private set; }

        /// <summary>
        /// Access GitHub's Reactions API for Issue Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public IObservableIssueCommentReactionsClient IssueComment { get; private set; }

        /// <summary>
        /// Access GitHub's Reactions API for Pull Request Review Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public IObservablePullRequestReviewCommentReactionsClient PullRequestReviewComment { get; private set; }
    }
}
