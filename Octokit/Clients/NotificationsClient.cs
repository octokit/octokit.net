using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity Notifications API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/notifications/">Activity Notifications API documentation</a> for more information.
    /// </remarks>
    public class NotificationsClient : ApiClient, INotificationsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Activity Notifications API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public NotificationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public Task<IReadOnlyList<Notification>> GetAllForCurrent()
        {
            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications());
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name)
        {
            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(owner, name));
        }
    }
}
