using System;
#if NET_45
using System.Collections.Generic;
using System.Collections.ObjectModel;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Users API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/">Users API documentation</a> for more information.
    /// </remarks>
    public class UsersClient : ApiClient, IUsersClient
    {
        static readonly Uri _userEndpoint = new Uri("user", UriKind.Relative);

        /// <summary>
        /// Instantiates a new GitHub Users API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public UsersClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns the user specified by the login.
        /// </summary>
        /// <param name="login">The login name for the user</param>
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
        /// <param name="user">The login for the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public Task<User> Update(UserUpdate user)
        {
            Ensure.ArgumentNotNull(user, "user");

            return ApiConnection.Patch<User>(_userEndpoint, user);
        }
    }
}
