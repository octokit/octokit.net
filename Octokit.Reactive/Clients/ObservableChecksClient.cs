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
        public ObservableChecksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Suite = new ObservableCheckSuitesClient(client);
        }

        /// <summary>
        /// A client for GitHub's Check Suites API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/">Check Suites API documentation</a> for more information.
        /// </remarks>
        public IObservableCheckSuitesClient Suite { get; private set; }
    }
}