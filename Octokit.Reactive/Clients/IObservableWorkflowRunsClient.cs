using System;

namespace Octokit.Reactive
{
    public interface IObservableWorkflowRunsClient
    {
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId);
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request);
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request, ApiOptions options);

        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name);
        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request);
        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request, ApiOptions options);
    }
}
