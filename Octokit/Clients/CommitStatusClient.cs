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
        /// <remarks>
        /// https://developer.github.com/v3/repos/statuses/#list-statuses-for-a-specific-ref
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{ref}/statuses")]
        public Task<IReadOnlyList<CommitStatus>> GetAll(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAll(owner, name, reference, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/repos/statuses/#list-statuses-for-a-specific-ref
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{ref}/statuses")]
        public Task<IReadOnlyList<CommitStatus>> GetAll(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAll(repositoryId, reference, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/repos/statuses/#list-statuses-for-a-specific-ref
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{ref}/statuses")]
        public Task<IReadOnlyList<CommitStatus>> GetAll(string owner, string name, string reference, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CommitStatus>(ApiUrls.CommitStatuses(owner, name, reference), options);
        }

        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/repos/statuses/#list-statuses-for-a-specific-ref
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{ref}/statuses")]
        public Task<IReadOnlyList<CommitStatus>> GetAll(long repositoryId, string reference, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CommitStatus>(ApiUrls.CommitStatuses(repositoryId, reference), options);
        }

        /// <summary>
        /// Retrieves a combined view of statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/repos/statuses/#get-the-combined-status-for-a-specific-ref
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{ref}/status")]
        public Task<CombinedCommitStatus> GetCombined(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<CombinedCommitStatus>(ApiUrls.CombinedCommitStatus(owner, name, reference));
        }

        /// <summary>
        /// Retrieves a combined view of statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/repos/statuses/#get-the-combined-status-for-a-specific-ref
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{ref}/status")]
        public Task<CombinedCommitStatus> GetCombined(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<CombinedCommitStatus>(ApiUrls.CombinedCommitStatus(repositoryId, reference));
        }

        /// <summary>
        /// Creates a commit status for the specified ref.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/repos/statuses/#create-a-status
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="newCommitStatus">The commit status to create</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/statuses/{sha}")]
        public Task<CommitStatus> Create(string owner, string name, string reference, NewCommitStatus newCommitStatus)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(newCommitStatus, nameof(newCommitStatus));

            return ApiConnection.Post<CommitStatus>(ApiUrls.CreateCommitStatus(owner, name, reference), newCommitStatus);
        }

        /// <summary>
        /// Creates a commit status for the specified ref.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/repos/statuses/#create-a-status
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="newCommitStatus">The commit status to create</param>
        [ManualRoute("POST", "/repositories/{id}/statuses/{sha}")]
        public Task<CommitStatus> Create(long repositoryId, string reference, NewCommitStatus newCommitStatus)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(newCommitStatus, nameof(newCommitStatus));

            return ApiConnection.Post<CommitStatus>(ApiUrls.CreateCommitStatus(repositoryId, reference), newCommitStatus);
        }
    }
}
