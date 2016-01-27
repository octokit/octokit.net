using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableNotificationsClient : IObservableNotificationsClient
    {
        readonly IConnection _connection;
        readonly INotificationsClient _notificationsClient;

        public ObservableNotificationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _connection = client.Connection;
            _notificationsClient = client.Activity.Notifications;
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
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public IObservable<Notification> GetAllForCurrent(NotificationsRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(), request.ToParametersDictionary());
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

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        public IObservable<Notification> GetAllForRepository(string owner, string name, NotificationsRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(owner, name), request.ToParametersDictionary());
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        /// <returns></returns>
        public IObservable<Unit> MarkAsRead()
        {
            return _notificationsClient.MarkAsRead().ToObservable();
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        /// <returns></returns>
        public IObservable<Unit> MarkAsRead(MarkAsReadRequest markAsReadRequest)
        {
            return _notificationsClient.MarkAsRead(markAsReadRequest).ToObservable();
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        /// <returns></returns>
        public IObservable<Unit> MarkAsReadForRepository(string owner, string name)
        {
            return _notificationsClient.MarkAsReadForRepository(owner, name).ToObservable();
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="markAsRead">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        /// <returns></returns>
        public IObservable<Unit> MarkAsReadForRepository(string owner, string name, MarkAsReadRequest markAsRead)
        {
            return _notificationsClient.MarkAsReadForRepository(owner, name, markAsRead).ToObservable();
        }

        /// <summary>
        /// Retrives a single <see cref="Notification"/> by Id.
        /// </summary>
        /// <param name="id">The Id of the notification to retrieve.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#view-a-single-thread</remarks>
        /// <returns>A <see cref="Notification"/> for the given Id.</returns>
        public IObservable<Notification> Get(int id)
        {
            return _notificationsClient.Get(id).ToObservable();
        }

        /// <summary>
        /// Marks a single notification as read.
        /// </summary>
        /// <param name="id">The id of the notification.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-a-thread-as-read</remarks>
        /// <returns></returns>
        public IObservable<Unit> MarkAsRead(int id)
        {
            return _notificationsClient.MarkAsRead(id).ToObservable();
        }

        /// <summary>
        /// Retrives a <see cref="ThreadSubscription"/> for the provided thread id.
        /// </summary>
        /// <param name="id">The Id of the thread to retrieve subscription status.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#get-a-thread-subscription</remarks>
        /// <returns>A <see cref="ThreadSubscription"/> for the chosen thread.</returns>
        public IObservable<ThreadSubscription> GetThreadSubscription(int id)
        {
            return _notificationsClient.GetThreadSubscription(id).ToObservable();
        }

        /// <summary>
        /// Sets the authenticated user's subscription settings for a given thread.
        /// </summary>
        /// <param name="id">The Id of the thread to update.</param>
        /// <param name="threadSubscription">The subscription parameters to set.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#set-a-thread-subscription</remarks>
        /// <returns></returns>
        public IObservable<ThreadSubscription> SetThreadSubscription(int id, NewThreadSubscription threadSubscription)
        {
            return _notificationsClient.SetThreadSubscription(id, threadSubscription).ToObservable();
        }

        /// <summary>
        /// Deletes the authenticated user's subscription to a given thread.
        /// </summary>
        /// <param name="id">The Id of the thread to delete subscription from.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#delete-a-thread-subscription</remarks>
        /// <returns></returns>
        public IObservable<Unit> DeleteThreadSubscription(int id)
        {
            return _notificationsClient.DeleteThreadSubscription(id).ToObservable();
        }
    }
}
