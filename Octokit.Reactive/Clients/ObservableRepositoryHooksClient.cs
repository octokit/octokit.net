using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableRepositoryHooksClient : IObservableRepositoryHooksClient
    {
        readonly IRepositoryHooksClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryHooksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Hooks;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<RepositoryHook> GetAll(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _connection.GetAndFlattenAllPages<RepositoryHook>(ApiUrls.RepositoryHooks(owner, repositoryName));
        }

        /// <summary>
        /// Gets a single hook defined for a repository by id
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<RepositoryHook> Get(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Get(owner, repositoryName, hookId).ToObservable();
        }

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<RepositoryHook> Create(string owner, string repositoryName, NewRepositoryHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");
            Ensure.ArgumentNotNull(hook, "hook");

            return _client.Create(owner, repositoryName, hook).ToObservable();
        }

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<RepositoryHook> Edit(string owner, string repositoryName, int hookId, EditRepositoryHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");
            Ensure.ArgumentNotNull(hook, "hook");

            return _client.Edit(owner, repositoryName, hookId, hook).ToObservable();
        }

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information. 
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook 
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        /// <returns></returns>
        public IObservable<Unit> Test(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Test(owner, repositoryName, hookId).ToObservable();
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Unit> Ping(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Ping(owner, repositoryName, hookId).ToObservable();
        }

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repositoryName"></param>
        /// <param name="hookId"></param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string repositoryName, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Delete(owner, repositoryName, hookId).ToObservable();
        }
    }
}
