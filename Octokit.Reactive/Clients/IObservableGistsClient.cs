using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

using System.Threading;

namespace Octokit.Reactive
{
    public interface IObservableGistsClient
    {
        IObservableGistCommentsClient Comment { get; set; }

        /// <summary>
        /// Gets a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#get-a-single-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> Get(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAll(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAll(DateTimeOffset since, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s gists or if called anonymously, 
        /// this will return all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAll(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllPublic(CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllPublic(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllPublic(DateTimeOffset since, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllPublic(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllStarred(CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllStarred(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllStarred(DateTimeOffset since, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s starred gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllStarred(DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllForUser(string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllForUser(string user, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllForUser(string user, DateTimeOffset since, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a user's gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user</param>
        /// <param name="since">Only gists updated at or after this time are returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> GetAllForUser(string user, DateTimeOffset since, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List gist commits
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-commits
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<GistHistory> GetAllCommits(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// List gist commits
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-commits
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<GistHistory> GetAllCommits(string id, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List gist forks
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-forks
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<GistFork> GetAllForks(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// List gist forks
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists-forks
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<GistFork> GetAllForks(string id, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#create-a-gist
        /// </remarks>
        /// <param name="newGist">The new gist to create</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> Create(NewGist newGist, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a fork of a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#fork-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist to fork</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> Fork(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Edits a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="gistUpdate">The update to the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Gist> Edit(string id, GistUpdate gistUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#delete-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Unit> Delete(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Stars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#star-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Unit> Star(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unstars a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#unstar-a-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unstar")]
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Unit> Unstar(string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if the gist is starred
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#check-if-a-gist-is-starred
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> IsStarred(string id, CancellationToken cancellationToken = default);
    }
}
