using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Code of Conduct API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/codes_of_conduct/">API documentation</a> for more information.
    /// </remarks>
    public class CodesOfConductClient : ApiClient, ICodesOfConductClient
    {
        /// <summary>
        /// Instantiates a new GitHub Code of Conduct API client.
        /// </summary>
        /// <param name="apiConnection">an API connection</param>
        public CodesOfConductClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets all code of conducts on GitHub.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#list-all-codes-of-conduct">API documentation</a> for more information.</remarks>
        /// <returns>A <see cref="IReadOnlyList{CodeOfConduct}"/> on GitHub.</returns>
        [Preview("scarlet-witch")]
        [ManualRoute("GET", "/codes_of_conduct")]
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 14/04/2020)")]
        public Task<IReadOnlyList<CodeOfConduct>> GetAll()
        {
            return ApiConnection.GetAll<CodeOfConduct>(ApiUrls.CodesOfConduct(), null, AcceptHeaders.CodesOfConductPreview);
        }

        /// <summary>
        /// Gets an individual code of conduct.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#get-an-individual-code-of-conduct">API documentation</a> for more information.</remarks>
        /// <param name="key">The unique key for the Code of Conduct</param>
        /// <returns>A <see cref="CodeOfConduct"/> that includes the code of conduct key, name, and API/HTML URL.</returns>
        [Preview("scarlet-witch")]
        [ManualRoute("GET", "/codes_of_conduct/{key}")]
        public Task<CodeOfConduct> Get(CodeOfConductType key)
        {
            return ApiConnection.Get<CodeOfConduct>(ApiUrls.CodeOfConduct(key), null, AcceptHeaders.CodesOfConductPreview);
        }

        /// <summary>
        /// Gets the code of conduct for a repository, if one is detected.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/codes_of_conduct/#get-the-contents-of-a-repositorys-code-of-conduct">API documentation</a> for more information.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The <see cref="CodeOfConduct"/> that the repository uses, if one is detected.</returns>
        [Preview("scarlet-witch")]
        [ManualRoute("GET", "/repos/{owner}/{repo}/community/code_of_conduct")]
        public Task<CodeOfConduct> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<CodeOfConduct>(ApiUrls.CodeOfConduct(owner, name), null, AcceptHeaders.CodesOfConductPreview);
        }
    }
}
