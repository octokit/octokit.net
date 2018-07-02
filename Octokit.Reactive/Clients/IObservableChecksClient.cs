namespace Octokit.Reactive
{
    public interface IObservableChecksClient
    {
        IObservableCheckSuitesClient Suite { get; }
    }
}
