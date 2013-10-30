using System;
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
        static readonly Uri _userEndpoint = new Uri("user", UriKind.Relative);

        public UsersClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the specified login (username). Returns the
        /// Authenticated <see cref="User"/> if no login (username) is given.
        /// </summary>
        /// <param name="login">Optional GitHub login (username)</param>
        /// <returns>A <see cref="User"/></returns>
        public Task<User> Get(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = "users/{0}".FormatUri(login);
            return ApiConnection.Get<User>(endpoint);
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public Task<User> Current()
        {
            return ApiConnection.Get<User>(_userEndpoint);
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public Task<User> Update(UserUpdate user)
        {
            Ensure.ArgumentNotNull(user, "user");

            return ApiConnection.Patch<User>(_userEndpoint, user);
        }

        /// <summary>
        /// Returns emails for the current user.
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyCollection<EmailAddress>> GetEmails()
        {
            return ApiConnection.Get<IReadOnlyCollection<EmailAddress>>(ApiUrls.Emails(), null);
        }
    }
}
