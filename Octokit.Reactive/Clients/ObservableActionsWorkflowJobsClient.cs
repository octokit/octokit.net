using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Workflow jobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-jobs/">Actions Workflow jobs API documentation</a> for more information.
    /// </remarks>
    public class ObservableActionsWorkflowJobsClient : IObservableActionsWorkflowJobsClient
    {
        readonly IActionsWorkflowJobsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Workflows jobs API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsWorkflowJobsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Workflows.Jobs;
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
        public IObservable<Unit> Rerun(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Rerun(owner, name, jobId).ToObservable();
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
        public IObservable<WorkflowJob> Get(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, jobId).ToObservable();
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
        public IObservable<string> GetLogs(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetLogs(owner, name, jobId).ToObservable();
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
        public IObservable<WorkflowJobsResponse> List(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.List(owner, name, runId).ToObservable();
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
        public IObservable<WorkflowJobsResponse> List(string owner, string name, long runId, WorkflowRunJobsRequest workflowRunJobsRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunJobsRequest, nameof(workflowRunJobsRequest));

            return _client.List(owner, name, runId, workflowRunJobsRequest).ToObservable();
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
        public IObservable<WorkflowJobsResponse> List(string owner, string name, long runId, WorkflowRunJobsRequest workflowRunJobsRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunJobsRequest, nameof(workflowRunJobsRequest));

            return _client.List(owner, name, runId, workflowRunJobsRequest, options).ToObservable();
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
        public IObservable<WorkflowJobsResponse> List(string owner, string name, long runId, int attemptNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.List(owner, name, runId, attemptNumber).ToObservable();
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
        public IObservable<WorkflowJobsResponse> List(string owner, string name, long runId, int attemptNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.List(owner, name, runId, attemptNumber, options).ToObservable();
        }
    }
}
