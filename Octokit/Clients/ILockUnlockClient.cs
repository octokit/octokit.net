using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Client to manage locking/unlocking a conversation for an Issue or a Pull request
    /// </summary>
    public interface ILockUnlockClient
    {
        /// <summary>
        /// Locks an issue for the specified repository. Issue owners and users with push access can lock an issue or pull request's conversation.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#lock-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="lockReason">The reason for locking the issue</param>
        Task Lock(string owner, string name, int number, LockReason? lockReason = null);

        /// <summary>
        /// Locks an issue for the specified repository. Issue owners and users with push access can lock an issue or pull request's conversation.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#lock-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="lockReason">The reason for locking the issue</param>
        Task Lock(long repositoryId, int number, LockReason? lockReason = null);

        /// <summary>
        /// Unlocks an issue for the specified repository. Issue owners and users with push access can unlock an issue or pull request's conversation.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#unlock-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        Task Unlock(string owner, string name, int number);

        /// <summary>
        /// Unlocks an issue for the specified repository. Issue owners and users with push access can unlock an issue or pull request's conversation.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#unlock-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        Task Unlock(long repositoryId, int number);

    }
}
