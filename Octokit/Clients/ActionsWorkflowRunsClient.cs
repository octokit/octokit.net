using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Workflows runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-runs/">Actions Workflows runs API documentation</a> for more information.
    /// </remarks>
    public class ActionsWorkflowRunsClient : ApiClient, IActionsWorkflowRunsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Workflows runs API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsWorkflowRunsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Lists all workflow runs for a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs")]
        public Task<WorkflowRunsResponse> List(string owner, string name)
        {
            return List(owner, name, new WorkflowRunsRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Lists all workflow runs for a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowRunsRequest">Details to filter the request, such as by check suite Id.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs")]
        public Task<WorkflowRunsResponse> List(string owner, string name, WorkflowRunsRequest workflowRunsRequest)
        {
            return List(owner, name, workflowRunsRequest, ApiOptions.None);
        }

        /// <summary>
        /// Lists all workflow runs for a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowRunsRequest">Details to filter the request, such as by check suite Id.</param>
        /// <param name="options">Options to change the API response.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs")]
        public async Task<WorkflowRunsResponse> List(string owner, string name, WorkflowRunsRequest workflowRunsRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<WorkflowRunsResponse>(ApiUrls.ActionsWorkflowRuns(owner, name), workflowRunsRequest.ToParametersDictionary(), options).ConfigureAwait(false);

            return new WorkflowRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.WorkflowRuns).ToList());
        }

        /// <summary>
        /// Gets a specific workflow run in a repository. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#get-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}")]
        public Task<WorkflowRun> Get(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<WorkflowRun>(ApiUrls.ActionsWorkflowRun(owner, name, runId), null);
        }

        /// <summary>
        /// Deletes a specific workflow run. Anyone with write access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#delete-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/actions/runs/{run_id}")]
        public Task Delete(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.ActionsWorkflowRun(owner, name, runId));
        }

        /// <summary>
        /// Get the review history for a workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#get-the-review-history-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/approvals")]
        public Task<IReadOnlyList<EnvironmentApprovals>> GetReviewHistory(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.GetAll<EnvironmentApprovals>(ApiUrls.ActionsWorkflowRunApprovals(owner, name, runId));
        }

        /// <summary>
        /// Approves a workflow run for a pull request from a public fork of a first time contributor.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#approve-a-workflow-run-for-a-fork-pull-request
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/runs/{run_id}/approve")]
        public Task Approve(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post(ApiUrls.ActionsApproveWorkflowRun(owner, name, runId));
        }

        /// <summary>
        /// Gets a specific workflow run attempt. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#get-a-workflow-run-attempt
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="attemptNumber">The attempt number of the workflow run.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/attempts/{attempt_number}")]
        public Task<WorkflowRun> GetAttempt(string owner, string name, long runId, long attemptNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<WorkflowRun>(ApiUrls.ActionsWorkflowRunAttempt(owner, name, runId, attemptNumber), null);
        }

        /// <summary>
        /// Gets a byte array containing an archive of log files for a specific workflow run attempt.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#download-workflow-run-attempt-logs
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="attemptNumber">The attempt number of the workflow run.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/attempts/{attempt_number}")]
        public async Task<byte[]> GetAttemptLogs(string owner, string name, long runId, long attemptNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var response = await Connection.GetRaw(ApiUrls.ActionsGetWorkflowRunAttemptLogs(owner, name, runId, attemptNumber), null).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Cancels a workflow run using its Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#cancel-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/runs/{run_id}/cancel")]
        public Task Cancel(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post(ApiUrls.ActionsCancelWorkflowRun(owner, name, runId));
        }

        /// <summary>
        /// Gets a byte array containing an archive of log files for a workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#download-workflow-run-logs
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/logs")]
        public async Task<byte[]> GetLogs(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var response = await Connection.GetRaw(ApiUrls.ActionsGetWorkflowRunLogs(owner, name, runId), null).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Deletes all logs for a workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#delete-workflow-run-logs
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/actions/runs/{run_id}/logs")]
        public Task DeleteLogs(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.ActionsGetWorkflowRunLogs(owner, name, runId));
        }

        /// <summary>
        /// Approve or reject pending deployments that are waiting on approval by a required reviewer.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#review-pending-deployments-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        /// <param name="review">The review for the pending deployment.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/runs/{run_id}/pending_deployments")]
        public Task<Deployment> ReviewPendingDeployments(string owner, string name, long runId, PendingDeploymentReview review)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(review, nameof(review));

            return ApiConnection.Post<Deployment>(ApiUrls.ActionsWorkflowRunPendingDeployments(owner, name, runId), review);
        }

        /// <summary>
        /// Re-runs your workflow run using its Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#re-run-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/runs/{run_id}/rerun")]
        public Task Rerun(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post(ApiUrls.ActionsRerunWorkflowRun(owner, name, runId));
        }

        /// <summary>
        /// Re-run all of the failed jobs and their dependent jobs in a workflow run using the Id of the workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#re-run-failed-jobs-from-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/runs/{run_id}/rerun-failed-jobs")]
        public Task RerunFailedJobs(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post(ApiUrls.ActionsRerunWorkflowRunFailedJobs(owner, name, runId));
        }

        /// <summary>
        /// Gets the number of billable minutes and total run time for a specific workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#get-workflow-run-usage
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/timing")]
        public Task<WorkflowRunUsage> GetUsage(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<WorkflowRunUsage>(ApiUrls.ActionsGetWorkflowRunUsage(owner, name, runId), null);
        }

        /// <summary>
        /// List all workflow runs for a workflow.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/runs")]
        public Task<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId)
        {
            return ListByWorkflow(owner, name, workflowId, new WorkflowRunsRequest(), ApiOptions.None);
        }

        /// <summary>
        /// List all workflow runs for a workflow.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <param name="workflowRunsRequest">Details to filter the request, such as by check suite Id.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/runs")]
        public Task<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId, WorkflowRunsRequest workflowRunsRequest)
        {
            return ListByWorkflow(owner, name, workflowId, workflowRunsRequest, ApiOptions.None);
        }

        /// <summary>
        /// List all workflow runs for a workflow.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <param name="workflowRunsRequest">Details to filter the request, such as by check suite Id.</param>
        /// <param name="options">Options to change the API response.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/runs")]
        public async Task<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId, WorkflowRunsRequest workflowRunsRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<WorkflowRunsResponse>(ApiUrls.ActionsListWorkflowRuns(owner, name, workflowId), workflowRunsRequest.ToParametersDictionary(), options).ConfigureAwait(false);

            return new WorkflowRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.WorkflowRuns).ToList());
        }

        /// <summary>
        /// List all workflow runs for a workflow.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The Id of the workflow.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/runs")]
        public Task<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName)
        {
            return ListByWorkflow(owner, name, workflowFileName, new WorkflowRunsRequest(), ApiOptions.None);
        }

        /// <summary>
        /// List all workflow runs for a workflow.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <param name="workflowRunsRequest">Details to filter the request, such as by check suite Id.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/runs")]
        public Task<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName, WorkflowRunsRequest workflowRunsRequest)
        {
            return ListByWorkflow(owner, name, workflowFileName, workflowRunsRequest, ApiOptions.None);
        }

        /// <summary>
        /// List all workflow runs for a workflow.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <param name="workflowRunsRequest">Details to filter the request, such as by check suite Id.</param>
        /// <param name="options">Options to change the API response.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/runs")]
        public async Task<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName, WorkflowRunsRequest workflowRunsRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<WorkflowRunsResponse>(ApiUrls.ActionsListWorkflowRuns(owner, name, workflowFileName), workflowRunsRequest.ToParametersDictionary(), options).ConfigureAwait(false);

            return new WorkflowRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.WorkflowRuns).ToList());
        }
    }
}
