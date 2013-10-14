﻿using System;
#if NET_45
using System.Collections.Generic;
using System.Collections.ObjectModel;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Supports the ability to get and update users via the GitHub API v3.
    /// http://developer.github.com/v3/users/
    /// </summary>
    public class UsersClient : ApiClient, IUsersClient
    {
        static readonly Uri userEndpoint = new Uri("/user", UriKind.Relative);
        static readonly Uri emailsEndpoint = new Uri("/user/emails", UriKind.Relative);

        public UsersClient(IApiConnection client) : base(client)
        {
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the specified login (username). Returns the
        /// Authenticated <see cref="User"/> if no login (username) is given.
        /// </summary>
        /// <param name="login">Optional GitHub login (username)</param>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> Get(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = "/users/{0}".FormatUri(login);
            return await Client.Get<User>(endpoint);
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> Current()
        {
            return await Client.Get<User>(userEndpoint);
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> Update(UserUpdate user)
        {
            Ensure.ArgumentNotNull(user, "user");

            return await Client.Patch<User>(userEndpoint, user);
        }

        /// <summary>
        /// Returns emails for the current user.
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<EmailAddress>> GetEmails()
        {
            return await Client.Get<ReadOnlyCollection<EmailAddress>>(emailsEndpoint, null);
        }
    }
}
