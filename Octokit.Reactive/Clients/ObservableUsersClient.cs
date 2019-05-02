using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableUsersClient : IObservableUsersClient
    {
        readonly IUsersClient _client;

        public ObservableUsersClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.User;

            Followers = new ObservableFollowersClient(client);
            Email = new ObservableUserEmailsClient(client);
            GitSshKey = new ObservableUserKeysClient(client);
            GpgKey = new ObservableUserGpgKeysClient(client);
            Administration = new ObservableUserAdministrationClient(client);
        }

        /// <summary>
        /// Returns the user specified by the login.
        /// </summary>
        /// <param name="login">The login name for the user</param>
        public IObservable<User> Get(string login)
        {
            Ensure.ArgumentNotNull(login, nameof(login));

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
            Ensure.ArgumentNotNull(user, nameof(user));

            return _client.Update(user).ToObservable();
        }

        /// <summary>
        /// A client for GitHub's User Followers API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/">Followers API documentation</a> for more information.
        ///</remarks>
        public IObservableFollowersClient Followers { get; private set; }

        /// <summary>
        /// A client for GitHub's User Emails API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/emails/">Emails API documentation</a> for more information.
        ///</remarks>
        public IObservableUserEmailsClient Email { get; private set; }

        /// <summary>
        /// A client for GitHub's User Keys API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/keys/">Keys API documentation</a> for more information.
        ///</remarks>
        public IObservableUserKeysClient GitSshKey { get; private set; }

        /// <summary>
        /// A client for GitHub's UserUser GPG Keys API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/">User GPG Keys documentation</a> for more information.
        /// </remarks>
        public IObservableUserGpgKeysClient GpgKey { get; private set; }

        /// <summary>
        /// A client for GitHub's User Administration API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/">User Administrator API documentation</a> for more information.
        ///</remarks>
        public IObservableUserAdministrationClient Administration { get; private set; }
    }
}
