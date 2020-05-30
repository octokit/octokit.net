namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Checks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/">Checks API documentation</a> for more information.
    /// </remarks>
    public class ChecksClient : IChecksClient
    {
        /// <summary>
        /// Initializes a new GitHub Checks API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ChecksClient(IApiConnection apiConnection)
        {
            Run = new CheckRunsClient(apiConnection);
            Suite = new CheckSuitesClient(apiConnection);
        }

        /// <summary>
        /// A client for GitHub's Check Runs API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
        /// </remarks>
        public ICheckRunsClient Run { get; private set; }

        /// <summary>
        /// A client for GitHub's Check Suites API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/">Check Suites API documentation</a> for more information.
        /// </remarks>
        public ICheckSuitesClient Suite { get; private set; }
    }
}