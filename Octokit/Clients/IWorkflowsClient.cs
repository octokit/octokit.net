using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Models.Response;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Workflows API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions/workflows">Git Workflows API documentation</a> for more information.
    /// </remarks>
    public interface IWorkflowsClient
    {
        /// <summary>
        /// list repository workflows
        /// https://docs.github.com/en/rest/reference/actions#list-repository-workflows
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<IReadOnlyList<Workflow>> GetAllForRepository(string owner, string name);
        
        /// <summary>
        /// list repository workflows
        /// https://docs.github.com/en/rest/reference/actions#list-repository-workflows
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Workflow>> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// create a workflow dispatch event
        /// https://docs.github.com/en/rest/reference/actions#create-a-workflow-dispatch-event
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="workflowId">The ID of the workflow</param>
        /// <param name="workflowDispatchEvent">The input to the workflow dispatch event creation</param>
        Task CreateWorkflowDispatchEvent(string owner, string name, string workflowId, WorkflowDispatchEvent workflowDispatchEvent);
    }
}
