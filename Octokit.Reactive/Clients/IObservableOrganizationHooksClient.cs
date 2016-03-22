using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableOrganizationHooksClient
    {
        /// <summary>
        /// Gets the list of hooks defined for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#list-hooks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<OrganizationHook> GetAll(string org);

        /// <summary>
        /// Gets a single hook defined for a organization by id
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "This is ok; we're matching HTTP verbs not keyworks")]
        IObservable<OrganizationHook> Get(string org, int hookId);

        /// <summary>
        /// Creates a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<OrganizationHook> Create(string org, NewOrganizationHook hook);

        /// <summary>
        /// Edits a hook for a organization
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<OrganizationHook> Edit(string org, int hookId, EditOrganizationHook hook);

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#ping-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<Unit> Ping(string org, int hookId);

        /// <summary>
        /// Deletes a hook for a organization
        /// </summary>
        /// <param name="org"></param>
        /// <param name="hookId"></param>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<Unit> Delete(string org, int hookId);
    }
}