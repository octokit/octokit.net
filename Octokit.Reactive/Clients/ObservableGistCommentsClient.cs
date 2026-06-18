using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableGistCommentsClient : IObservableGistCommentsClient
    {
        readonly IGistCommentsClient _client;
        readonly IConnection _connection;

        public ObservableGistCommentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Gist.Comment;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets a single comment by gist- and comment id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#get-a-single-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        /// <returns>IObservable{GistComment}.</returns>
        public IObservable<GistComment> Get(string gistId, long commentId, CancellationToken cancellationToken = default)
        {
            return _client.Get(gistId, commentId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets all comments for the gist with the specified id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#list-comments-on-a-gist</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <returns>IObservable{GistComment}.</returns>
        public IObservable<GistComment> GetAllForGist(string gistId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));

            return GetAllForGist(gistId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all comments for the gist with the specified id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#list-comments-on-a-gist</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>IObservable{GistComment}.</returns>
        public IObservable<GistComment> GetAllForGist(string gistId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, nameof(gistId));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<GistComment>(ApiUrls.GistComments(gistId), options, cancellationToken);
        }

        /// <summary>
        /// Creates a comment for the gist with the specified id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#create-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="comment">The body of the comment</param>
        /// <returns>IObservable{GistComment}.</returns>
        public IObservable<GistComment> Create(string gistId, string comment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(comment, nameof(comment));

            return _client.Create(gistId, comment, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates the comment with the specified gist- and comment id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#edit-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        /// <param name="comment">The updated body of the comment</param>
        /// <returns>IObservable{GistComment}.</returns>
        public IObservable<GistComment> Update(string gistId, long commentId, string comment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(comment, nameof(comment));

            return _client.Update(gistId, commentId, comment, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes the comment with the specified gist- and comment id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#delete-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        /// <returns>IObservable{Unit}.</returns>
        public IObservable<Unit> Delete(string gistId, long commentId, CancellationToken cancellationToken = default)
        {
            return _client.Delete(gistId, commentId, cancellationToken).ToObservable();
        }
    }
}
