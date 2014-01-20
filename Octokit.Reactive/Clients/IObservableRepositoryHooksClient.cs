using System;
using System.Collections.Generic;
using System.Reactive;

namespace Octokit.Reactive.Clients
{
    public interface IObservableRepositoryHooksClient
    {
        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<IReadOnlyList<RepositoryHook>> Get(string owner, string repositoryName);

        /// <summary>
        /// Gets a single hook defined for a repository by id
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<RepositoryHook> GetById(string owner, string repositoryName, int hookId);

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<RepositoryHook> Create(string owner, string repositoryName, NewRepositoryHook hook);

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<RepositoryHook> Edit(string owner, string repositoryName, int hookId, EditRepositoryHook hook);

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information. 
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook 
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        /// <returns></returns>
        IObservable<Unit> Test(string owner, string repositoryName, int hookId);

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repositoryName"></param>
        /// <param name="hookId"></param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<Unit> Delete(string owner, string repositoryName, int hookId);
    }
}