using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Self-hosted runner groups API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/self-hosted-runner-groups/">Actions Self-hosted runner groups API documentation</a> for more information.
    /// </remarks>
    public interface IObservableActionsSelfHostedRunnerGroupsClient
    {
        /// <summary>
        /// Get a self-hosted runner group for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#get-a-self-hosted-runner-group-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name.</param>
        /// <param name="runnerGroupId">Unique identifier of the self-hosted runner group.</param>
        IObservable<RunnerGroup> GetRunnerGroupForEnterprise(string enterprise, long runnerGroupId);

        /// <summary>
        /// Get a self-hosted runner group for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#get-a-self-hosted-runner-group-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name.</param>
        /// <param name="runnerGroupId">Unique identifier of the self-hosted runner group.</param>
        IObservable<RunnerGroup> GetRunnerGroupForOrganization(string org, long runnerGroupId);

        /// <summary>
        /// List self-hosted runner groups for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        IObservable<RunnerGroupResponse> ListAllRunnerGroupsForEnterprise(string enterprise);

        /// <summary>
        /// List self-hosted runner groups for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<RunnerGroupResponse> ListAllRunnerGroupsForEnterprise(string enterprise, ApiOptions options);

        /// <summary>
        /// List self-hosted runner groups for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        IObservable<RunnerGroupResponse> ListAllRunnerGroupsForOrganization(string org);

        /// <summary>
        /// List self-hosted runner groups for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<RunnerGroupResponse> ListAllRunnerGroupsForOrganization(string org, ApiOptions options);
        /// <summary>
        /// Gets a list of all self-hosted runners in a group for an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="runnerGroupId">The runner group ID.</param>
        IObservable<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId);

        /// <summary>
        /// Gets a list of all self-hosted runners in a group for an enterprise.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise.</param>
        /// <param name="runnerGroupId">The runner group ID.</param>
        /// <param name="options">Options to change the API response.</param>
        IObservable<RunnerResponse> ListAllRunnersForEnterpriseRunnerGroup(string enterprise, long runnerGroupId, ApiOptions options);

        /// <summary>
        /// Gets a list of all self-hosted runners in a group for an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="runnerGroupId">The runner group ID.</param>
        IObservable<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long runnerGroupId);

        /// <summary>
        /// Gets a list of all self-hosted runners in a group for an organization.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runners-in-a-group-for-an-organization
        /// </remarks>
        /// <param name="organization">The organization.</param>
        /// <param name="runnerGroupId">The runner group ID.</param>
        /// <param name="options">Options to change the API response.</param>
        IObservable<RunnerResponse> ListAllRunnersForOrganizationRunnerGroup(string organization, long runnerGroupId, ApiOptions options);

        /// <summary>
        /// List organization access to a self-hosted runner group in an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        IObservable<OrganizationsResponse> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId);

        /// <summary>
        /// List organization access to a self-hosted runner group in an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<OrganizationsResponse> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId, ApiOptions options);

        /// <summary>
        /// List repository access to a self-hosted runner group in an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        IObservable<RepositoriesResponse> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId);

        /// <summary>
        /// List repository access to a self-hosted runner group in an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="runnerGroupId">The runner group id</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<RepositoriesResponse> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId, ApiOptions options);

    }
}
