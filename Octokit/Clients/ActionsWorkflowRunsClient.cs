using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Workflows runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-runs/">Actions Workflows runs API documentation</a> for more information.
    /// </remarks>
    public class ActionsWorkflowRunsClient : ApiClient, IActionsWorkflowRunsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Workflows runs API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsWorkflowRunsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}
