using System.Collections.Generic;
using System.Threading.Tasks;


namespace Octokit
{
    /// <inheritdoc/>
    public class AutolinksClient : ApiClient, IAutolinksClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Autolinks API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public AutolinksClient(IApiConnection apiConnection) : base(apiConnection)
        { }


        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/autolinks/{autolinkId}")]
        public Task<Autolink> Get(string owner, string repo, int autolinkId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return ApiConnection.Get<Autolink>(ApiUrls.AutolinksGet(owner, repo, autolinkId));
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/autolinks")]
        public Task<IReadOnlyList<Autolink>> GetAll(string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return GetAll(owner, repo, ApiOptions.None);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/autolinks")]
        public Task<IReadOnlyList<Autolink>> GetAll(string owner, string repo, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Autolink>(ApiUrls.AutolinksGetAll(owner, repo), options);
        }

        /// <inheritdoc/>
        [ManualRoute("POST", "/repos/{owner}/{repo}/autolinks")]
        public Task<Autolink> Create(string owner, string repo, AutolinkRequest autolink)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));
            Ensure.ArgumentNotNull(autolink, nameof(autolink));

            return ApiConnection.Post<Autolink>(ApiUrls.AutolinksCreate(owner, repo), autolink);
        }

        /// <inheritdoc/>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/autolinks/{autolinkId}")]
        public Task Delete(string owner, string repo, int autolinkId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return ApiConnection.Delete(ApiUrls.AutolinksDelete(owner, repo, autolinkId));
        }
    }
}
