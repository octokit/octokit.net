namespace Octokit.Reactive
{
    public class ObservableChecksClient : IObservableChecksClient
    {
        public ObservableChecksClient(IGitHubClient gitHubClient)
        {
            Run = new ObservableCheckRunsClient(gitHubClient);
            Suite = new ObservableCheckSuitesClient(gitHubClient);
        }

        public IObservableCheckRunsClient Run { get; private set; }

        public IObservableCheckSuitesClient Suite { get; private set; }
    }
}