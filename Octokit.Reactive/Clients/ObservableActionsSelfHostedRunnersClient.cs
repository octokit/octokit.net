using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
  public class ObservableActionsSelfHostedRunnersClient : IObservableActionsSelfHostedRunnersClient
  {
    readonly IActionsSelfHostedRunnersClient _client;

    /// <summary>
    /// Instantiate a new GitHub Actions Self-hosted runners API client.
    /// </summary>
    /// <param name="client">A GitHub client.</param>
    public ObservableActionsSelfHostedRunnersClient(IGitHubClient client)
    {
      Ensure.ArgumentNotNull(client, nameof(client));

      _client = client.Actions.SelfHostedRunners;
    }

    public IObservable<RunnerResponse> ListAllRunnersForEnterprise(string enterprise)
    {
      return ListAllRunnersForEnterprise(enterprise, ApiOptions.None);
    }

    public IObservable<RunnerResponse> ListAllRunnersForEnterprise(string enterprise, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.ListAllRunnersForEnterprise(enterprise, options).ToObservable();
    }

    public IObservable<RunnerResponse> ListAllRunnersForOrganization(string organization)
    {
      return ListAllRunnersForOrganization(organization, ApiOptions.None);
    }

    public IObservable<RunnerResponse> ListAllRunnersForOrganization(string organization, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.ListAllRunnersForOrganization(organization, options).ToObservable();
    }

    public IObservable<RunnerResponse> ListAllRunnersForRepository(string owner, string name)
    {
      return ListAllRunnersForRepository(owner, name, ApiOptions.None);
    }

    public IObservable<RunnerResponse> ListAllRunnersForRepository(string owner, string name, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
      Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.ListAllRunnersForRepository(owner, name, options).ToObservable();
    }

    public IObservable<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long groupId)
    {
      return ListAllRunnersForEnterpriseRunnerGroup(enterprise, groupId, ApiOptions.None);
    }

    public IObservable<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long groupId, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.ListAllRunnersForEnterpriseRunnerGroup(enterprise, groupId, options).ToObservable();
    }

    public IObservable<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long groupId)
    {
      return ListAllRunnersForOrganizationRunnerGroup(organization, groupId, ApiOptions.None);
    }

    public IObservable<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long groupId, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.ListAllRunnersForOrganizationRunnerGroup(organization, groupId, options).ToObservable();
    }

    public IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForEnterprise(string enterprise)
    {
      return ListAllRunnerApplicationsForEnterprise(enterprise, ApiOptions.None);
    }

    public IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForEnterprise(string enterprise, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.ListAllRunnerApplicationsForEnterprise(enterprise, options).ToObservable();
    }

    public IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForOrganization(string organization)
    {
      return ListAllRunnerApplicationsForOrganization(organization, ApiOptions.None);
    }

    public IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForOrganization(string organization, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.ListAllRunnerApplicationsForOrganization(organization, options).ToObservable();
    }

    public IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForRepository(string owner, string name)
    {
      return ListAllRunnerApplicationsForRepository(owner, name, ApiOptions.None);
    }

    public IObservable<RunnerApplicationResponse> ListAllRunnerApplicationsForRepository(string owner, string name, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
      Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.ListAllRunnerApplicationsForRepository(owner, name, options).ToObservable();
    }

    public IObservable<Unit> DeleteEnterpriseRunner(string enterprise, long runnerId)
    {
      return DeleteEnterpriseRunner(enterprise, runnerId, ApiOptions.None);
    }

    public IObservable<Unit> DeleteEnterpriseRunner(string enterprise, long groupId, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.DeleteEnterpriseRunner(enterprise, groupId, options).ToObservable();
    }

    public IObservable<Unit> DeleteOrganizationRunner(string organization, long runnerId)
    {
      return DeleteOrganizationRunner(organization, runnerId, ApiOptions.None);
    }

    public IObservable<Unit> DeleteOrganizationRunner(string organization, long groupId, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.DeleteOrganizationRunner(organization, groupId, options).ToObservable();
    }

    public IObservable<Unit> DeleteRepositoryRunner(string owner, string name, long runnerId)
    {
      return DeleteRepositoryRunner(owner, name, runnerId, ApiOptions.None);
    }

    public IObservable<Unit> DeleteRepositoryRunner(string owner, string name, long groupId, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.DeleteRepositoryRunner(owner, name, groupId, options).ToObservable();
    }

    public IObservable<AccessToken> CreateEnterpriseRegistrationToken(string enterprise)
    {
      return CreateEnterpriseRegistrationToken(enterprise, ApiOptions.None);
    }

    public IObservable<AccessToken> CreateEnterpriseRegistrationToken(string enterprise, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.CreateEnterpriseRegistrationToken(enterprise, options).ToObservable();
    }

    public IObservable<AccessToken> CreateOrganizationRegistrationToken(string organization)
    {
      return CreateOrganizationRegistrationToken(organization, ApiOptions.None);
    }

    public IObservable<AccessToken> CreateOrganizationRegistrationToken(string organization, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.CreateOrganizationRegistrationToken(organization, options).ToObservable();
    }

    public IObservable<AccessToken> CreateRepositoryRegistrationToken(string owner, string name)
    {
      return CreateRepositoryRegistrationToken(owner, name, ApiOptions.None);
    }

    public IObservable<AccessToken> CreateRepositoryRegistrationToken(string owner, string name, ApiOptions options)
    {
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.CreateRepositoryRegistrationToken(owner, name, options).ToObservable();
    }
  }
}
