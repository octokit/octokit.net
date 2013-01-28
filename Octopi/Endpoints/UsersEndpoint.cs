using System;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Endpoints
{
    /// <summary>
    /// Supports the ability to get and update users via the GitHub API v3.
    /// http://developer.github.com/v3/users/
    /// </summary>
    public class UsersEndpoint : ApiEndpoint<User>, IUsersEndpoint
    {
        static readonly Uri userEndpoint = new Uri("/user", UriKind.Relative);

        public UsersEndpoint(IApiClient<User> client) : base(client)
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
            return await Client.Get(endpoint);
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> Current()
        {
            return await Client.Get(userEndpoint);
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> Update(UserUpdate user)
        {
            Ensure.ArgumentNotNull(user, "user");

            return await Client.Update(userEndpoint, user);
        }
    }
}
