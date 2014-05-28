using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Repository Status API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/statuses/">Repository Statuses API documentation</a> for more information.
    /// </remarks>
    public class CommitStatusClient : ApiClient, ICommitStatusClient
    {
        /// <summary>
        /// Initializes a new Commit Status API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public CommitStatusClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>Only users with pull access can see this.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns></returns>
        public Task<IReadOnlyList<CommitStatus>> GetAll(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            // This is currently a preview feature of the API and is subject to change
            // https://developer.github.com/v3/repos/statuses/#list-statuses-for-a-specific-ref
            return ApiConnection.GetAll<CommitStatus>(
                ApiUrls.CommitStatus(owner, name, reference),
                null,
                "application/vnd.github.she-hulk-preview+json");
        }

        /// <summary>
        /// Creates a commit status for the specified ref.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="commitStatus">The commit status to create</param>
        /// <returns></returns>
        public Task<CommitStatus> Create(string owner, string name, string reference, NewCommitStatus commitStatus)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");
            Ensure.ArgumentNotNull(commitStatus, "commitStatus");

            // This is currently a preview feature of the API and is subject to change
            // https://developer.github.com/v3/repos/statuses/#list-statuses-for-a-specific-ref
            return ApiConnection.Post<CommitStatus>(
                ApiUrls.CommitStatus(owner, name, reference),
                commitStatus,
                "application/vnd.github.she-hulk-preview+json");
        }
    }
}
