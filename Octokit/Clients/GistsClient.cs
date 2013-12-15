using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Gists API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/gists/">Gists API documentation</a> for more information.
    /// </remarks>
    public class GistsClient : ApiClient, IGistsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Gists API client.
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

        /// <summary>
        /// Gets the list of all gists for the provided <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <param name="user">The user the gists of whom are returned</param>
        /// <returns>A list with the gists</returns>
        public Task<IReadOnlyList<Gist>> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.GetAll<Gist>(ApiUrls.Gists(user));
        }

        /// <summary>
        /// Gets the list of all gists for the authenticated user.
        /// If the user is not authenticated returns all public gists
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#list-gists
        /// </remarks>
        /// <returns>A list with the gists</returns>
        public Task<IReadOnlyList<Gist>> GetAllForCurrent()
        {
            return ApiConnection.GetAll<Gist>(ApiUrls.Gists());
        }

    }
}