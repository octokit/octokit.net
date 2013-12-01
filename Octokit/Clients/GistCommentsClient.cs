using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class GistCommentsClient : ApiClient, IGistCommentsClient
    {
        public GistCommentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<GistComment> Get(string gistId, string commentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(commentId, "commentId");

            return ApiConnection.Get<GistComment>(ApiUrls.GistComment(gistId, commentId));
        }

        public Task<IReadOnlyList<GistComment>> GetForGist(string gistId)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");

            return ApiConnection.GetAll<GistComment>(ApiUrls.GistComments(gistId));
        }

        public Task<GistComment> Create(string gistId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            return ApiConnection.Post<GistComment>(ApiUrls.GistComments(gistId), comment);
        }

        public Task<GistComment> Update(string gistId, string commentId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(commentId, "commentId");
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            return ApiConnection.Patch<GistComment>(ApiUrls.GistComment(gistId, commentId), comment);
        }

        public Task Delete(string gistId, string commentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(commentId, "commentId");

            return ApiConnection.Delete(ApiUrls.GistComment(gistId, commentId));
        }
    }
}