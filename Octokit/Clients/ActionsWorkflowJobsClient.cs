using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Workflows jobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-jobs/">Actions Workflows jobs API documentation</a> for more information.
    /// </remarks>
    public class ActionsWorkflowJobsClient : ApiClient, IActionsWorkflowJobsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Workflows jobs API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsWorkflowJobsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Re-runs a specific workflow job in a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#re-run-a-job-from-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="jobId">The Id of the workflow job.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/jobs/{job_id}/rerun")]
        public Task Rerun(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post(ApiUrls.ActionsRerunWorkflowJob(owner, name, jobId));
        }
    }
}
