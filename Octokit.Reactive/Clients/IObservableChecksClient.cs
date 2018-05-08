namespace Octokit.Reactive
{
    public interface IObservableChecksClient
    {
        IObservableCheckRunsClient Runs { get; }
        IObservableCheckSuitesClient Suites { get; }
    }
}
