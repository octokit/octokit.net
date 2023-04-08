using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableActionsSelfHostedRunnerGroupsClient : IObservableActionsSelfHostedRunnerGroupsClient
    {
        readonly IActionsSelfHostedRunnerGroupsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Self-hosted runner groups API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsSelfHostedRunnerGroupsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.SelfHostedRunnerGroups;
        }

        /// <summary>
        /// List self-hosted runner groups for an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        public IObservable<RunnerGroupResponse> ListAllRunnerGroupsForEnterprise(string enterprise)
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
        public IObservable<RunnerGroupResponse> ListAllRunnerGroupsForEnterprise(string enterprise, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListAllRunnerGroupsForEnterprise(enterprise, options).ToObservable();
        }

        /// <summary>
        /// List self-hosted runner groups for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        public IObservable<RunnerGroupResponse> ListAllRunnerGroupsForOrganization(string org)
        {
            return ListAllRunnerGroupsForOrganization(org, ApiOptions.None);
        }

        /// <summary>
        /// List self-hosted runner groups for an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-self-hosted-runner-groups-for-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RunnerGroupResponse> ListAllRunnerGroupsForOrganization(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListAllRunnerGroupsForOrganization(org, options).ToObservable();
        }

        /// <summary>
        /// List organization access to a self-hosted runner group in an enterprise
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
        /// </remarks>
        /// <param name="enterprise">The enterprise name</param>
        /// <param name="runnerGroupId">The runner group ID</param>
        public IObservable<IReadOnlyList<Organization>> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId)
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
        /// <param name="runnerGroupId">The runner group ID</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IReadOnlyList<Organization>> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(enterprise, nameof(enterprise));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListAllRunnerGroupOrganizationsForEnterprise(enterprise, runnerGroupId, options).ToObservable();
        }

        /// <summary>
        /// List repository access to a self-hosted runner group in an organization
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
        /// </remarks>
        /// <param name="org">The organization name</param>
        /// <param name="runnerGroupId">The runner group ID</param>
        public IObservable<IReadOnlyList<Repository>> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId)
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
        /// <param name="runnerGroupId">The runner group ID</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IReadOnlyList<Repository>> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.ListAllRunnerGroupRepositoriesForOrganization(org, runnerGroupId, options).ToObservable();
        }


    }
}
