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

            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<GistComment>> GetForGist(string gistId)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");

            throw new System.NotImplementedException();
        }

        public Task<GistComment> Create(string gistId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            throw new System.NotImplementedException();
        }

        public Task<GistComment> Update(string gistId, string commentId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(commentId, "commentId");
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            throw new System.NotImplementedException();
        }

        public Task Delete(string gistId, string commentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(gistId, "gistId");
            Ensure.ArgumentNotNullOrEmptyString(commentId, "commentId");

            throw new System.NotImplementedException();
        }
    }
}