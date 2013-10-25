using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class NotificationsClient : ApiClient, INotificationsClient
    {
        public NotificationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public async Task<IReadOnlyCollection<Notification>> GetAllForCurrent()
        {
            return await ApiConnection.GetAll<Notification>(ApiUrls.Notifications());
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public async Task<IReadOnlyCollection<Notification>> GetAllForRepository(string owner, string name)
        {
            return await ApiConnection.GetAll<Notification>(ApiUrls.Notifications(owner, name));
        }
    }
}
