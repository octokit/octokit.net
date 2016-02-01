using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's User Administration API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
    /// </remarks>
    public interface IObservableUserAdministrationClient
    {
        /// <summary>
        /// Promotes ordinary user to a site administrator.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#promote-an-ordinary-user-to-a-site-administrator
        /// </remarks>
        /// <param name="login">The user to promote to administrator.</param>
        /// <returns></returns>
        IObservable<Unit> Promote(string login);

        /// <summary>
        /// Demotes a site administrator to an ordinary user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#demote-a-site-administrator-to-an-ordinary-user
        /// </remarks>
        /// <param name="login">The user to demote from administrator.</param>
        /// <returns></returns>
        IObservable<Unit> Demote(string login);

        /// <summary>
        /// Suspends a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#suspend-a-user
        /// </remarks>
        /// <param name="login">The user to suspend.</param>
        /// <returns></returns>
        IObservable<Unit> Suspend(string login);

        /// <summary>
        /// Unsuspends a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#unsuspend-a-user
        /// </remarks>
        /// <param name="login">The user to unsuspend.</param>
        /// <returns></returns>
        IObservable<Unit> Unsuspend(string login);
    }
}
