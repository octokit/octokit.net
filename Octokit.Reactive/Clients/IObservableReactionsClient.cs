namespace Octokit.Reactive
{
    public interface IObservableReactionsClient
    {
        IObservableCommitCommentReactionClient CommitComments { get; }
    }
}
