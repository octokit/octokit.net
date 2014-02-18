using System;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableNotificationsClient : IObservableNotificationsClient
    {
        readonly IConnection _connection;

        public ObservableNotificationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _connection = client.Connection;
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IObservable{Notification}"/> of <see cref="Notification"/>.</returns>
        public IObservable<Notification> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications());
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IObservable{Notification}"/> of <see cref="Notification"/>.</returns>
        public IObservable<Notification> GetAllForRepository(string owner, string name)
        {
            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(owner, name));
        }
    }
}
