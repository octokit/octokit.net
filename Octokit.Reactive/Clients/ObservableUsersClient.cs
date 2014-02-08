using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableUsersClient : IObservableUsersClient
    {
        readonly IUsersClient _client;

        public ObservableUsersClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User;
        }

        /// <summary>
        /// Returns the user specified by the login.
        /// </summary>
        /// <param name="login">The login name for the user</param>
        public IObservable<User> Get(string login)
        {
            Ensure.ArgumentNotNull(login, "login");

            return _client.Get(login).ToObservable();
        }

        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public IObservable<User> Current()
        {
            return _client.Current().ToObservable();
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        public IObservable<User> Update(UserUpdate user)
        {
            Ensure.ArgumentNotNull(user, "user");

            return _client.Update(user).ToObservable();
        }
    }
}
