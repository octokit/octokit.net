using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Self-hosted runners API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#about-self-hosted-runners-in-github-actions">Actions Self-hosted runners API documentation</a> for more information.
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
        /// List runner applications for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        [ManualRoute("GET", "/enterprises/{enterprise}/actions/runners/downloads")]
        public Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForEnterprise(string enterprise)
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
        public async Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForEnterprise(string enterprise, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));

            return await ApiConnection.GetAll<RunnerApplication>(ApiUrls.ActionsListRunnerApplicationsForEnterprise(enterprise), options).ConfigureAwait(false);
        }

        /// <summary>
        /// List runner applications for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#list-runner-applications-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        [ManualRoute("GET", "/orgs/{org}/actions/runners/downloads")]
        public Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForOrganization(string organization)
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
        public async Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForOrganization(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return await ApiConnection.GetAll<RunnerApplication>(ApiUrls.ActionsListRunnerApplicationsForOrganization(organization), options).ConfigureAwait(false);
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
        public Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForRepository(string owner, string repo)
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
        public async Task<IReadOnlyList<RunnerApplication>> ListAllRunnerApplicationsForRepository(string owner, string repo, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return await ApiConnection.GetAll<RunnerApplication>(ApiUrls.ActionsListRunnerApplicationsForRepository(owner, repo), options).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a self-hosted runner from an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="runnerId">The runner id.</param>
        [ManualRoute("DELETE", "/enterprises/{enterprise}/actions/runners/{runner_id}")]
        public Task DeleteEnterpriseRunner(string enterprise, long runnerId)
        {
            Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));

            return ApiConnection.Delete(ApiUrls.ActionsDeleteEnterpriseRunner(enterprise, runnerId));
        }

        /// <summary>
        /// Delete a self-hosted runner from an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="runnerId">The runner id.</param>
        [ManualRoute("DELETE", "/orgs/{org}/actions/runners/{runner_id}")]
        public Task DeleteOrganizationRunner(string organization, long runnerId)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return ApiConnection.Delete(ApiUrls.ActionsDeleteOrganizationRunner(organization, runnerId));
        }

        /// <summary>
        /// Delete a self-hosted runner from a repository
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#delete-a-self-hosted-runner-from-a-repository
        /// </remarks>
        /// <param name="owner">The owner.</param>
        /// <param name="repo">The repo.</param>
        /// <param name="runnerId">The runner id.</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/actions/runners/{runner_id}")]
        public Task DeleteRepositoryRunner(string owner, string repo, long runnerId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return ApiConnection.Delete(ApiUrls.ActionsDeleteRepositoryRunner(owner, repo, runnerId));
        }

        /// <summary>
        /// Create a self-hosted runner registration token for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-self-hosted-runner-registration-token-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        [ManualRoute("POST", "/enterprises/{enterprise}/actions/runners/registration-token")]
        public Task<AccessToken> CreateEnterpriseRegistrationToken(string enterprise)
        {
            return CreateEnterpriseRegistrationToken(enterprise, CancellationToken.None);
        }

        /// <summary>
        /// Create a self-hosted runner registration token for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-self-hosted-runner-registration-token-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        [ManualRoute("POST", "/enterprises/{enterprise}/actions/runners/registration-token")]
        public Task<AccessToken> CreateEnterpriseRegistrationToken(string enterprise, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));

            return ApiConnection.Post<AccessToken>(ApiUrls.ActionsCreateEnterpriseRegistrationToken(enterprise), cancellationToken);
        }

        /// <summary>
        /// Create a self-hosted runner registration token for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-self-hosted-runner-registration-token-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        [ManualRoute("POST", "/orgs/{org}/actions/runners/registration-token")]
        public Task<AccessToken> CreateOrganizationRegistrationToken(string organization)
        {
            return CreateOrganizationRegistrationToken(organization, CancellationToken.None);
        }

        /// <summary>
        /// Create a self-hosted runner registration token for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-self-hosted-runner-registration-token-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        [ManualRoute("POST", "/orgs/{org}/actions/runners/registration-token")]
        public Task<AccessToken> CreateOrganizationRegistrationToken(string organization, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return ApiConnection.Post<AccessToken>(ApiUrls.ActionsCreateOrganizationRegistrationToken(organization), cancellationToken);
        }

        /// <summary>
        /// Create a self-hosted runner registration token for a repository
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-self-hosted-runner-registration-token-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner.</param>
        /// <param name="repo">The repo.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/runners/registration-token")]
        public Task<AccessToken> CreateRepositoryRegistrationToken(string owner, string repo)
        {
            return CreateRepositoryRegistrationToken(owner, repo, CancellationToken.None);
        }

        /// <summary>
        /// Create a self-hosted runner registration token for a repository
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runners?apiVersion=2022-11-28#create-a-self-hosted-runner-registration-token-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner.</param>
        /// <param name="repo">The repo.</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/runners/registration-token")]
        public Task<AccessToken> CreateRepositoryRegistrationToken(string owner, string repo, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return ApiConnection.Post<AccessToken>(ApiUrls.ActionsCreateRepositoryRegistrationToken(owner, repo), cancellationToken);
        }
    }
}
