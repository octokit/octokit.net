using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Webhooks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/hooks/">Webhooks API documentation</a> for more information.
    /// </remarks>
    public class ObservableRepositoryHooksClient : IObservableRepositoryHooksClient
    {
        readonly IRepositoryHooksClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryHooksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Hooks;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<RepositoryHook>(ApiUrls.RepositoryHooks(owner, name), options);
        }

        /// <summary>
        /// Gets the list of hooks defined for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#list">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<RepositoryHook>(ApiUrls.RepositoryHooks(repositoryId), options);
        }

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> Get(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, hookId).ToObservable();
        }

        /// <summary>
        /// Gets a single hook by Id
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#get-single-hook">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> Get(long repositoryId, int hookId)
        {
            return _client.Get(repositoryId, hookId).ToObservable();
        }

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> Create(string owner, string name, NewRepositoryHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return _client.Create(owner, name, hook).ToObservable();
        }

        /// <summary>
        /// Creates a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hook">The hook's parameters</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#create-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> Create(long repositoryId, NewRepositoryHook hook)
        {
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return _client.Create(repositoryId, hook).ToObservable();
        }

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <param name="hook">The requested changes to an edit repository hook</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> Edit(string owner, string name, int hookId, EditRepositoryHook hook)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return _client.Edit(owner, name, hookId, hook).ToObservable();
        }

        /// <summary>
        /// Edits a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <param name="hook">The requested changes to an edit repository hook</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<RepositoryHook> Edit(long repositoryId, int hookId, EditRepositoryHook hook)
        {
            Ensure.ArgumentNotNull(hook, nameof(hook));

            return _client.Edit(repositoryId, hookId, hook).ToObservable();
        }

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information. 
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook 
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        public IObservable<Unit> Test(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Test(owner, name, hookId).ToObservable();
        }

        /// <summary>
        /// Tests a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#test-a-hook">API documentation</a> for more information. 
        /// This will trigger the hook with the latest push to the current repository if the hook is subscribed to push events. If the hook 
        /// is not subscribed to push events, the server will respond with 204 but no test POST will be generated.</remarks>
        public IObservable<Unit> Test(long repositoryId, int hookId)
        {
            return _client.Test(repositoryId, hookId).ToObservable();
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<Unit> Ping(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Ping(owner, name, hookId).ToObservable();
        }

        /// <summary>
        /// This will trigger a ping event to be sent to the hook.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#edit-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<Unit> Ping(long repositoryId, int hookId)
        {
            return _client.Ping(repositoryId, hookId).ToObservable();
        }

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<Unit> Delete(string owner, string name, int hookId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, hookId).ToObservable();
        }

        /// <summary>
        /// Deletes a hook for a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="hookId">The repository's hook id</param>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/#delete-a-hook">API documentation</a> for more information.</remarks>
        public IObservable<Unit> Delete(long repositoryId, int hookId)
        {
            return _client.Delete(repositoryId, hookId).ToObservable();
        }
    }
}