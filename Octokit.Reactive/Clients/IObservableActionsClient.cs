namespace Octokit.Reactive
{
    public interface IObservableActionsClient
    {
        IObservableWorkflowRunsClient Run { get; }
    }
}
