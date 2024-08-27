using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// Client to manage locking/unlocking a conversation for an Issue or a Pull request
    /// </summary>
    public class ObservableLockUnlockClient : IObservableLockUnlockClient
    {
        readonly ILockUnlockClient _client;

        public ObservableLockUnlockClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Issue.LockUnlock;
        }

        /// <summary>
        /// Locks an issue for the specified repository. Issue owners and users with push access can lock an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#lock-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="lockReason">The reason for locking the issue</param>
        public IObservable<Unit> Lock(string owner, string name, long issueNumber, LockReason? lockReason = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Lock(owner, name, issueNumber, lockReason).ToObservable();
        }

        /// <summary>
        /// Locks an issue for the specified repository. Issue owners and users with push access can lock an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#lock-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="lockReason">The reason for locking the issue</param>
        public IObservable<Unit> Lock(long repositoryId, long issueNumber, LockReason? lockReason = null)
        {
            return _client.Lock(repositoryId, issueNumber, lockReason).ToObservable();
        }

        /// <summary>
        /// Unlocks an issue for the specified repository. Issue owners and users with push access can unlock an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#unlock-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        public IObservable<Unit> Unlock(string owner, string name, long issueNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Unlock(owner, name, issueNumber).ToObservable();
        }

        /// <summary>
        /// Unlocks an issue for the specified repository. Issue owners and users with push access can unlock an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#unlock-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        public IObservable<Unit> Unlock(long repositoryId, long issueNumber)
        {
            return _client.Unlock(repositoryId, issueNumber).ToObservable();
        }
    }
}
