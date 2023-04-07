using System;

namespace Octokit.Reactive
{
  /// <summary>
  /// A client for GitHub's Actions Self-hosted runners API.
  /// </summary>
  /// <remarks>
  /// See the <a href="https://developer.github.com/v3/actions/self-hosted-runners/">Actions Self-hosted runners API documentation</a> for more information.
  /// </remarks>
  public interface IObservableActionsSelfHostedRunnersClient
  {
    /// <summary>
    /// Gets a list of all self-hosted runners for an enterprise.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    IObservable<RunnerResponse> ListAllRunnersForEnterprise(string enterprise);

    /// <summary>
    /// Gets a list of all self-hosted runners for an enterprise.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerResponse> ListAllRunnersForEnterprise(string enterprise, ApiOptions options);

    /// <summary>
    /// Gets a list of all self-hosted runners for an organization.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    IObservable<RunnerResponse> ListAllRunnersForOrganization(string organization);

    /// <summary>
    /// Gets a list of all self-hosted runners for an organization.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerResponse> ListAllRunnersForOrganization(string organization, ApiOptions options);

    /// <summary>
    /// Gets a list of all self-hosted runners for a repository.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    IObservable<RunnerResponse> ListAllRunnersForRepository(string owner, string name);

    /// <summary>
    /// Gets a list of all self-hosted runners for a repository.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerResponse> ListAllRunnersForRepository(string owner, string name, ApiOptions options);

    /// <summary>
    /// Gets a list of all self-hosted runners in a group for an enterprise.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-in-a-group-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    IObservable<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId);

    /// <summary>
    /// Gets a list of all self-hosted runners in a group for an enterprise.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-in-a-group-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId, ApiOptions options);

    /// <summary>
    /// Gets a list of all self-hosted runners in a group for an organization.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-in-a-group-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    IObservable<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long runnerGroupId);

    /// <summary>
    /// Gets a list of all self-hosted runners in a group for an organization.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-in-a-group-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="runnerGroupId">The runner group ID.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long runnerGroupId, ApiOptions options);

    /// <summary>
    /// List runner applications for an enterprise
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-runner-applications-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForEnterprise(string enterprise);

    /// <summary>
    /// List runner applications for an enterprise
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-runner-applications-for-an-enterprise
    /// </remarks>
    /// <param name="enterprise">The enterprise.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForEnterprise(string enterprise, ApiOptions options);

    /// <summary>
    /// List runner applications for an organization
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-runner-applications-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForOrganization(string organization);

    /// <summary>
    /// List runner applications for an organization
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-runner-applications-for-an-organization
    /// </remarks>
    /// <param name="organization">The organization.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForOrganization(string organization, ApiOptions options);

    /// <summary>
    /// List runner applications for a repository
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-runner-applications-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForRepository(string owner, string name);

    /// <summary>
    /// List runner applications for a repository
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-runner-applications-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForRepository(string owner, string name, ApiOptions options);

  }
}
