using System;

namespace Octokit.Reactive
{
    public interface IObservableWorkflowJobsClient
    {
        IObservable<WorkflowJobsResponse> GetAll(string owner, string name, long runId);
        IObservable<WorkflowJobsResponse> GetAll(long repositoryId, long runId);

        IObservable<WorkflowJobsResponse> GetAll(string owner, string name, long runId, ApiOptions options);
        IObservable<WorkflowJobsResponse> GetAll(long repositoryId, long runId, ApiOptions options);

        IObservable<WorkflowJob> Get(string owner, string name, long jobId);
        IObservable<WorkflowJob> Get(long repositoryId, long jobId);

        IObservable<string> LogsUrl(string owner, string name, long jobId);
        IObservable<string> LogsUrl(long repositoryId, long jobId);

    }
}
