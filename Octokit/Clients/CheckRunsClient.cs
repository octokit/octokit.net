using System;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Check Runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
    /// </remarks>
    public class CheckRunsClient : ApiClient, ICheckRunsClient
    {
        /// <summary>
        /// Initializes a new GitHub Check Runs API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public CheckRunsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        public Task<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return ApiConnection.Post<CheckRun>(ApiUrls.CheckRuns(owner, name), newCheckRun, AcceptHeaders.ChecksApiPreview);
        }

        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        public Task<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return ApiConnection.Post<CheckRun>(ApiUrls.CheckRuns(repositoryId), newCheckRun, AcceptHeaders.ChecksApiPreview);
        }
    }
}
