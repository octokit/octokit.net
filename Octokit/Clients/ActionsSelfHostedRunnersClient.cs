using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
  /// <summary>
  /// A client for GitHub's Actions Self-hosted runners API.
  /// </summary>
  /// <remarks>
  /// See the <a href="https://developer.github.com/v3/actions/self-hosted-runners/">Actions Self-hosted runners API documentation</a> for more information.
  /// </remarks>
  public class ActionsSelfHostedRunnersClient : ApiClient, IActionsSelfHostedRunnersClient
  {
    /// <summary>
    /// Initializes a new GitHub Actions Self-hosted runners API client
    /// </summary>
    /// <param name="apiConnection">An API connection</param>
    public ActionsSelfHostedRunnersClient(IApiConnection apiConnection) : base(apiConnection)
    {
    }

    [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runners")]
    public async Task<IReadOnlyList<Runner>> List(string owner, string name)
    {
      Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
      Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

      return await ApiConnection.GetAll<Runner>(ApiUrls.ActionsListSelfHostedRunnersForRepo(owner, name));
    }

    [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runners")]
    public async Task<IReadOnlyList<Runner>> List(string owner, string name, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
      Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

      return await ApiConnection.GetAll<Runner>(ApiUrls.ActionsListSelfHostedRunnersForRepo(owner, name), options);
    }
  }
}
