using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Self-hosted runner groups API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/self-hosted-runner-groups/">Actions Self-hosted runner groups API documentation</a> for more information.
    /// </remarks>
    public class ActionsSelfHostedRunnerGroupsClient : ApiClient, IActionsSelfHostedRunnerGroupsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Self-hosted runner groups API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsSelfHostedRunnerGroupsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Get a self-hosted runner group for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#get-a-self-hosted-runner-group-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name.</param>
        /// <param name="runnerGroupId">Unique identifier of the self-hosted runner group.</param>
        [ManualRoute("GET", "/enterprises/{enterprise}/actions/runner-groups/{runner_group_id}")]
        public async Task<RunnerGroup> GetRunnerGroupForEnterprise(string enterprise, long runnerGroupId)
        {
            Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));

            return await ApiConnection.Get<RunnerGroup>(ApiUrls.ActionsGetEnterpriseRunnerGroup(enterprise, runnerGroupId)).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a self-hosted runner group for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#get-a-self-hosted-runner-group-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name.</param>
        /// <param name="runnerGroupId">Unique identifier of the self-hosted runner group.</param>
        [ManualRoute("GET", "/orgs/{org}/actions/runner-groups/{runner_group_id}")]
        public async Task<RunnerGroup> GetRunnerGroupForOrganization(string org, long runnerGroupId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return await ApiConnection.Get<RunnerGroup>(ApiUrls.ActionsGetOrganizationRunnerGroup(org, runnerGroupId)).ConfigureAwait(false);
        }

        /// <summary>
        /// List self-hosted runner groups for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        [ManualRoute("GET", "/enterprises/{enterprise}/actions/runner-groups")]
        public Task<RunnerGroupResponse> ListAllRunnerGroupsForEnterprise(string enterprise)
        {
            return ListAllRunnerGroupsForEnterprise(enterprise, ApiOptions.None);
        }

        /// <summary>
        /// List self-hosted runner groups for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/enterprises/{enterprise}/actions/runner-groups")]
        public async Task<RunnerGroupResponse> ListAllRunnerGroupsForEnterprise(string enterprise, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));

            var results = await ApiConnection.GetAll<RunnerGroupResponse>(ApiUrls.ActionsListEnterpriseRunnerGroups(enterprise), options).ConfigureAwait(false);

            return new RunnerGroupResponse(
              results.Count > 0 ? results[0].TotalCount : 0,
              results.SelectMany(x => x.RunnerGroups).ToList()
            );
        }

        /// <summary>
        /// List self-hosted runners groups for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        [ManualRoute("GET", "/orgs/{org}/actions/runner-groups")]
        public Task<RunnerGroupResponse> ListAllRunnerGroupsForOrganization(string org)
        {
            return ListAllRunnerGroupsForOrganization(org, ApiOptions.None);
        }

        /// <summary>
        /// List self-hosted runners groups for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/orgs/{org}/actions/runner-groups")]
        public async Task<RunnerGroupResponse> ListAllRunnerGroupsForOrganization(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            var results = await ApiConnection.GetAll<RunnerGroupResponse>(ApiUrls.ActionsListOrganizationRunnerGroups(org), options).ConfigureAwait(false);

            return new RunnerGroupResponse(
              results.Count > 0 ? results[0].TotalCount : 0,
              results.SelectMany(x => x.RunnerGroups).ToList()
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
        /// List organization access to a self-hosted runner group in an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        [ManualRoute("GET", "/enterprises/{enterprise}/actions/runner-groups/{runner_group_id}/organizations")]
        public Task<OrganizationsResponse> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId)
        {
            return ListAllRunnerGroupOrganizationsForEnterprise(enterprise, runnerGroupId, ApiOptions.None);
        }

        /// <summary>
        /// List organization access to a self-hosted runner group in an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/enterprises/{enterprise}/actions/runner-groups/{runner_group_id}/organizations")]
        public async Task<OrganizationsResponse> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));

            var results = await ApiConnection.GetAll<OrganizationsResponse>(ApiUrls.ActionsListEnterpriseRunnerGroupOrganizations(enterprise, runnerGroupId), options).ConfigureAwait(false);

            return new OrganizationsResponse(
              results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
              results.SelectMany(x => x.Organizations).ToList()
            );
        }

        /// <summary>
        /// List repository access to a self-hosted runner group in an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        [ManualRoute("GET", "/orgs/{org}/actions/runner-groups/{runner_group_id}/repositories")]
        public Task<RepositoriesResponse> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId)
        {
            return ListAllRunnerGroupRepositoriesForOrganization(org, runnerGroupId, ApiOptions.None);
        }

        /// <summary>
        /// List repository access to a self-hosted runner group in an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/orgs/{org}/actions/runner-groups/{runner_group_id}/repositories")]
        public async Task<RepositoriesResponse> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            var results = await ApiConnection.GetAll<RepositoriesResponse>(ApiUrls.ActionsListOrganizationRunnerGroupRepositories(org, runnerGroupId), options).ConfigureAwait(false);

            return new RepositoriesResponse(
              results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
              results.SelectMany(x => x.Repositories).ToList()
            );
        }
  }
}
