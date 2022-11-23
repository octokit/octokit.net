using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Workflow runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-jobs/">Actions Workflow runs API documentation</a> for more information.
    /// </remarks>
    public class ObservableActionsWorkflowRunsClient : IObservableActionsWorkflowRunsClient
    {
        readonly IActionsWorkflowRunsClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Instantiate a new GitHub Actions Workflows runs API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsWorkflowRunsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Workflows.Runs;
            _connection = client.Connection;
        }

        /// <summary>
        /// Lists all workflow runs for a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        public IObservable<WorkflowRunsResponse> List(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.List(owner, name).ToObservable();
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
        public IObservable<WorkflowRunsResponse> List(string owner, string name, WorkflowRunsRequest workflowRunsRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));

            return _client.List(owner, name, workflowRunsRequest).ToObservable();
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
        public IObservable<WorkflowRunsResponse> List(string owner, string name, WorkflowRunsRequest workflowRunsRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.List(owner, name, workflowRunsRequest, options).ToObservable();
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
        public IObservable<WorkflowRun> Get(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, runId).ToObservable();
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
        public IObservable<Unit> Delete(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, runId).ToObservable();
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
        public IObservable<EnvironmentApprovals> GetReviewHistory(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _connection.GetAndFlattenAllPages<EnvironmentApprovals>(ApiUrls.ActionsWorkflowRunApprovals(owner, name, runId));
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
        public IObservable<Unit> Approve(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Approve(owner, name, runId).ToObservable();
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
        public IObservable<WorkflowRun> GetAttempt(string owner, string name, long runId, long attemptNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetAttempt(owner, name, runId, attemptNumber).ToObservable();
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
        public IObservable<byte[]> GetAttemptLogs(string owner, string name, long runId, long attemptNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetAttemptLogs(owner, name, runId, attemptNumber).ToObservable();
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
        public IObservable<Unit> Cancel(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Cancel(owner, name, runId).ToObservable();
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
        public IObservable<byte[]> GetLogs(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetLogs(owner, name, runId).ToObservable();
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
        public IObservable<Unit> DeleteLogs(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.DeleteLogs(owner, name, runId).ToObservable();
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
        public IObservable<Deployment> ReviewPendingDeployments(string owner, string name, long runId, PendingDeploymentReview review)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(review, nameof(review));

            return _client.ReviewPendingDeployments(owner, name, runId, review).ToObservable();
        }

        /// <summary>
        /// Re-runs a specific workflow run in a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#re-run-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        public IObservable<Unit> Rerun(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Rerun(owner, name, runId).ToObservable();
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
        public IObservable<Unit> RerunFailedJobs(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.RerunFailedJobs(owner, name, runId).ToObservable();
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
        public IObservable<WorkflowRunUsage> GetUsage(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetUsage(owner, name, runId).ToObservable();
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
        public IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.ListByWorkflow(owner, name, workflowId).ToObservable();
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
        public IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId, WorkflowRunsRequest workflowRunsRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));

            return _client.ListByWorkflow(owner, name, workflowId, workflowRunsRequest).ToObservable();
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
        public IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId, WorkflowRunsRequest workflowRunsRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListByWorkflow(owner, name, workflowId, workflowRunsRequest, options).ToObservable();
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
        public IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return _client.ListByWorkflow(owner, name, workflowFileName).ToObservable();
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
        public IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName, WorkflowRunsRequest workflowRunsRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));

            return _client.ListByWorkflow(owner, name, workflowFileName, workflowRunsRequest).ToObservable();
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
        public IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName, WorkflowRunsRequest workflowRunsRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));
            Ensure.ArgumentNotNull(workflowRunsRequest, nameof(workflowRunsRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListByWorkflow(owner, name, workflowFileName, workflowRunsRequest, options).ToObservable();
        }
    }
}
