using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Endpoints
{
    /// <summary>
    /// Supports the ability to get and update users via the GitHub API v3.
    /// http://developer.github.com/v3/users/
    /// </summary>
    public class UsersEndpoint : IUsersEndpoint
    {
        static readonly Uri userEndpoint = new Uri("/user", UriKind.Relative);
        static readonly Uri usersEndpoint = new Uri("/users", UriKind.Relative);

        readonly IConnection connection;

        public UsersEndpoint(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "client");

            this.connection = connection;
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

            var endpoint = new Uri(string.Format("/users/{0}", login), UriKind.Relative);
            var res = await connection.GetAsync<User>(endpoint);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> Current()
        {
            if (connection.AuthenticationType == AuthenticationType.Anonymous)
            {
                throw new AuthenticationException("You must be authenticated to call this method. Either supply a login/password or an oauth token.");
            }

            var res = await connection.GetAsync<User>(userEndpoint);
            return res.BodyAsObject;
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

            if (connection.AuthenticationType == AuthenticationType.Anonymous)
            {
                throw new AuthenticationException("You must be authenticated to call this method. Either supply a login/password or an oauth token.");
            }

            var res = await connection.PatchAsync<User>(userEndpoint, user);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Returns a list of public <see cref="User"/>s on GitHub.com.
        /// </summary>
        /// <returns>A <see cref="User"/></returns>
        public async Task<List<User>> GetAll()
        {
            var res = await connection.GetAsync<List<User>>(usersEndpoint);

            return res.BodyAsObject;
        }
    }
}
