using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

using System.Threading;

namespace Octokit.Reactive
{
    public class ObservableGistsClient : IObservableGistsClient
    {
        readonly IGistsClient _client;
        readonly IConnection _connection;

        public ObservableGistsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Gist;
            _connection = client.Connection;
            Comment = new ObservableGistCommentsClient(client);
        }

        public IObservableGistCommentsClient Comment { get; set; }

        /// <summary>
        /// Gets a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#get-a-single-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<Gist> Get(string id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));

            return _client.Get(id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#create-a-gist
        /// </remarks>
        /// <param name="newGist">The new gist to create</param>
        public IObservable<Gist> Create(NewGist newGist, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newGist, nameof(newGist));

            return _client.Create(newGist, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a fork of a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#fork-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist to fork</param>
        public IObservable<Gist> Fork(string id, CancellationToken cancellationToken = default)
        {
            return _client.Fork(id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<Unit> Delete(string id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(id, nameof(id));

            return _client.Delete(id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        public IObservable<Gist> GetAll(CancellationToken cancellationToken = default)
        {
            return GetAll(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAll(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.Gist(), options, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        public IObservable<Gist> GetAll(DateTimeOffset since, CancellationToken cancellationToken = default)
        {
            return GetAll(since, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAll(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.Gist(), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        public IObservable<Gist> GetAllPublic(CancellationToken cancellationToken = default)
        {
            return GetAllPublic(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllPublic(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.PublicGists(), options, cancellationToken);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        public IObservable<Gist> GetAllPublic(DateTimeOffset since, CancellationToken cancellationToken = default)
        {
            return GetAllPublic(since, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllPublic(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.PublicGists(), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        public IObservable<Gist> GetAllStarred(CancellationToken cancellationToken = default)
        {
            return GetAllStarred(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllStarred(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.StarredGists(), options, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        public IObservable<Gist> GetAllStarred(DateTimeOffset since, CancellationToken cancellationToken = default)
        {
            return GetAllStarred(since, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllStarred(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.StarredGists(), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        public IObservable<Gist> GetAllForUser(string user, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllForUser(string user, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.UsersGists(user), options, cancellationToken);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        public IObservable<Gist> GetAllForUser(string user, DateTimeOffset since, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, since, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllForUser(string user, DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.UsersGists(user), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// List gist commits
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-commits
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<GistHistory> GetAllCommits(string id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));

            return GetAllCommits(id, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List gist commits
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-commits
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GistHistory> GetAllCommits(string id, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<GistHistory>(ApiUrls.GistCommits(id), options, cancellationToken);
        }

        /// <summary>
        /// List gist forks
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-forks
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<GistFork> GetAllForks(string id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));

            return GetAllForks(id, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List gist forks
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-forks
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GistFork> GetAllForks(string id, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<GistFork>(ApiUrls.ForkGist(id), options, cancellationToken);
        }

        /// <summary>
        /// Edits a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="gistUpdate">The update to the gist</param>
        public IObservable<Gist> Edit(string id, GistUpdate gistUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(id, nameof(id));
            Ensure.ArgumentNotNull(gistUpdate, nameof(gistUpdate));

            return _client.Edit(id, gistUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Stars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#star-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<Unit> Star(string id, CancellationToken cancellationToken = default)
        {
            return _client.Star(id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Unstars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#unstar-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<Unit> Unstar(string id, CancellationToken cancellationToken = default)
        {
            return _client.Unstar(id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Checks if the gist is starred
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#check-if-a-gist-is-starred
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<bool> IsStarred(string id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));

            return _client.IsStarred(id, cancellationToken).ToObservable();
        }
    }
}
