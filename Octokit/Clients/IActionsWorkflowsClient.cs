using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Workflows API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflows/">Actions Workflows API documentation</a> for more information.
    /// </remarks>
    public interface IActionsWorkflowsClient
    {
        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by slug.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        Task CreateDispatch(string owner, string name, string workflowFileName, CreateWorkflowDispatch createDispatch);

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by slug.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        Task CreateDispatch(string owner, string name, long workflowId, CreateWorkflowDispatch createDispatch);

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        Task CreateDispatch(long repositoryId, string workflowFileName, CreateWorkflowDispatch createDispatch);

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        Task CreateDispatch(long repositoryId, long workflowId, CreateWorkflowDispatch createDispatch);

        /// <summary>
        /// Disables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#disable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        Task Disable(string owner, string name, string workflowFileName);

        /// <summary>
        /// Disables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#disable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        Task Disable(string owner, string name, long workflowId);

        /// <summary>
        /// Enables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#enable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        Task Enable(string owner, string name, string workflowFileName);

        /// <summary>
        /// Enables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#enable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        Task Enable(string owner, string name, long workflowId);

        /// <summary>
        /// Gets a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        Task<Workflow> Get(string owner, string name, string workflowFileName);

        /// <summary>
        /// Gets a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        Task<Workflow> Get(string owner, string name, long workflowId);

        /// <summary>
        /// Gets usage of a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-workflow-usage
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        Task<WorkflowUsage> GetUsage(string owner, string name, string workflowFileName);

        /// <summary>
        /// Gets usage of a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-workflow-usage
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        Task<WorkflowUsage> GetUsage(string owner, string name, long workflowId);

        /// <summary>
        /// Lists the workflows in a repository. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#list-repository-workflows
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        Task<WorkflowsResponse> List(string owner, string name);

        /// <summary>
        /// Lists the workflows in a repository. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#list-repository-workflows
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options to change the API response.</param>
        Task<WorkflowsResponse> List(string owner, string name, ApiOptions options);

        /// <summary>
        /// Client for the Workflow jobs API.
        /// </summary>
        IActionsWorkflowJobsClient Jobs { get; }

        /// <summary>
        /// Client for the Workflow runs API.
        /// </summary>
        IActionsWorkflowRunsClient Runs { get; }
    }
}
