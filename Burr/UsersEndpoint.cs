using System.Threading.Tasks;
using Burr.Helpers;

namespace Burr
{
    /// <summary>
    /// Supports the ability to get and update users via the GitHub API v3.
    /// http://developer.github.com/v3/users/
    /// </summary>
    public class UsersEndpoint : IUsersEndpoint
    {
        IGitHubClient client;

        public UsersEndpoint(IGitHubClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the specified login (username). Returns the
        /// Authenticated <see cref="User"/> if no login (username) is given.
        /// </summary>
        /// <param name="login">Optional GitHub login (username)</param>
        /// <returns>A <see cref="User"/></returns>
        public async Task<User> GetAsync(string login = null)
        {
            if (login.IsBlank() && client.AuthenticationType == AuthenticationType.Anonymous)
            {
                throw new AuthenticationException("You must be authenticated to call this method. Either supply a login/password or an oauth token.");
            }

            var endpoint = login.IsBlank() ? "/user" : string.Format("/users/{0}", login);
            var res = await client.Connection.GetAsync<User>(endpoint);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Update the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateAsync(User user)
        {
            Ensure.ArgumentNotNull(user, "user");

            if (client.AuthenticationType == AuthenticationType.Anonymous)
            {
                throw new AuthenticationException("You must be authenticated to call this method. Either supply a login/password or an oauth token.");
            }

            var res = await client.Connection.PatchAsync<User>("/user", user);

            return res.BodyAsObject;
        }
    }
}
