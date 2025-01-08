using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Codespaces API.
    /// Gets and creates Codespaces.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/codespaces/codespaces?apiVersion=2022-11-28">Codespaces API documentation</a> for more information.
    /// </remarks>
    public class CodespacesClient : ApiClient, ICodespacesClient
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

        /// <summary>
        /// Returns available machines for the specified repository.
        /// </summary>
        [ManualRoute("GET", "/repos/{repoOwner}/{repoName}/machines")]
        public Task<MachinesCollection> GetAvailableMachinesForRepo(string repoOwner, string repoName, string reference = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(repoOwner, nameof(repoOwner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            return ApiConnection.Get<MachinesCollection>(ApiUrls.GetAvailableMachinesForRepo(repoOwner, repoName, reference));
        }

        /// <summary>
        /// Creates a new codespace for the authenticated user.
        /// </summary>
        [ManualRoute("POST", "/repos/{owner}/{repo}/codespaces")]
        public Task<Codespace> Create(string owner, string repo, NewCodespace newCodespace)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return ApiConnection.Post<Codespace>(ApiUrls.CreateCodespace(owner, repo), newCodespace);
        }
    }
}
