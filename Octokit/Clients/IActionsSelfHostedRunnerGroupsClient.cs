using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Self-hosted runner groups API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/self-hosted-runner-groups/">Actions Self-hosted runner groups API documentation</a> for more information.
    /// </remarks>
    public interface IActionsSelfHostedRunnerGroupsClient
    {
        /// <summary>
        /// Get a self-hosted runner group for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#get-a-self-hosted-runner-group-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name.</param>
        /// <param name="runnerGroupId">Unique identifier of the self-hosted runner group.</param>
        Task<RunnerGroup> GetRunnerGroupForEnterprise(string enterprise, long runnerGroupId);

        /// <summary>
        /// Get a self-hosted runner group for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#get-a-self-hosted-runner-group-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name.</param>
        /// <param name="runnerGroupId">Unique identifier of the self-hosted runner group.</param>
        Task<RunnerGroup> GetRunnerGroupForOrganization(string org, long runnerGroupId);

        /// <summary>
        /// List self-hosted runner groups for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        Task<RunnerGroupResponse> ListAllRunnerGroupsForEnterprise(string enterprise);

        /// <summary>
        /// List self-hosted runner groups for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="options">Options for changing the API response</param>
        Task<RunnerGroupResponse> ListAllRunnerGroupsForEnterprise(string enterprise, ApiOptions options);

        /// <summary>
        /// List self-hosted runners groups for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        Task<RunnerGroupResponse> ListAllRunnerGroupsForOrganization(string org);

        /// <summary>
        /// List self-hosted runners groups for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="options">Options for changing the API response</param>
        Task<RunnerGroupResponse> ListAllRunnerGroupsForOrganization(string org, ApiOptions options);

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
        /// List organization access to a self-hosted runner group in an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        Task<OrganizationsResponse> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId);

        /// <summary>
        /// List organization access to a self-hosted runner group in an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        /// <param name="options">Options for changing the API response</param>
        Task<OrganizationsResponse> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId, ApiOptions options);

        /// <summary>
        /// List repository access to a self-hosted runner group in an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        Task<RepositoriesResponse> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId);

        /// <summary>
        /// List repository access to a self-hosted runner group in an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        /// <param name="options">Options for changing the API response</param>
        Task<RepositoriesResponse> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId, ApiOptions options);
    }
}
