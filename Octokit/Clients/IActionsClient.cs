using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions">Git Actions API documentation</a> for more information.
    /// </remarks>
    public interface IActionsClient
    {
        IWorkflowsClient Workflows { get; }
    }
}