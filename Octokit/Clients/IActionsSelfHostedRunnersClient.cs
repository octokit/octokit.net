using System.Collections.Generic;
using System.Threading;
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
    /// List runner applications for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForEnterprise(string enterprise);

    /// <summary>
    /// List runner applications for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForEnterprise(string enterprise, ApiOptions options);

    /// <summary>
    /// List runner applications for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForOrganization(string organization);

    /// <summary>
    /// List runner applications for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForOrganization(string organization, ApiOptions options);

    /// <summary>
    /// List runner applications for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForRepository(string owner, string name);

    /// <summary>
    /// List runner applications for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="options">Options to change the API response.</param>
    Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForRepository(string owner, string name, ApiOptions options);

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
    /// Delete a self-hosted runner from an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerId">The runner ID.</param>
    Task DeleteOrganizationRunner(string organization, long runnerId);

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
    /// Create a registration token for an enterprise
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
    Task<AccessToken> CreateEnterpriseRegistrationToken(string enterprise, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a registration token for an organization
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
    Task<AccessToken> CreateOrganizationRegistrationToken(string organization, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a registration token for a repository
    /// </summary>
    /// <remarks>
    /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
    Task<AccessToken> CreateRepositoryRegistrationToken(string owner, string name, CancellationToken cancellationToken = default);

  }
}
