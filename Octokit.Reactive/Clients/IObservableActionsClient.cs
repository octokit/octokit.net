namespace Octokit.Reactive
{
    public interface IObservableActionsClient
    {
        /// <summary>
        /// Client for managing workflow runs
        /// </summary>
        IObservableWorkflowRunsClient Run { get; }
    }
}
