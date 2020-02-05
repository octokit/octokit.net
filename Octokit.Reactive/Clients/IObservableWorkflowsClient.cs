using System;
namespace Octokit.Reactive
{
    public interface IObservableWorkflowsClient
    {
        IObservable<WorkflowsResponse> GetAll(string owner, string name);
        IObservable<WorkflowsResponse> GetAll(long repositoryId);

        IObservable<WorkflowsResponse> GetAll(string owner, string name, ApiOptions options);
        IObservable<WorkflowsResponse> GetAll(long repositoryId, ApiOptions options);

        IObservable<Workflow> Get(string owner, string name, long workflowId);
        IObservable<Workflow> Get(string owner, string name, string workflowFileName);
        IObservable<Workflow> Get(long repositoryId, long workflowId);
        IObservable<Workflow> Get(long repositoryId, string workflowFileName);

    }
}
