using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Check Runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
    /// </remarks>
    public interface ICheckRunsClient
    {
        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        Task<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun);

        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        Task<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun);

    }
}
