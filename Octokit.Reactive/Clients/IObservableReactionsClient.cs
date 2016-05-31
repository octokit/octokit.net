namespace Octokit.Reactive
{
    public interface IObservableReactionsClient
    {
        IObservableCommitCommentsReactionsClient CommitComment { get; }

        IObservableIssuesReactionsClient Issue { get; }
    }
}
