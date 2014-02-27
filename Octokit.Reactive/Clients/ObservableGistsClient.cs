using System;
using System.Reactive.Threading.Tasks;
using System.Reactive;
using System.Net;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableGistsClient : IObservableGistsClient 
    {
        readonly IGistsClient _client;
        readonly IConnection _connection;

        public ObservableGistsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

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
            Ensure.ArgumentNotNullOrEmptyString(id, "id");

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
            Ensure.ArgumentNotNull(newGist, "newGist");

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
            Ensure.ArgumentNotNull(id, "id");

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
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.Gist());
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
            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.Gist(), request.ToParametersDictionary());
        }

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        public IObservable<Gist> GetAllPublic()
        {
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.PublicGists());
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
            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.PublicGists(), request.ToParametersDictionary());
        }

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        public IObservable<Gist> GetAllStarred()
        {
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.StarredGists());
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
            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.StarredGists(), request.ToParametersDictionary());
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
            Ensure.ArgumentNotNull(user, "user");

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.UsersGists(user));
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
            Ensure.ArgumentNotNull(user, "user");

            var request = new GistRequest(since);
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.UsersGists(user), request.ToParametersDictionary());
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
            Ensure.ArgumentNotNull(id, "id");
            Ensure.ArgumentNotNull(gistUpdate, "gistUpdate");

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
            Ensure.ArgumentNotNullOrEmptyString(id, "id");

            return _client.IsStarred(id).ToObservable();
        }
    }
}