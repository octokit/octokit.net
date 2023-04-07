using System.Threading.Tasks;

namespace Octokit
{
  /// <summary>
  /// A client for GitHub's Actions Self-hosted runners API.
  /// </summary>
  /// <remarks>
  /// See the <a href="https://developer.github.com/v3/actions/self-hosted-runners/">Actions Self-hosted runners API documentation</a> for more information.
  /// </remarks>
  public interface IActionsSelfHostedRunnersClient
  {
    /// <summary>
    /// List self-hosted runners for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    Task<RunnerResponse> ListAllRunnersForEnterprise(string enterprise);

    /// <summary>
    /// List self-hosted runners for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<RunnerResponse> ListAllRunnersForEnterprise(string enterprise, ApiOptions options);

    /// <summary>
    /// List self-hosted runners for an organization
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    Task<RunnerResponse> ListAllRunnersForOrganization(string organization);

    /// <summary>
    /// List self-hosted runners for an organization
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<RunnerResponse> ListAllRunnersForOrganization(string organization, ApiOptions options);

    /// <summary>
    /// List self-hosted runners for a repository
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    Task<RunnerResponse> ListAllRunnersForRepository(string owner, string name);

    /// <summary>
    /// List self-hosted runners for a repository
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<RunnerResponse> ListAllRunnersForRepository(string owner, string name, ApiOptions options);

    /// <summary>
    /// List self-hosted runners in a group for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    Task<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId);

    /// <summary>
    /// List self-hosted runners in a group for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId, ApiOptions options);

    /// <summary>
    /// List self-hosted runners in a group for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    Task<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long runnerGroupId);

    /// <summary>
    /// List self-hosted runners in a group for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long runnerGroupId, ApiOptions options);

    /// <summary>
    /// List runner applications for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    Task<RunnerApplicationResponse> ListAllRunnerApplicationsForEnterprise(string enterprise);

    /// <summary>
    /// List runner applications for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<RunnerApplicationResponse> ListAllRunnerApplicationsForEnterprise(string enterprise, ApiOptions options);

    /// <summary>
    /// List runner applications for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    Task<RunnerApplicationResponse> ListAllRunnerApplicationsForOrganization(string organization);

    /// <summary>
    /// List runner applications for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<RunnerApplicationResponse> ListAllRunnerApplicationsForOrganization(string organization, ApiOptions options);

    /// <summary>
    /// List runner applications for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    Task<RunnerApplicationResponse> ListAllRunnerApplicationsForRepository(string owner, string name);

    /// <summary>
    /// List runner applications for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<RunnerApplicationResponse> ListAllRunnerApplicationsForRepository(string owner, string name, ApiOptions options);

    /// <summary>
    /// Delete a self-hosted runner from an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="runnerId">The runner ID.</param>
    Task DeleteEnterpriseRunner(string enterprise, long runnerId);

    /// <summary>
    /// Delete a self-hosted runner from an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="runnerId">The runner ID.</param>
    /// <param name="options">Options to change the API response.</param>
    Task DeleteEnterpriseRunner(string enterprise, long runnerId, ApiOptions options);

    /// <summary>
    /// Delete a self-hosted runner from an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerId">The runner ID.</param>
    Task DeleteOrganizationRunner(string organization, long runnerId);

    /// <summary>
    /// Delete a self-hosted runner from an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerId">The runner ID.</param>
    /// <param name="options">Options to change the API response.</param>
    Task DeleteOrganizationRunner(string organization, long runnerId, ApiOptions options);

    /// <summary>
    /// Delete a self-hosted runner from a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="runnerId">The runner ID.</param>
    Task DeleteRepositoryRunner(string owner, string name, long runnerId);

    /// <summary>
    /// Delete a self-hosted runner from a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="runnerId">The runner ID.</param>
    /// <param name="options">Options to change the API response.</param>
    Task DeleteRepositoryRunner(string owner, string name, long runnerId, ApiOptions options);

    /// <summary>
    /// Create a registration token for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    Task<AccessToken> CreateEnterpriseRegistrationToken(string enterprise);

    /// <summary>
    /// Create a registration token for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<AccessToken> CreateEnterpriseRegistrationToken(string enterprise, ApiOptions options);

    /// <summary>
    /// Create a registration token for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    Task<AccessToken> CreateOrganizationRegistrationToken(string organization);

    /// <summary>
    /// Create a registration token for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<AccessToken> CreateOrganizationRegistrationToken(string organization, ApiOptions options);

    /// <summary>
    /// Create a registration token for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    Task<AccessToken> CreateRepositoryRegistrationToken(string owner, string name);

    /// <summary>
    /// Create a registration token for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<AccessToken> CreateRepositoryRegistrationToken(string owner, string name, ApiOptions options);

  }
}
