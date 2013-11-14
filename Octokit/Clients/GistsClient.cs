using System.Threading.Tasks;

namespace Octokit
{
    public class GistsClient : ApiClient, IGistsClient
    {
        public GistsClient(IApiConnection apiConnection) : 
            base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#get-a-single-gist
        /// </remarks>
        /// <param name="id">The id of the gist</param>
        public Task<Gist> Get(int id)
        {
            return ApiConnection.Get<Gist>(ApiUrls.Gist(id));
        }
    }
}