using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Clients
{
    /// <summary>
    /// A client for GitHub's Codespaces API.
    /// Gets and creates Codespaces.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/codespaces/codespaces?apiVersion=2022-11-28">Codespaces API documentation</a> for more information.
    /// </remarks>
    internal class CodespacesClient : ApiClient, ICodespacesClient
    {
        /// <summary>
        /// Instantiates a new GitHub Codespaces API client.
        /// </summary>
        /// <param name="apiConnection"></param>
        public CodespacesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns all the codespaces for the authenticated user.
        /// </summary>
        /// <returns>A codespaces collection</returns>
        [ManualRoute("GET", "/user/codespaces")]
        public Task<CodespacesCollection> GetAll()
        {
            return ApiConnection.Get<CodespacesCollection>(ApiUrls.Codespaces());
        }

        /// <summary>
        /// Returns all the codespaces for the specified repository.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repo"></param>
        /// <returns>A codespaces collection</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/codespaces")]
        public Task<CodespacesCollection> GetForRepository(string owner, string repo)
        {
            return ApiConnection.Get<CodespacesCollection>(ApiUrls.CodespacesForRepository(owner, repo));
        }

        /// <summary>
        /// Gets a codespace for the authenticated user.
        /// </summary>
        /// <param name="codespaceName"></param>
        /// <returns>A codespace</returns>
        [ManualRoute("GET", "/user/codespaces/{codespace_name}")]
        public Task<Codespace> Get(string codespaceName)
        {
            return ApiConnection.Get<Codespace>(ApiUrls.Codespace(codespaceName));
        }

        /// <summary>
        /// Starts a codespace for the authenticated user.
        /// </summary>
        /// <param name="codespaceName"></param>
        /// <returns></returns>
        [ManualRoute("POST", "/user/codespaces/{codespace_name}/start")]
        public Task<Codespace> Start(string codespaceName)
        {
            return ApiConnection.Post<Codespace>(ApiUrls.CodespaceStart(codespaceName));
        }

        /// <summary>
        /// Stops a codespace for the authenticated user.
        /// </summary>
        /// <param name="codespaceName"></param>
        /// <returns></returns>
        [ManualRoute("POST", "/user/codespaces/{codespace_name}/stop")]
        public Task<Codespace> Stop(string codespaceName)
        {
            return ApiConnection.Post<Codespace>(ApiUrls.CodespaceStop(codespaceName));
        }
    }
}
