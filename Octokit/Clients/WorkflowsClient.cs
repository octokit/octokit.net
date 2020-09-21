using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Models.Response;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Action Workflows API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflows/">Workflows API documentation</a> for more information.
    /// </remarks>
    public class WorkflowsClient : ApiClient, IWorkflowsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Workflows API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public WorkflowsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// list repository workflows
        /// https://docs.github.com/en/rest/reference/actions#list-repository-workflows
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/workflows")]
        public Task<IReadOnlyList<Workflow>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNull(owner, nameof(owner));
            Ensure.ArgumentNotNull(name, nameof(name));
            return ApiConnection.GetAll<Workflow>(ApiUrls.Workflows(owner, name));
        }

        /// <summary>
        /// create a workflow dispatch event
        /// https://docs.github.com/en/rest/reference/actions#create-a-workflow-dispatch-event
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="workflowId">The ID of the workflow</param>
        /// <param name="workflowDispatchEvent">The input to the workflow dispatch event creation</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/workflows/{workflow_id}/dispatches")]
        public Task CreateWorkflowDispatchEvent(string owner, string name, string workflowId, WorkflowDispatchEvent workflowDispatchEvent)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowId, nameof(workflowId));
            Ensure.ArgumentNotNull(workflowDispatchEvent, nameof(workflowDispatchEvent));
            
            return ApiConnection.Post(ApiUrls.CreateWorkflowDispatchEvent(owner, name, workflowId), workflowDispatchEvent);
        }
        
    }
}
