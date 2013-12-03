using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repositories API for Contents.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/contents/">Repositories API documentation</a> for more details.
    /// </remarks>
    public class RepositoryContentsClient : ApiClient, IRepositoryContentsClient
    {
        /// <summary>
        /// Initializes a new GitHub Repos API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public RepositoryContentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            
        }


        /// <summary>
        /// Gets the Content of a specified file or folder from the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file to fetch from the repository</param>
        /// <returns></returns>
        public async Task<ContentsResponse> GetFile(string owner, string name, string path)
        {
            //Check we got all the parameters we need for the API call
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");

            // Example Path
            // /Razor/Navi/navi.cshtml
            // https://api.github.com/repos/warrenbuckley/Umbraco-Snippets/contents/Razor/Navi/navi.cshtml

            //repos/:owner/:repo/contents/:path
            var endpoint = "repos/{0}/{1}/contents/{2}".FormatUri(owner, name, path);
            var contents = await ApiConnection.Get<ContentsResponse>(endpoint, null).ConfigureAwait(false);

            return contents;
        }

        public async Task<IReadOnlyList<ContentsResponse>> GetContents(string owner, string name, string path)
        {
            //Check we got all the parameters we need for the API call
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(path, "path");

            // Example Path
            // /Razor/Navi/navi.cshtml
            // https://api.github.com/repos/warrenbuckley/Umbraco-Snippets/contents/Razor/Navi/navi.cshtml

            //repos/:owner/:repo/contents/:path
            var endpoint = "repos/{0}/{1}/contents/{2}".FormatUri(owner, name, path);
            var contents = await ApiConnection.GetAll<ContentsResponse>(endpoint, null).ConfigureAwait(false);

            return contents;
        }

        public async Task<IReadOnlyList<ContentsResponse>> GetContents(string owner, string name)
        {
                        //Check we got all the parameters we need for the API call
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            // Example Path
            // /Razor/Navi/navi.cshtml
            // https://api.github.com/repos/warrenbuckley/Umbraco-Snippets/contents/Razor/Navi/navi.cshtml

            //repos/:owner/:repo/contents/:path
            var endpoint = "repos/{0}/{1}/contents/".FormatUri(owner, name);
            var contents = await ApiConnection.GetAll<ContentsResponse>(endpoint, null).ConfigureAwait(false);

            return contents;
        }
    }
}
