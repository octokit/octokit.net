using System;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// Client to manage locking/unlocking a conversation for an Issue or a Pull request
    /// </summary>
    public interface IObservableLockUnlockClient
    {
        /// <summary>
        /// Locks an issue for the specified repository. Issue owners and users with push access can lock an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#lock-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="lockReason">The reason for locking the issue</param>
        IObservable<Unit> Lock(string owner, string name, int issueNumber, LockReason? lockReason = null);

        /// <summary>
        /// Locks an issue for the specified repository. Issue owners and users with push access can lock an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#lock-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="lockReason">The reason for locking the issue</param>
        IObservable<Unit> Lock(long repositoryId, int issueNumber, LockReason? lockReason = null);

        /// <summary>
        /// Unlocks an issue for the specified repository. Issue owners and users with push access can unlock an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#unlock-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        IObservable<Unit> Unlock(string owner, string name, int issueNumber);

        /// <summary>
        /// Unlocks an issue for the specified repository. Issue owners and users with push access can unlock an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#unlock-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        IObservable<Unit> Unlock(long repositoryId, int issueNumber);
    }
}
