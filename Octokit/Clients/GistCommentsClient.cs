using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class GistCommentsClient : ApiClient, IGistCommentsClient
    {
        public GistCommentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single comment by gist- and comment id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#get-a-single-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        /// <returns>Task{GistComment}.</returns>
        public Task<GistComment> Get(string gistId, string commentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(commentId, "commentId");

            return ApiConnection.Get<GistComment>(ApiUrls.GistComment(gistId, commentId));
        }

        /// <summary>
        /// Gets all comments for the gist with the specified id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#list-comments-on-a-gist</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <returns>Task{IReadOnlyList{GistComment}}.</returns>
        public Task<IReadOnlyList<GistComment>> GetForGist(string gistId)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");

            return ApiConnection.GetAll<GistComment>(ApiUrls.GistComments(gistId));
        }

        /// <summary>
        /// Creates a comment for the gist with the specified id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#create-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="comment">The body of the comment</param>
        /// <returns>Task{GistComment}.</returns>
        public Task<GistComment> Create(string gistId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            return ApiConnection.Post<GistComment>(ApiUrls.GistComments(gistId), comment);
        }

        /// <summary>
        /// Updates the comment with the specified gist- and comment id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#edit-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        /// <param name="comment">The updated body of the comment</param>
        /// <returns>Task{GistComment}.</returns>
        public Task<GistComment> Update(string gistId, string commentId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(commentId, "commentId");
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            return ApiConnection.Patch<GistComment>(ApiUrls.GistComment(gistId, commentId), comment);
        }

        /// <summary>
        /// Deletes the comment with the specified gist- and comment id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/gists/comments/#delete-a-comment</remarks>
        /// <param name="gistId">The id of the gist</param>
        /// <param name="commentId">The id of the comment</param>
        /// <returns>Task.</returns>
        public Task Delete(string gistId, string commentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(commentId, "commentId");

            return ApiConnection.Delete(ApiUrls.GistComment(gistId, commentId));
        }
    }
}