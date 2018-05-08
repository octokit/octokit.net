namespace Octokit.Reactive
{
    internal class ObservableChecksClient : IObservableChecksClient
    {
        public ObservableChecksClient(IGitHubClient gitHubClient)
        {
        }

        public IObservableCheckRunsClient Runs => throw new System.NotImplementedException();

        public IObservableCheckSuitesClient Suites => throw new System.NotImplementedException();
    }
}