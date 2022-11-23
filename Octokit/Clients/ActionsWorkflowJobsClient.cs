using System.Linq;
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

        /// <summary>
        /// Gets a specific job in a workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-jobs/#get-a-job-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="jobId">The unique identifier of the job.</param>
        [ManualRoute("GET", "\n/repos/{owner}/{repo}/actions/jobs/{job_id}")]
        public Task<WorkflowJob> Get(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<WorkflowJob>(ApiUrls.ActionsGetWorkflowJob(owner, name, jobId));
        }

        /// <summary>
        /// Gets the plain text log file for a workflow job.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-jobs/#download-job-logs-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="jobId">The Id of the workflow job.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/jobs/{job_id}/logs")]
        public async Task<string> GetLogs(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var response = await Connection.Get<string>(ApiUrls.ActionsGetWorkflowJobLogs(owner, name, jobId), null).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Lists jobs for a specific workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-jobs-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/jobs")]
        public Task<WorkflowJobsResponse> List(string owner, string name, long runId)
        {
            return List(owner, name, runId, new WorkflowRunJobsRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Lists jobs for a specific workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-jobs-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="workflowRunJobsRequest">Details to filter the request, such as by when completed.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/jobs")]
        public Task<WorkflowJobsResponse> List(string owner, string name, long runId, WorkflowRunJobsRequest workflowRunJobsRequest)
        {
            return List(owner, name, runId, workflowRunJobsRequest, ApiOptions.None);
        }

        /// <summary>
        /// Lists jobs for a specific workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-jobs-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="workflowRunJobsRequest">Details to filter the request, such as by when completed.</param>
        /// <param name="options">Options to change the API response.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/jobs")]
        public async Task<WorkflowJobsResponse> List(string owner, string name, long runId, WorkflowRunJobsRequest workflowRunJobsRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunJobsRequest, nameof(workflowRunJobsRequest));

            var results = await ApiConnection.GetAll<WorkflowJobsResponse>(ApiUrls.ActionsListWorkflowJobs(owner, name, runId), workflowRunJobsRequest.ToParametersDictionary(), options).ConfigureAwait(false);

            return new WorkflowJobsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Jobs).ToList());
        }

        /// <summary>
        /// Lists jobs for a specific workflow run attempt.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-jobs-for-a-workflow-run-attempt
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="attemptNumber">The attempt number of the workflow run.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/attempts/{attempt_number}/jobs")]
        public Task<WorkflowJobsResponse> List(string owner, string name, long runId, int attemptNumber)
        {
            return List(owner, name, runId, attemptNumber, ApiOptions.None);
        }

        /// <summary>
        /// Lists jobs for a specific workflow run attempt.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-jobs-for-a-workflow-run-attempt
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="attemptNumber">The attempt number of the workflow run.</param>
        /// <param name="options">Options to change the API response.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/attempts/{attempt_number}/jobs")]
        public async Task<WorkflowJobsResponse> List(string owner, string name, long runId, int attemptNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var results = await ApiConnection.GetAll<WorkflowJobsResponse>(ApiUrls.ActionsListWorkflowJobs(owner, name, runId, attemptNumber), null, options).ConfigureAwait(false);

            return new WorkflowJobsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Jobs).ToList());
        }
    }
}
