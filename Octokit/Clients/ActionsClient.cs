namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions API. Gives you access to manage workflows
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions/">Git API documentation</a> for more information.
    /// </remarks>
    public class ActionsClient : ApiClient, IActionsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Git API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            Workflows = new WorkflowsClient(apiConnection);
        }

        public IWorkflowsClient Workflows { get; private set; }
    }
}