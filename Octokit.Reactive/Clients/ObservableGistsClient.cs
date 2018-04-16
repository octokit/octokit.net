using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

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
        public IObservable<Gist> Get(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));

            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Creates a new gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#create-a-gist
        /// </remarks>
        /// <param name="newGist">The new gist to create</param>
        public IObservable<Gist> Create(NewGist newGist)
        {
            Ensure.ArgumentNotNull(newGist, nameof(newGist));

            return _client.Create(newGist).ToObservable();
        }

        /// <summary>
        /// Creates a fork of a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#fork-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist to fork</param>
        public IObservable<Gist> Fork(string id)
        {
            return _client.Fork(id).ToObservable();
        }

        /// <summary>
        /// Deletes a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<Unit> Delete(string id)
        {
            Ensure.ArgumentNotNull(id, nameof(id));

            return _client.Delete(id).ToObservable();
        }

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        public IObservable<Gist> GetAll()
        {
            return GetAll(ApiOptions.None);
        }

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.Gist(), options);
        }

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        public IObservable<Gist> GetAll(DateTimeOffset since)
        {
            return GetAll(since, ApiOptions.None);
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
        public IObservable<Gist> GetAll(DateTimeOffset since, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.Gist(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        public IObservable<Gist> GetAllPublic()
        {
            return GetAllPublic(ApiOptions.None);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllPublic(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.PublicGists(), options);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        public IObservable<Gist> GetAllPublic(DateTimeOffset since)
        {
            return GetAllPublic(since, ApiOptions.None);
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllPublic(DateTimeOffset since, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.PublicGists(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        public IObservable<Gist> GetAllStarred()
        {
            return GetAllStarred(ApiOptions.None);
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllStarred(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.StarredGists(), options);
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        public IObservable<Gist> GetAllStarred(DateTimeOffset since)
        {
            return GetAllStarred(since, ApiOptions.None);
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllStarred(DateTimeOffset since, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.StarredGists(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        public IObservable<Gist> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, ApiOptions.None);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Gist> GetAllForUser(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.UsersGists(user), options);
        }

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        public IObservable<Gist> GetAllForUser(string user, DateTimeOffset since)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, since, ApiOptions.None);
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
        public IObservable<Gist> GetAllForUser(string user, DateTimeOffset since, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.UsersGists(user), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// List gist commits
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-commits
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<GistHistory> GetAllCommits(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));

            return GetAllCommits(id, ApiOptions.None);
        }

        /// <summary>
        /// List gist commits
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-commits
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GistHistory> GetAllCommits(string id, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<GistHistory>(ApiUrls.GistCommits(id), options);
        }

        /// <summary>
        /// List gist forks
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-forks
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<GistFork> GetAllForks(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));

            return GetAllForks(id, ApiOptions.None);
        }

        /// <summary>
        /// List gist forks
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-forks
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GistFork> GetAllForks(string id, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<GistFork>(ApiUrls.ForkGist(id), options);
        }

        /// <summary>
        /// Edits a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="gistUpdate">The update to the gist</param>
        public IObservable<Gist> Edit(string id, GistUpdate gistUpdate)
        {
            Ensure.ArgumentNotNull(id, nameof(id));
            Ensure.ArgumentNotNull(gistUpdate, nameof(gistUpdate));

            return _client.Edit(id, gistUpdate).ToObservable();
        }

        /// <summary>
        /// Stars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#star-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<Unit> Star(string id)
        {
            return _client.Star(id).ToObservable();
        }

        /// <summary>
        /// Unstars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#unstar-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<Unit> Unstar(string id)
        {
            return _client.Unstar(id).ToObservable();
        }

        /// <summary>
        /// Checks if the gist is starred
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#check-if-a-gist-is-starred
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public IObservable<bool> IsStarred(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, nameof(id));

            return _client.IsStarred(id).ToObservable();
        }
    }
}