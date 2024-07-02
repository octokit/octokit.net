using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Activity Notifications API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/notifications/">Activity Notifications API documentation</a> for more information.
    /// </remarks>
    public class ObservableNotificationsClient : IObservableNotificationsClient
    {
        readonly IConnection _connection;
        readonly INotificationsClient _notificationsClient;

        public ObservableNotificationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _notificationsClient = client.Activity.Notifications;
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(owner, name), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(repositoryId), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForCurrent(NotificationsRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForCurrent(request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForCurrent(NotificationsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForRepository(string owner, string name, NotificationsRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForRepository(long repositoryId, NotificationsRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForRepository(string owner, string name, NotificationsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        public IObservable<Notification> GetAllForRepository(long repositoryId, NotificationsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Notification>(ApiUrls.Notifications(repositoryId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        public IObservable<Unit> MarkAsRead()
        {
            return _notificationsClient.MarkAsRead().ToObservable();
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        public IObservable<Unit> MarkAsRead(MarkAsReadRequest markAsReadRequest)
        {
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return _notificationsClient.MarkAsRead(markAsReadRequest).ToObservable();
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        public IObservable<Unit> MarkAsReadForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _notificationsClient.MarkAsReadForRepository(owner, name).ToObservable();
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        public IObservable<Unit> MarkAsReadForRepository(long repositoryId)
        {
            return _notificationsClient.MarkAsReadForRepository(repositoryId).ToObservable();
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        public IObservable<Unit> MarkAsReadForRepository(string owner, string name, MarkAsReadRequest markAsReadRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return _notificationsClient.MarkAsReadForRepository(owner, name, markAsReadRequest).ToObservable();
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        public IObservable<Unit> MarkAsReadForRepository(long repositoryId, MarkAsReadRequest markAsReadRequest)
        {
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return _notificationsClient.MarkAsReadForRepository(repositoryId, markAsReadRequest).ToObservable();
        }

        /// <summary>
        /// Retrives a single <see cref="Notification"/> by Id.
        /// </summary>
        /// <param name="notificationId">The Id of the notification to retrieve.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#view-a-single-thread</remarks>
        public IObservable<Notification> Get(int notificationId)
        {
            return _notificationsClient.Get(notificationId).ToObservable();
        }

        /// <summary>
        /// Marks a single notification as read.
        /// </summary>
        /// <param name="notificationId">The id of the notification.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-a-thread-as-read</remarks>
        public IObservable<Unit> MarkAsRead(int notificationId)
        {
            return _notificationsClient.MarkAsRead(notificationId).ToObservable();
        }

        /// <summary>
        /// Retrives a <see cref="ThreadSubscription"/> for the provided thread id.
        /// </summary>
        /// <param name="threadId">The Id of the thread to retrieve subscription status.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#get-a-thread-subscription</remarks>
        public IObservable<ThreadSubscription> GetThreadSubscription(int threadId)
        {
            return _notificationsClient.GetThreadSubscription(threadId).ToObservable();
        }

        /// <summary>
        /// Sets the authenticated user's subscription settings for a given thread.
        /// </summary>
        /// <param name="threadId">The Id of the thread to update.</param>
        /// <param name="threadSubscription">The subscription parameters to set.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#set-a-thread-subscription</remarks>
        public IObservable<ThreadSubscription> SetThreadSubscription(int threadId, NewThreadSubscription threadSubscription)
        {
            Ensure.ArgumentNotNull(threadSubscription, nameof(threadSubscription));

            return _notificationsClient.SetThreadSubscription(threadId, threadSubscription).ToObservable();
        }

        /// <summary>
        /// Deletes the authenticated user's subscription to a given thread.
        /// </summary>
        /// <param name="threadId">The Id of the thread to delete subscription from.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#delete-a-thread-subscription</remarks>
        public IObservable<Unit> DeleteThreadSubscription(int threadId)
        {
            return _notificationsClient.DeleteThreadSubscription(threadId).ToObservable();
        }
    }
}
