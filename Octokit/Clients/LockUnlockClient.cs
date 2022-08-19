using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Client to manage locking/unlocking a conversation for an Issue or a Pull request
    /// </summary>
    public class LockUnlockClient : ApiClient, ILockUnlockClient
    {
        /// <summary>
        /// Instantiates a new GitHub Issue Lock API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public LockUnlockClient(IApiConnection apiConnection) : base(apiConnection)
        {

        }
        /// <summary>
        /// Locks an issue for the specified repository. Issue owners and users with push access can lock an issue or pull request's conversation.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#lock-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="lockReason">The reason for locking the issue</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/issues/{issue_number}/lock")]
        public Task Lock(string owner, string name, int number, LockReason? lockReason = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Put<Issue>(ApiUrls.IssueLock(owner, name, number), lockReason.HasValue ? new { LockReason = lockReason } : new object());
        }

        /// <summary>
        /// Locks an issue for the specified repository. Issue owners and users with push access can lock an issue or pull request's conversation.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#lock-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="lockReason">The reason for locking the issue</param>
        [ManualRoute("PUT", "/repositories/{id}/issues/{number}/lock")]
        public Task Lock(long repositoryId, int number, LockReason? lockReason = null)
        {
            return ApiConnection.Put<Issue>(ApiUrls.IssueLock(repositoryId, number), lockReason.HasValue ? new { LockReaons = lockReason } : new object());
        }

        /// <summary>
        /// Unlocks an issue for the specified repository. Issue owners and users with push access can unlock an issue or pull request's conversation.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#unlock-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/issues/{issue_number}/lock")]
        public Task Unlock(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.IssueLock(owner, name, number));
        }

        /// <summary>
        /// Unlocks an issue for the specified repository. Issue owners and users with push access can unlock an issue or pull request's conversation.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#unlock-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        [ManualRoute("DELETE", "/repositories/{id}/issues/{number}/lock")]
        public Task Unlock(long repositoryId, int number)
        {
            return ApiConnection.Delete(ApiUrls.IssueLock(repositoryId, number));
        }
    }
}
