namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Workflows API.
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

       
    }
}
