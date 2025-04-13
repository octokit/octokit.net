using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Workflows API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflows/">Actions Workflows API documentation</a> for more information.
    /// </remarks>
    public class ActionsWorkflowsClient : ApiClient, IActionsWorkflowsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Workflows API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsWorkflowsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Jobs = new ActionsWorkflowJobsClient(apiConnection);
            Runs = new ActionsWorkflowRunsClient(apiConnection);
        }

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by file name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/dispatches")]
        public Task CreateDispatch(string owner, string name, string workflowFileName, CreateWorkflowDispatch createDispatch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));
            Ensure.ArgumentNotNull(createDispatch, nameof(createDispatch));

            return ApiConnection.Post<object>(ApiUrls.ActionsDispatchWorkflow(owner, name, workflowFileName), createDispatch);
        }

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/dispatches")]
        public Task CreateDispatch(string owner, string name, long workflowId, CreateWorkflowDispatch createDispatch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(createDispatch, nameof(createDispatch));

            return ApiConnection.Post<object>(ApiUrls.ActionsDispatchWorkflow(owner, name, workflowId), createDispatch);
        }

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by file name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        [ManualRoute("POST", "/repositories/{id}/actions/workflows/{workflow_id}/dispatches")]
        public Task CreateDispatch(long repositoryId, string workflowFileName, CreateWorkflowDispatch createDispatch)
        {
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));
            Ensure.ArgumentNotNull(createDispatch, nameof(createDispatch));

            return ApiConnection.Post<object>(ApiUrls.ActionsDispatchWorkflow(repositoryId, workflowFileName), createDispatch);
        }

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        [ManualRoute("POST", "/repositories/{id}/actions/workflows/{workflow_id}/dispatches")]
        public Task CreateDispatch(long repositoryId, long workflowId, CreateWorkflowDispatch createDispatch)
        {
            Ensure.ArgumentNotNull(createDispatch, nameof(createDispatch));

            return ApiConnection.Post<object>(ApiUrls.ActionsDispatchWorkflow(repositoryId, workflowId), createDispatch);
        }

        /// <summary>
        /// Disables a specific workflow in a repository by file name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#disable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/disable")]
        public Task Disable(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return ApiConnection.Put(ApiUrls.ActionsDisableWorkflow(owner, name, workflowFileName));
        }

        /// <summary>
        /// Disables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#disable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/disable")]
        public Task Disable(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Put(ApiUrls.ActionsDisableWorkflow(owner, name, workflowId));
        }

        /// <summary>
        /// Enables a specific workflow in a repository by file name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#enable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/enable")]
        public Task Enable(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return ApiConnection.Put(ApiUrls.ActionsEnableWorkflow(owner, name, workflowFileName));
        }

        /// <summary>
        /// Enables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#enable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/enable")]
        public Task Enable(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Put(ApiUrls.ActionsEnableWorkflow(owner, name, workflowId));
        }

        /// <summary>
        /// Gets a specific workflow in a repository by file name. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}")]
        public Task<Workflow> Get(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return ApiConnection.Get<Workflow>(ApiUrls.ActionsGetWorkflow(owner, name, workflowFileName), null);
        }

        /// <summary>
        /// Gets a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}")]
        public Task<Workflow> Get(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<Workflow>(ApiUrls.ActionsGetWorkflow(owner, name, workflowId), null);
        }

        /// <summary>
        /// Gets usage of a specific workflow in a repository by file name. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-workflow-usage
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/timing")]
        public Task<WorkflowUsage> GetUsage(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return ApiConnection.Get<WorkflowUsage>(ApiUrls.ActionsGetWorkflowUsage(owner, name, workflowFileName), null);
        }

        /// <summary>
        /// Gets usage of a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-workflow-usage
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/timing")]
        public Task<WorkflowUsage> GetUsage(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<WorkflowUsage>(ApiUrls.ActionsGetWorkflowUsage(owner, name, workflowId), null);
        }

        /// <summary>
        /// Lists the workflows in a repository. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#list-repository-workflows
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows")]
        public Task<WorkflowsResponse> List(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return List(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Lists the workflows in a repository. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#list-repository-workflows
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options to change the API response.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows")]
        public async Task<WorkflowsResponse> List(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<WorkflowsResponse>(ApiUrls.ActionsListWorkflows(owner, name), null, options).ConfigureAwait(false);

            return new WorkflowsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Workflows).ToList());
        }

        /// <summary>
        /// Client for the Workflow jobs API.
        /// </summary>
        public IActionsWorkflowJobsClient Jobs { get; private set; }

        /// <summary>
        /// Client for the Workflow runs API.
        /// </summary>
        public IActionsWorkflowRunsClient Runs { get; private set; }
    }
}
