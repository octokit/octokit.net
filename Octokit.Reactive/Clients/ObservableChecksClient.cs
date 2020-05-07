namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Checks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/">Checks API documentation</a> for more information.
    /// </remarks>
    public class ObservableChecksClient : IObservableChecksClient
    {
        /// <summary>
        /// Initializes a new GitHub Checks API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableChecksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Run = new ObservableCheckRunsClient(client);
            Suite = new ObservableCheckSuitesClient(client);
        }

        /// <summary>
        /// A client for GitHub's Check Runs API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
        /// </remarks>
        public IObservableCheckRunsClient Run { get; private set; }

        /// <summary>
        /// A client for GitHub's Check Suites API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/">Check Suites API documentation</a> for more information.
        /// </remarks>
        public IObservableCheckSuitesClient Suite { get; private set; }
    }
}