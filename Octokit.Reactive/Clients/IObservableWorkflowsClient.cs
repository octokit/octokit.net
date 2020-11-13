using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Threading.Tasks;
using Octokit.Models.Response;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Action Workflows API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflows/">Workflows API documentation</a> for more information.
    /// </remarks>
    public interface IObservableWorkflowsClient
    {
        /// <summary>
        /// list repository workflows
        /// https://docs.github.com/en/rest/reference/actions#list-repository-workflows
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        IObservable<Workflow> GetAllForRepository(string owner, string name);

        /// <summary>
        /// list repository workflows
        /// https://docs.github.com/en/rest/reference/actions#list-repository-workflows
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<Workflow> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// create a workflow dispatch event
        /// https://docs.github.com/en/rest/reference/actions#create-a-workflow-dispatch-event
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="workflowId">The ID of the workflow</param>
        /// <param name="workflowDispatchEvent">The input to the workflow dispatch event creation</param>
        IObservable<Unit> CreateWorkflowDispatchEvent(string owner, string name, string workflowId, WorkflowDispatchEvent workflowDispatchEvent);
    }
}
