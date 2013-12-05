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
        /// Gets all  labels for the issue.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-labels-on-an-issue">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <returns>The list of labels</returns>
        public Task<IReadOnlyList<Label>> GetForIssue(string owner, string repo, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            return ApiConnection.GetAll<Label>(ApiUrls.IssueLabels(owner, repo, number));
        }

        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#list-all-labels-for-this-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <returns>The list of labels</returns>
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
        /// See the <a href="http://developer.github.com/v3/issues/labels/#get-a-single-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="name">The name of the label</param>
        /// <returns>The label</returns>
        public Task<Label> Get(string owner, string repo, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<Label>(ApiUrls.Label(owner, repo, name));
        }

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#delete-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="name">The name of the label</param>
        /// <returns></returns>
        public Task Delete(string owner, string repo, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.Label(owner, repo, name));
        }

        /// <summary>
        /// Creates a label.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/issues/labels/#create-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="newLabel">The data for the label to be created</param>
        /// <returns>The created label</returns>
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
        /// See the <a href="http://developer.github.com/v3/issues/labels/#update-a-label">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="name">The name of the label</param>
        /// <param name="labelUpdate">The data for the label to be updated</param>
        /// <returns>The updated label</returns>
        public Task<Label> Update(string owner, string repo, string name, LabelUpdate labelUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(labelUpdate, "labelUpdate");

            return ApiConnection.Post<Label>(ApiUrls.Label(owner, repo, name), labelUpdate);
        }
    }
}
