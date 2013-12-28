using System;
using System.Diagnostics.CodeAnalysis;
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
        /// <returns>IObservable{Gist}.</returns>
        public IObservable<Gist> Get(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, "id");

            return _client.Get(id).ToObservable();
        }
        
        /// <summary>
        /// Gets the list of all gists for the provided <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user the gists of whom are returned</param>
        /// <returns>IObservable{Gist}</returns>
        public IObservable<Gist> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.Gists(user));
        }

        /// <summary>
        /// Gets the list of all gists for the authenticated user.
        /// If the user is not authenticated returns all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>IObservable{Gist}</returns>
        public IObservable<Gist> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.Gists());
        }

        /// <summary>
        /// Gets the list of all starred gists for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>IObservable{Gist}</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IObservable<Gist> GetStarredForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.GistsStarred());
        }

        /// <summary>
        /// Get the list of all public gists.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>IObservable{Gist}</returns>
        public IObservable<Gist> GetPublic()
        {
            return _connection.GetAndFlattenAllPages<Gist>(ApiUrls.GistsPublic());
        }

        /// <summary>
        /// Stars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#star-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <returns>IObservable{Unit}</returns>
        public IObservable<Unit> Star(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, "id");

            return _client.Star(id).ToObservable();
        }

        /// <summary>
        /// Unstars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#unstar-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <returns>IObservable{Unit}</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unstar",
            Justification = "This is how it's called in GitHub API")]
        public IObservable<Unit> Unstar(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, "id");

            return _client.Unstar(id).ToObservable();
        }

        /// <summary>
        /// Deletes a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <returns>IObservable{Unit}</returns>
        public IObservable<Unit> Delete(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, "id");

            return _client.Delete(id).ToObservable();
        }
    }
}