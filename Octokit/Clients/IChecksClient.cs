namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Checks API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/">Checks API documentation</a> for more information.
    /// </remarks>
    public interface IChecksClient
    {
        /// <summary>
        /// A client for GitHub's Check Runs API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
        /// </remarks>
        ICheckRunsClient Run { get; }

        /// <summary>
        /// A client for GitHub's Check Suites API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/">Check Suites API documentation</a> for more information.
        /// </remarks>
        ICheckSuitesClient Suite { get; }
    }
}
