namespace Octokit.Reactive
{
    public interface IObservableReactionsClient
    {
        IObservableCommitCommentReactionsClient CommitComment { get; }

        IObservableIssueReactionsClient Issue { get; }

        IObservableIssueCommentReactionsClient IssueComment { get; }

        IObservablePullRequestReviewCommentReactionsClient PullRequestReviewComment { get; }
    }
}
