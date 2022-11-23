using System;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Workflows runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-runs/">Actions Workflows runs API documentation</a> for more information.
    /// </remarks>
    public interface IObservableActionsWorkflowRunsClient
    {
        /// <summary>
        /// Lists all workflow runs for a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        IObservable<WorkflowRunsResponse> List(string owner, string name);

        /// <summary>
        /// Lists all workflow runs for a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowRunsRequest">Details to filter the request, such as by check suite Id.</param>
        IObservable<WorkflowRunsResponse> List(string owner, string name, WorkflowRunsRequest workflowRunsRequest);

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
        IObservable<WorkflowRunsResponse> List(string owner, string name, WorkflowRunsRequest workflowRunsRequest, ApiOptions options);

        /// <summary>
        /// Gets a specific workflow run in a repository. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#get-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<WorkflowRun> Get(string owner, string name, long runId);

        /// <summary>
        /// Deletes a specific workflow run. Anyone with write access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#delete-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<Unit> Delete(string owner, string name, long runId);

        /// <summary>
        /// Get the review history for a workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#get-the-review-history-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<EnvironmentApprovals> GetReviewHistory(string owner, string name, long runId);

        /// <summary>
        /// Approves a workflow run for a pull request from a public fork of a first time contributor.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#approve-a-workflow-run-for-a-fork-pull-request
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<Unit> Approve(string owner, string name, long runId);

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
        IObservable<WorkflowRun> GetAttempt(string owner, string name, long runId, long attemptNumber);

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
        IObservable<byte[]> GetAttemptLogs(string owner, string name, long runId, long attemptNumber);

        /// <summary>
        /// Cancels a workflow run using its Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#cancel-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<Unit> Cancel(string owner, string name, long runId);

        /// <summary>
        /// Gets a byte array containing an archive of log files for a workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#download-workflow-run-logs
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<byte[]> GetLogs(string owner, string name, long runId);

        /// <summary>
        /// Deletes all logs for a workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#delete-workflow-run-logs
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<Unit> DeleteLogs(string owner, string name, long runId);

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
        IObservable<Deployment> ReviewPendingDeployments(string owner, string name, long runId, PendingDeploymentReview review);

        /// <summary>
        /// Re-runs a specific workflow run in a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#re-run-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<Unit> Rerun(string owner, string name, long runId);

        /// <summary>
        /// Re-run all of the failed jobs and their dependent jobs in a workflow run using the Id of the workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#re-run-failed-jobs-from-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<Unit> RerunFailedJobs(string owner, string name, long runId);

        /// <summary>
        /// Gets the number of billable minutes and total run time for a specific workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#get-workflow-run-usage
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<WorkflowRunUsage> GetUsage(string owner, string name, long runId);

        /// <summary>
        /// List all workflow runs for a workflow.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId);

        /// <summary>
        /// List all workflow runs for a workflow.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-workflow-runs-for-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName);

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
        IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId, WorkflowRunsRequest workflowRunsRequest);

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
        IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName, WorkflowRunsRequest workflowRunsRequest);

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
        IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, long workflowId, WorkflowRunsRequest workflowRunsRequest, ApiOptions options);

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
        IObservable<WorkflowRunsResponse> ListByWorkflow(string owner, string name, string workflowFileName, WorkflowRunsRequest workflowRunsRequest, ApiOptions options);
    }
}
