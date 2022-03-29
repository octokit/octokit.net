using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Workflows jobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-jobs/">Actions Workflows jobs API documentation</a> for more information.
    /// </remarks>
    public class ActionsWorkflowJobsClient : ApiClient, IActionsWorkflowJobsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Workflows jobs API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsWorkflowJobsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }
    }
}
