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

        public IObservableCommitCommentReactionsClient CommitComment { get; private set; }

        public IObservableIssueReactionsClient Issue { get; private set; }

        public IObservableIssueCommentReactionsClient IssueComment { get; private set; }

        public IObservablePullRequestReviewCommentReactionsClient PullRequestReviewComment { get; private set; }
    }
}
