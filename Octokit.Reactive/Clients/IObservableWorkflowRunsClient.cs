using System;
namespace Octokit.Reactive
{
    public interface IObservableWorkflowRunsClient
    {
        IObservable<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId);
        IObservable<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId);

        IObservable<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId, WorkflowRunsRequest request);
        IObservable<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId, WorkflowRunsRequest request);

        IObservable<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId, WorkflowRunsRequest request, ApiOptions options);
        IObservable<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId, WorkflowRunsRequest request, ApiOptions options);

        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name);
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId);

        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request);
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request);

        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request, ApiOptions options);
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request, ApiOptions options);

        IObservable<WorkflowRun> Get(string owner, string name, long runId);
        IObservable<WorkflowRun> Get(long repositoryId, long runId);

        IObservable<bool> ReRun(string owner, string name, long runId);

        IObservable<bool> ReRun(long repositoryId, long runId);

        IObservable<bool> Cancel(string owner, string name, long runId);

        IObservable<bool> Cancel(long repositoryId, long runId);

        IObservable<string> LogsUrl(string owner, string name, long runId);

        IObservable<string> LogsUrl(long repositoryId, long runId);


    }
}
