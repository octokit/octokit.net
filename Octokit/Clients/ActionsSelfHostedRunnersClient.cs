using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
    public Task<RunnerResponse> List(string owner, string name)
    {
      return List(owner, name, ApiOptions.None);
    }

    [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runners")]
    public async Task<RunnerResponse> List(string owner, string name, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
      Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

      var results = await ApiConnection.GetAll<RunnerResponse>(ApiUrls.ActionsListSelfHostedRunnersForRepo(owner, name), ApiOptions.None).ConfigureAwait(false);

      return new RunnerResponse(
        results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
        results.SelectMany(x => x.Runners).ToList()
      );
    }
  }
}
