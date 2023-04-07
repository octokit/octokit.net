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

    /// <summary>
    /// Lists all self-hosted runners for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    [ManualRoute("GET", "/enterprises/{enterprise}/actions/runners")]
    public Task<RunnerResponse> ListAllRunnersForEnterprise(string enterprise)
    {
      return ListAllRunnersForEnterprise(enterprise, ApiOptions.None);
    }

    /// <summary>
    /// Lists all self-hosted runners for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="options">Options for changing the API response</param>
    [ManualRoute("GET", "/enterprises/{enterprise}/actions/runners")]
    public async Task<RunnerResponse> ListAllRunnersForEnterprise(string enterprise, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));
      Ensure.ArgumentNotNull(options, nameof(options));

      var results = await ApiConnection.GetAll<RunnerResponse>(ApiUrls.ActionsListSelfHostedRunnersForEnterprise(enterprise), options).ConfigureAwait(false);

      return new RunnerResponse(
        results.Count > 0 ? results[0].TotalCount : 0,
        results.SelectMany(x => x.Runners).ToList()
      );
    }

    /// <summary>
    /// Lists all self-hosted runners for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    [ManualRoute("GET", "/orgs/{org}/actions/runners")]
    public Task<RunnerResponse> ListAllRunnersForOrganization(string organization)
    {
      return ListAllRunnersForOrganization(organization, ApiOptions.None);
    }

    /// <summary>
    /// Lists all self-hosted runners for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="options">Options for changing the API response</param>
    [ManualRoute("GET", "/orgs/{org}/actions/runners")]
    public async Task<RunnerResponse> ListAllRunnersForOrganization(string organization, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

      var results = await ApiConnection.GetAll<RunnerResponse>(ApiUrls.ActionsListSelfHostedRunnersForOrganization(organization), options).ConfigureAwait(false);

      return new RunnerResponse(
        results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
        results.SelectMany(x => x.Runners).ToList()
      );
    }


    /// <summary>
    /// Lists all self-hosted runners for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runners")]
    public Task<RunnerResponse> ListAllRunnersForRepository(string owner, string name)
    {
      return ListAllRunnersForRepository(owner, name, ApiOptions.None);
    }

    /// <summary>
    /// Lists all self-hosted runners for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="options">Options for changing the API response</param>
    [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runners")]
    public async Task<RunnerResponse> ListAllRunnersForRepository(string owner, string name, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
      Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

      var results = await ApiConnection.GetAll<RunnerResponse>(ApiUrls.ActionsListSelfHostedRunnersForRepository(owner, name), options).ConfigureAwait(false);

      return new RunnerResponse(
        results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
        results.SelectMany(x => x.Runners).ToList()
      );
    }

    /// <summary>
    /// List self-hosted runners in a group for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    [ManualRoute("GET", "/enterprises/{enterprise}/actions/runner-groups/{runner_group_id}/runners")]
    public Task<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId)
    {
      return ListAllRunnersForEnterpriseRunnerGroup(enterprise, runnerGroupId, ApiOptions.None);
    }

    /// <summary>
    /// List self-hosted runners in a group for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    /// <param name="options">Options for changing the API response</param>
    [ManualRoute("GET", "/enterprises/{enterprise}/actions/runner-groups/{runner_group_id}/runners")]
    public async Task<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));

      var results = await ApiConnection.GetAll<RunnerResponse>(ApiUrls.ActionsListSelfHostedRunnersForEnterpriseRunnerGroup(enterprise, runnerGroupId), options).ConfigureAwait(false);

      return new RunnerResponse(
        results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
        results.SelectMany(x => x.Runners).ToList()
      );
    }

    /// <summary>
    /// List self-hosted runners in a group for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    [ManualRoute("GET", "/orgs/{org}/actions/runner-groups/{runner_group_id}/runners")]
    public Task<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long runnerGroupId)
    {
      return ListAllRunnersForOrganizationRunnerGroup(organization, runnerGroupId, ApiOptions.None);
    }

    /// <summary>
    /// List self-hosted runners in a group for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    /// <param name="options">Options for changing the API response</param>
    [ManualRoute("GET", "/orgs/{org}/actions/runner-groups/{runner_group_id}/runners")]
    public async Task<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long runnerGroupId, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

      var results = await ApiConnection.GetAll<RunnerResponse>(ApiUrls.ActionsListSelfHostedRunnersForOrganizationRunnerGroup(organization, runnerGroupId), options).ConfigureAwait(false);

      return new RunnerResponse(
        results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
        results.SelectMany(x => x.Runners).ToList()
      );
    }

    /// <summary>
    /// List self-hosted runners in a group for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-a-repository
    /// </remarks>


    /// <summary>
    /// List runner applications for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    [ManualRoute("GET", "/enterprises/{enterprise}/actions/runners/downloads")]
    public Task<RunnerApplicationResponse> ListAllRunnerApplicationsForEnterprise(string enterprise)
    {
      return ListAllRunnerApplicationsForEnterprise(enterprise, ApiOptions.None);
    }

    /// <summary>
    /// List runner applications for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="options">Options for changing the API response</param>
    [ManualRoute("GET", "/enterprises/{enterprise}/actions/runners/downloads")]
    public async Task<RunnerApplicationResponse> ListAllRunnerApplicationsForEnterprise(string enterprise, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));

      var results = await ApiConnection.GetAll<RunnerApplicationResponse>(ApiUrls.ActionsListRunnerApplicationsForEnterprise(enterprise), options).ConfigureAwait(false);

      return new RunnerApplicationResponse(
        results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
        results.SelectMany(x => x.RunnerApplications).ToList()
      );
    }

    /// <summary>
    /// List runner applications for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    [ManualRoute("GET", "/orgs/{org}/actions/runners/downloads")]
    public Task<RunnerApplicationResponse> ListAllRunnerApplicationsForOrganization(string organization)
    {
      return ListAllRunnerApplicationsForOrganization(organization, ApiOptions.None);
    }

    /// <summary>
    /// List runner applications for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="options">Options for changing the API response</param>
    [ManualRoute("GET", "/orgs/{org}/actions/runners/downloads")]
    public async Task<RunnerApplicationResponse> ListAllRunnerApplicationsForOrganization(string organization, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

      var results = await ApiConnection.GetAll<RunnerApplicationResponse>(ApiUrls.ActionsListRunnerApplicationsForOrganization(organization), options).ConfigureAwait(false);

      return new RunnerApplicationResponse(
        results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
        results.SelectMany(x => x.RunnerApplications).ToList()
      );
    }

    /// <summary>
    /// List runner applications for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner.</param>
    /// <param name="repo">The repo.</param>
    [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runners/downloads")]
    public Task<RunnerApplicationResponse> ListAllRunnerApplicationsForRepository(string owner, string repo)
    {
      return ListAllRunnerApplicationsForRepository(owner, repo, ApiOptions.None);
    }


    /// <summary>
    /// List runner applications for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner.</param>
    /// <param name="repo">The repo.</param>
    /// <param name="options">Options for changing the API response</param>
    [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runners/downloads")]
    public async Task<RunnerApplicationResponse> ListAllRunnerApplicationsForRepository(string owner, string repo, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
      Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

      var results = await ApiConnection.GetAll<RunnerApplicationResponse>(ApiUrls.ActionsListRunnerApplicationsForRepository(owner, repo), options).ConfigureAwait(false);

      return new RunnerApplicationResponse(
        results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
        results.SelectMany(x => x.RunnerApplications).ToList()
      );
    }

  }
}
