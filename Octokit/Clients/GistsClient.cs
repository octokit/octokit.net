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
        /// Creates a new gist
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/gists/#create-a-gist
        /// </remarks>
        /// <param name="newGist">The new gist to create</param>
        public Task<Gist> Create(NewGist newGist)
        {
            Ensure.ArgumentNotNull(newGist, "newGist");

            //Required to create anonymous object to match signature of files hash.  Allowing the serializer
            //to handle Dictionary<string,NewGistFile> will fail to match.
            var filesAsJsonObject = new JsonObject();
            foreach(var kvp in newGist.Files)
            {
                filesAsJsonObject.Add(new KeyValuePair<string, object>(kvp.Key, kvp.Value));
            }
            var gist = new { Description = newGist.Description, Public = newGist.Public, Files = filesAsJsonObject };

            return ApiConnection.Post<Gist>(ApiUrls.Gist(), gist);
        }
    }
}