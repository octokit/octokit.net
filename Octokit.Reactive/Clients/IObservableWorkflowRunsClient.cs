using System;

namespace Octokit.Reactive
{
    public interface IObservableWorkflowRunsClient
    {
        /// <summary>
        /// Lists all workflow runs for a repository
        /// </summary>
        /// <param name="repositoryId">Id of the repository</param>
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId);
        /// <summary>
        /// Lists all workflow runs for a repository
        /// </summary>
        /// <param name="repositoryId">Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of workflow runs returned</param>
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request);
        /// <summary>
        /// Lists all workflow runs for a repository
        /// </summary>
        /// <param name="repositoryId">Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of workflow runs returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request, ApiOptions options);

        /// <summary>
        /// Lists all workflow runs for a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name);
        /// <summary>
        /// Lists all workflow runs for a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of workflow runs returned</param>
        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request);
        /// <summary>
        /// Lists all workflow runs for a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of workflow runs returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request, ApiOptions options);
    }
}
