using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class IssuesLabelsClient : ApiClient, IIssuesLabelsClient
    {
        public IssuesLabelsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/:name
        /// </remarks>
        /// <returns></returns>
        public Task<IReadOnlyList<Label>> GetForRepository(string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            return ApiConnection.GetAll<Label>(ApiUrls.Labels(owner, repo));
        }

        /// <summary>
        /// Gets a single Label by name.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/:name
        /// </remarks>
        /// <returns></returns>
        public Task<Label> Get(string owner, string repo, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<Label>(ApiUrls.Label(owner, repo, name));
        }

        /// <summary>
        /// Creates a label.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/
        /// </remarks>
        /// <returns></returns>
        public Task<Label> Create(string owner, string repo, NewLabel newLabel)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNull(newLabel, "newLabel");

            return ApiConnection.Post<Label>(ApiUrls.Labels(owner, repo), newLabel);
        }

        /// <summary>
        /// Updates a label.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/:label
        /// </remarks>
        /// <returns></returns>
        public Task<Label> Create(string owner, string repo, string name, LabelUpdate labelUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(labelUpdate, "labelUpdate");

            return ApiConnection.Post<Label>(ApiUrls.Label(owner, repo, name), labelUpdate);
        }

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/
        /// </remarks>
        /// <returns></returns>
        public Task Delete(string owner, string repo, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.Label(owner, repo, name));
        }
    }
}
