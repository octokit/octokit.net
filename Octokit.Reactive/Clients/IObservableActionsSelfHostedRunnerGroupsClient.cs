using System;
using System.Collections.Generic;

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
      /// List organization access to a self-hosted runner group in an enterprise
      /// </summary>
      /// <remarks>
      /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
      /// </remarks>
      /// <param name="enterprise">The enterprise name</param>
      /// <param name="runnerGroupId">The runner group id</param>
      IObservable<IReadOnlyList<Organization>> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId);

      /// <summary>
      /// List organization access to a self-hosted runner group in an enterprise
      /// </summary>
      /// <remarks>
      /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-organization-access-to-a-self-hosted-runner-group-in-an-enterprise
      /// </remarks>
      /// <param name="enterprise">The enterprise name</param>
      /// <param name="runnerGroupId">The runner group id</param>
      /// <param name="options">Options for changing the API response</param>
      IObservable<IReadOnlyList<Organization>> ListAllRunnerGroupOrganizationsForEnterprise(string enterprise, long runnerGroupId, ApiOptions options);

      /// <summary>
      /// List repository access to a self-hosted runner group in an organization
      /// </summary>
      /// <remarks>
      /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
      /// </remarks>
      /// <param name="org">The organization name</param>
      /// <param name="runnerGroupId">The runner group id</param>
      IObservable<IReadOnlyList<Repository>> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId);

      /// <summary>
      /// List repository access to a self-hosted runner group in an organization
      /// </summary>
      /// <remarks>
      /// https://docs.github.com/en/enterprise-cloud@latest/rest/actions/self-hosted-runner-groups?apiVersion=2022-11-28#list-repository-access-to-a-self-hosted-runner-group-in-an-organization
      /// </remarks>
      /// <param name="org">The organization name</param>
      /// <param name="runnerGroupId">The runner group id</param>
      /// <param name="options">Options for changing the API response</param>
      IObservable<IReadOnlyList<Repository>> ListAllRunnerGroupRepositoriesForOrganization(string org, long runnerGroupId, ApiOptions options);

    }
}
