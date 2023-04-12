using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableActionsSelfHostedRunnersClient : IObservableActionsSelfHostedRunnersClient
    {
        readonly IActionsSelfHostedRunnersClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Instantiate a new GitHub Actions Self-hosted runners API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsSelfHostedRunnersClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.SelfHostedRunners;
            _connection = client.Connection;
        }

        /// <summary>
        /// Lists self-hosted runners for an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        public IObservable<RunnerResponse> ListAllRunnersForEnterprise(string enterprise)
        {
            return ListAllRunnersForEnterprise(enterprise, ApiOptions.None);
        }

        /// <summary>
        /// Lists self-hosted runners for an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RunnerResponse> ListAllRunnersForEnterprise(string enterprise, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListAllRunnersForEnterprise(enterprise, options).ToObservable();
        }

        /// <summary>
        /// Lists self-hosted runners for an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        public IObservable<RunnerResponse> ListAllRunnersForOrganization(string organization)
        {
            return ListAllRunnersForOrganization(organization, ApiOptions.None);
        }

        /// <summary>
        /// Lists self-hosted runners for an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RunnerResponse> ListAllRunnersForOrganization(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListAllRunnersForOrganization(organization, options).ToObservable();
        }

        /// <summary>
        /// Lists self-hosted runners for a repository.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        public IObservable<RunnerResponse> ListAllRunnersForRepository(string owner, string name)
        {
            return ListAllRunnersForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Lists self-hosted runners for a repository.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-self-hosted-runners-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RunnerResponse> ListAllRunnersForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListAllRunnersForRepository(owner, name, options).ToObservable();
        }

        /// <summary>
        /// Lists all runner applications for an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        public IObservable<RunnerApplication> ListAllRunnerApplicationsForEnterprise(string enterprise)
        {
            return ListAllRunnerApplicationsForEnterprise(enterprise, ApiOptions.None);
        }

        /// <summary>
        /// Lists all runner applications for an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RunnerApplication> ListAllRunnerApplicationsForEnterprise(string enterprise, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<RunnerApplication>(ApiUrls.ActionsListRunnerApplicationsForEnterprise(enterprise));
        }

        /// <summary>
        /// Lists all runner applications for an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        public IObservable<RunnerApplication> ListAllRunnerApplicationsForOrganization(string organization)
        {
            return ListAllRunnerApplicationsForOrganization(organization, ApiOptions.None);
        }

        /// <summary>
        /// Lists all runner applications for an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RunnerApplication> ListAllRunnerApplicationsForOrganization(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<RunnerApplication>(ApiUrls.ActionsListRunnerApplicationsForOrganization(organization));
        }

        /// <summary>
        /// Lists all runner applications for a repository.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        public IObservable<RunnerApplication> ListAllRunnerApplicationsForRepository(string owner, string name)
        {
            return ListAllRunnerApplicationsForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Lists all runner applications for a repository.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RunnerApplication> ListAllRunnerApplicationsForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<RunnerApplication>(ApiUrls.ActionsListRunnerApplicationsForRepository(owner, name));
        }

        /// <summary>
        /// Deletes a self-hosted runner from an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="runnerId">The runner id.</param>
        public IObservable<Unit> DeleteEnterpriseRunner(string enterprise, long runnerId)
        {
            return DeleteEnterpriseRunner(enterprise, runnerId, ApiOptions.None);
        }

        /// <summary>
        /// Deletes a self-hosted runner from an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="runnerId">The runner id.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Unit> DeleteEnterpriseRunner(string enterprise, long groupId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.DeleteEnterpriseRunner(enterprise, groupId, options).ToObservable();
        }

        /// <summary>
        /// Deletes a self-hosted runner from an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="runnerId">The runner id.</param>
        public IObservable<Unit> DeleteOrganizationRunner(string organization, long runnerId)
        {
            return DeleteOrganizationRunner(organization, runnerId, ApiOptions.None);
        }

        /// <summary>
        /// Deletes a self-hosted runner from an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="runnerId">The runner id.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Unit> DeleteOrganizationRunner(string organization, long runnerId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.DeleteOrganizationRunner(organization, runnerId, options).ToObservable();
        }

        /// <summary>
        /// Deletes a self-hosted runner from a repository.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runnerId">The runner id.</param>
        public IObservable<Unit> DeleteRepositoryRunner(string owner, string name, long runnerId)
        {
            return DeleteRepositoryRunner(owner, name, runnerId, ApiOptions.None);
        }

        /// <summary>
        /// Deletes a self-hosted runner from a repository.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="runnerId">The runner id.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Unit> DeleteRepositoryRunner(string owner, string name, long runnerId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.DeleteRepositoryRunner(owner, name, runnerId, options).ToObservable();
        }

        /// <summary>
        /// Creates a registration token for an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>

        public IObservable<AccessToken> CreateEnterpriseRegistrationToken(string enterprise)
        {
            return CreateEnterpriseRegistrationToken(enterprise, ApiOptions.None);
        }

        /// <summary>
        /// Creates a registration token for an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<AccessToken> CreateEnterpriseRegistrationToken(string enterprise, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.CreateEnterpriseRegistrationToken(enterprise, options).ToObservable();
        }

        /// <summary>
        /// Creates a registration token for an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        public IObservable<AccessToken> CreateOrganizationRegistrationToken(string organization)
        {
            return CreateOrganizationRegistrationToken(organization, ApiOptions.None);
        }

        /// <summary>
        /// Creates a registration token for an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<AccessToken> CreateOrganizationRegistrationToken(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.CreateOrganizationRegistrationToken(organization, options).ToObservable();
        }

        /// <summary>
        /// Creates a registration token for a repository.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        public IObservable<AccessToken> CreateRepositoryRegistrationToken(string owner, string name)
        {
            return CreateRepositoryRegistrationToken(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Creates a registration token for a repository.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-registration-token-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<AccessToken> CreateRepositoryRegistrationToken(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.CreateRepositoryRegistrationToken(owner, name, options).ToObservable();
        }
    }
}
