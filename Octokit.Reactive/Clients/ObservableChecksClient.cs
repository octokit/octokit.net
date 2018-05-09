namespace Octokit.Reactive
{
    public class ObservableChecksClient : IObservableChecksClient
    {
        public ObservableChecksClient(IGitHubClient gitHubClient)
        {
            Runs = new ObservableCheckRunsClient(gitHubClient);
            Suites = new ObservableCheckSuitesClient(gitHubClient);
        }

        public IObservableCheckRunsClient Runs { get; private set; }

        public IObservableCheckSuitesClient Suites { get; private set; }
    }
}