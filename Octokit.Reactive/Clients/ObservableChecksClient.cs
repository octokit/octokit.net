namespace Octokit.Reactive
{
    public class ObservableChecksClient : IObservableChecksClient
    {
        public ObservableChecksClient(IGitHubClient gitHubClient)
        {
            Suite = new ObservableCheckSuitesClient(gitHubClient);
        }

        public IObservableCheckSuitesClient Suite { get; private set; }
    }
}