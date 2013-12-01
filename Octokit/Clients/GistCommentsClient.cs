using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class GistCommentsClient : ApiClient, IGistCommentsClient
    {
        public GistCommentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<GistComment> Get(int gistId, int commentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<GistComment>> GetForGist(int gistId)
        {
            throw new System.NotImplementedException();
        }

        public Task<GistComment> Create(int gistId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            throw new System.NotImplementedException();
        }

        public Task<GistComment> Update(int gistId, int commentId, string comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(comment, "comment");

            throw new System.NotImplementedException();
        }

        public Task Delete(int gistId, int commentId)
        {
            throw new System.NotImplementedException();
        }
    }
}