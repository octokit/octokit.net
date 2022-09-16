using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's gitignore APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/gitignore">GitIgnore API documentation</a> for more details.
    /// </remarks>
    public class GitIgnoreClient : ApiClient, IGitIgnoreClient
    {
        /// <summary>
        ///     Initializes a new GitHub gitignore API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public GitIgnoreClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// List all templates available to pass as an option when creating a repository.
        /// </summary>
        /// <returns>A list of template names</returns>
        [ManualRoute("GET", "/gitignore/templates")]
        public Task<IReadOnlyList<string>> GetAllGitIgnoreTemplates()
        {
            return ApiConnection.GetAll<string>(ApiUrls.GitIgnoreTemplates());
        }

        /// <summary>
        /// Retrieves the source for a single GitIgnore template
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns>A template and its source</returns>
        [ManualRoute("GET", "/gitignore/templates/{name}")]
        public Task<GitIgnoreTemplate> GetGitIgnoreTemplate(string templateName)
        {
            Ensure.ArgumentNotNullOrEmptyString(templateName, nameof(templateName));

            return ApiConnection.Get<GitIgnoreTemplate>(ApiUrls.GitIgnoreTemplates(templateName));
        }
    }
}