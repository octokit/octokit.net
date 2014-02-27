using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableGistCommentsClient : IObservableGistCommentsClient
    {
        readonly IGistCommentsClient _client;
        readonly IConnection _connection;

        public ObservableGistCommentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

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
        public IObservable<GistComment> Get(int gistId, int commentId)
        {
            return _client.Get(gistId, commentId).ToObservable();
        }

        /// <summary>
        /// Gets all comments for the gist with the specified id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#list-comments-on-a-gist</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <returns>IObservable{GistComment}.</returns>
        public IObservable<GistComment> GetForGist(int gistId)
        {
            return _connection.GetAndFlattenAllPages<GistComment>(ApiUrls.GistComments(gistId));
        }

        /// <summary>
        /// Creates a comment for the gist with the specified id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#create-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="comment">The body of the comment</param>
        /// <returns>IObservable{GistComment}.</returns>
        public IObservable<GistComment> Create(int gistId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            return _client.Create(gistId, comment).ToObservable();
        }

        /// <summary>
        /// Updates the comment with the specified gist- and comment id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#edit-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        /// <param name="comment">The updated body of the comment</param>
        /// <returns>IObservable{GistComment}.</returns>
        public IObservable<GistComment> Update(int gistId, int commentId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            return _client.Update(gistId, commentId, comment).ToObservable();
        }

        /// <summary>
        /// Deletes the comment with the specified gist- and comment id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#delete-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        /// <returns>IObservable{Unit}.</returns>
        public IObservable<Unit> Delete(int gistId, int commentId)
        {
            return _client.Delete(gistId, commentId).ToObservable();
        }
    }
}