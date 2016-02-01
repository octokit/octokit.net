using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;
using System.Reactive.Linq;


namespace Octokit.Reactive
{
    public class ObservableUserAdministrationClient : IObservableUserAdministrationClient
    {
        readonly IUserAdministrationClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableUserAdministrationClient"/> class.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableUserAdministrationClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User.Administration;
        }

        /// <summary>
        /// Promotes ordinary user to a site administrator.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#promote-an-ordinary-user-to-a-site-administrator
        /// </remarks>
        /// <param name="login">The user to promote to administrator.</param>
        /// <returns></returns>
        public IObservable<Unit> Promote(string login)
        {
            return _client.Promote(login).ToObservable();
        }

        /// <summary>
        /// Demotes a site administrator to an ordinary user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#demote-a-site-administrator-to-an-ordinary-user
        /// </remarks>
        /// <param name="login">The user to demote from administrator.</param>
        /// <returns></returns>
        public IObservable<Unit> Demote(string login)
        {
            return _client.Demote(login).ToObservable();
        }

        /// <summary>
        /// Suspends a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#suspend-a-user
        /// </remarks>
        /// <param name="login">The user to suspend.</param>
        /// <returns></returns>
        public IObservable<Unit> Suspend(string login)
        {
            return _client.Suspend(login).ToObservable();
        }

        /// <summary>
        /// Unsuspends a user.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/users/administration/#unsuspend-a-user
        /// </remarks>
        /// <param name="login">The user to unsuspend.</param>
        /// <returns></returns>
        public IObservable<Unit> Unsuspend(string login)
        {
            return _client.Unsuspend(login).ToObservable();
        }
    }
}
