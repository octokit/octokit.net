﻿using System;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Workflows jobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-jobs/">Actions Workflows jobs API documentation</a> for more information.
    /// </remarks>
    public interface IObservableActionsWorkflowJobsClient
    {
        /// <summary>
        /// Re-runs a specific workflow job in a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#re-run-a-job-from-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="jobId">The Id of the workflow job.</param>
        IObservable<Unit> Rerun(string owner, string name, long jobId);

        /// <summary>
        /// Gets a specific job in a workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-jobs/#get-a-job-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="jobId">The unique identifier of the job.</param>
        IObservable<WorkflowJob> Get(string owner, string name, long jobId);

        /// <summary>
        /// Gets the plain text log file for a workflow job.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-jobs/#download-job-logs-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="jobId">The Id of the workflow job.</param>
        IObservable<string> GetLogs(string owner, string name, long jobId);

        /// <summary>
        /// Lists jobs for a specific workflow run.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#list-jobs-for-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runId">The Id of the workflow run.</param>
        IObservable<WorkflowJobsResponse> List(string owner, string name, long runId);

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
        IObservable<WorkflowJobsResponse> List(string owner, string name, long runId, WorkflowRunJobsRequest workflowRunJobsRequest);

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
        IObservable<WorkflowJobsResponse> List(string owner, string name, long runId, WorkflowRunJobsRequest workflowRunJobsRequest, ApiOptions options);

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
        IObservable<WorkflowJobsResponse> List(string owner, string name, long runId, int attemptNumber);

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
        IObservable<WorkflowJobsResponse> List(string owner, string name, long runId, int attemptNumber, ApiOptions options);
    }
}
