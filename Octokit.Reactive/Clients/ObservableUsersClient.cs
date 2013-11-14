using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableUsersClient : IObservableUsersClient
    {
        readonly IUsersClient _client;
        readonly IConnection _connection;

        public ObservableUsersClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User;
            _connection = client.Connection;
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

        /// <summary>
        /// Returns emails for the current user.
        /// </summary>
        /// <returns></returns>
        public IObservable<EmailAddress> GetEmails()
        {
            return _connection.GetAndFlattenAllPages<EmailAddress>(ApiUrls.Emails());
        }
    }
}
