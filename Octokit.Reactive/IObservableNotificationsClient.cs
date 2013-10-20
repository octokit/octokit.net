using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableNotificationsClient
    {
        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IObservable{Notification}"/> of <see cref="Notification"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<Notification> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IObservable{Notification}"/> of <see cref="Notification"/>.</returns>
        IObservable<Notification> GetAllForRepository(string owner, string name);

    }
}
