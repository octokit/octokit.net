using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;


namespace Octokit.Reactive
{
    /// <inheritdoc/>
    public class ObservableAutolinksClient : IObservableAutolinksClient
    {
        readonly IAutolinksClient _client;
        readonly IConnection _connection;


        public ObservableAutolinksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Autolinks;
            _connection = client.Connection;
        }


        /// <inheritdoc/>
        public IObservable<Autolink> Get(string owner, string repo, int autolinkId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return _client.Get(owner, repo, autolinkId).ToObservable();
        }

        /// <inheritdoc/>
        public IObservable<Autolink> GetAll(string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return this.GetAll(owner, repo, ApiOptions.None);
        }

        /// <inheritdoc/>
        public IObservable<Autolink> GetAll(string owner, string repo, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Autolink>(ApiUrls.AutolinksGetAll(owner, repo), options);
        }

        /// <inheritdoc/>
        public IObservable<Autolink> Create(string owner, string repo, AutolinkRequest autolink)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));
            Ensure.ArgumentNotNull(autolink, nameof(autolink));

            return _client.Create(owner, repo, autolink).ToObservable();
        }

        /// <inheritdoc/>
        public IObservable<Unit> Delete(string owner, string repo, int autolinkId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return _client.Delete(owner, repo, autolinkId).ToObservable();
        }
    }
}
