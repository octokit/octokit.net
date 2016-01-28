using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableUserAdministrationClient
    {
        /// <summary>
        /// A client for GitHub's User Administration API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
        /// </remarks>
        IObservable<Unit> Promote(string login);

        /// <summary>
        /// A client for GitHub's User Administration API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
        /// </remarks>
        IObservable<Unit> Demote(string login);

        /// <summary>
        /// A client for GitHub's User Administration API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
        /// </remarks>
        IObservable<Unit> Suspend(string login);

        /// <summary>
        /// A client for GitHub's User Administration API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
        /// </remarks>
        IObservable<Unit> Unsuspend(string login);

    }
}
