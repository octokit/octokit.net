﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableUsersClient
    {
        /// <summary>
        /// Returns the user specified by the login.
        /// </summary>
        /// <param name="login">The login name for the user</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "login")]
        IObservable<User> Get(string login);

        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        IObservable<User> Current();

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        IObservable<User> Update(UserUpdate user);

        /// <summary>
        /// A client for GitHub's User Followers API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/">Followers API documentation</a> for more information.
        ///</remarks>
        IObservableFollowersClient Followers { get; }

        /// <summary>
        /// A client for GitHub's User Emails API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/emails/">Emails API documentation</a> for more information.
        ///</remarks>
        IObservableUserEmailsClient Email { get; }

        /// <summary>
        /// A client for GitHub's User Keys API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/keys/">Keys API documentation</a> for more information.
        ///</remarks>
        IObservableUserKeysClient GitSshKey { get; }

        /// <summary>
        /// A client for GitHub's UserUser GPG Keys API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/">User GPG Keys documentation</a> for more information.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
        IObservableUserGpgKeysClient GpgKey { get; }

        /// <summary>
        /// A client for GitHub's User Administration API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/">User Administrator API documentation</a> for more information.
        ///</remarks>
        IObservableUserAdministrationClient Administration { get; }
    }
}
