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
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public Task<IReadOnlyList<Notification>> GetAllForCurrent(NotificationsRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(), request.ToParametersDictionary());
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(owner, name));
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, NotificationsRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(owner, name), request.ToParametersDictionary());
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        /// <returns></returns>
        public Task MarkAsRead()
        {
            return ApiConnection.Put(ApiUrls.Notifications());
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        /// <returns></returns>
        public Task MarkAsRead(MarkAsReadRequest markAsReadRequest)
        {
            return ApiConnection.Put<object>(ApiUrls.Notifications(), markAsReadRequest);
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        /// <returns></returns>
        public Task MarkAsReadForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Put(ApiUrls.Notifications(owner, name));
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="markAsRead">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        /// <returns></returns>
        public Task MarkAsReadForRepository(string owner, string name, MarkAsReadRequest markAsRead)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Put<object>(ApiUrls.Notifications(owner, name), markAsRead);
        }

        /// <summary>
        /// Retrives a single <see cref="Notification"/> by Id.
        /// </summary>
        /// <param name="id">The Id of the notification to retrieve.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#view-a-single-thread</remarks>
        /// <returns>A <see cref="Notification"/> for the given Id.</returns>
        public Task<Notification> Get(int id)
        {
            return ApiConnection.Get<Notification>(ApiUrls.Notification(id));
        }

        /// <summary>
        /// Marks a single notification as read.
        /// </summary>
        /// <param name="id">The id of the notification.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-a-thread-as-read</remarks>
        /// <returns></returns>
        public Task MarkAsRead(int id)
        {
            return ApiConnection.Patch(ApiUrls.Notification(id));
        }

        /// <summary>
        /// Retrives a <see cref="ThreadSubscription"/> for the provided thread id.
        /// </summary>
        /// <param name="id">The Id of the thread to retrieve subscription status.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#get-a-thread-subscription</remarks>
        /// <returns>A <see cref="ThreadSubscription"/> for the chosen thread.</returns>
        public Task<ThreadSubscription> GetThreadSubscription(int id)
        {
            return ApiConnection.Get<ThreadSubscription>(ApiUrls.NotificationSubscription(id));
        }

        /// <summary>
        /// Sets the authenticated user's subscription settings for a given thread.
        /// </summary>
        /// <param name="id">The Id of the thread to update.</param>
        /// <param name="threadSubscription">The subscription parameters to set.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#set-a-thread-subscription</remarks>
        /// <returns></returns>
        public Task<ThreadSubscription> SetThreadSubscription(int id, NewThreadSubscription threadSubscription)
        {
            Ensure.ArgumentNotNull(threadSubscription, "threadSubscription");

            return ApiConnection.Put<ThreadSubscription>(ApiUrls.NotificationSubscription(id), threadSubscription);
        }

        /// <summary>
        /// Deletes the authenticated user's subscription to a given thread.
        /// </summary>
        /// <param name="id">The Id of the thread to delete subscription from.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#delete-a-thread-subscription</remarks>
        /// <returns></returns>
        public Task DeleteThreadSubscription(int id)
        {
            return ApiConnection.Delete(ApiUrls.NotificationSubscription(id));
        }
    }
}
