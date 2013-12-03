using System.Threading.Tasks;

namespace Octokit
{
    public class GistsClient : ApiClient, IGistsClient
    {
        /// <summary>
        /// Instatiates a new GitHub Gists API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public GistsClient(IApiConnection apiConnection) : 
            base(apiConnection)
        {
            Comment = new GistCommentsClient(apiConnection);
        }

        public IGistCommentsClient Comment { get; set; }

        /// <summary>
        /// Gets a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#get-a-single-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public Task<Gist> Get(string id)
        {
            return ApiConnection.Get<Gist>(ApiUrls.Gist(id));
        }
    }
}