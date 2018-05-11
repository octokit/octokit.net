namespace Octokit.Reactive
{
    public interface IObservableChecksClient
    {
        IObservableCheckRunsClient Run { get; }
        IObservableCheckSuitesClient Suite { get; }
    }
}
