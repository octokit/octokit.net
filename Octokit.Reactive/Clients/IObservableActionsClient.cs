namespace Octokit.Reactive
{
    /// <summary>
    /// Used to maintain api structure therefore contains no methods
    /// </summary>
    public interface IObservableActionsClient
    {
        IObservableWorkflowsClient Blob { get; }
    }
}
