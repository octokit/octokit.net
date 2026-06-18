using System.Collections.Generic;
using System.Threading;
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
        [ManualRoute("GET", "/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForCurrent(CancellationToken cancellationToken = default)
        {
            return GetAllForCurrent(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForCurrent(NotificationsRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForCurrent(request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForCurrent(NotificationsRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(owner, name), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(repositoryId), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, NotificationsRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, NotificationsRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, NotificationsRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(owner, name), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, NotificationsRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(repositoryId), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        [ManualRoute("PUT", "/notifications")]
        public Task MarkAsRead(CancellationToken cancellationToken = default)
        {
            return ApiConnection.Put<object>(ApiUrls.Notifications(), new object(), cancellationToken);
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        [ManualRoute("PUT", "/notifications")]
        public Task MarkAsRead(MarkAsReadRequest markAsReadRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return ApiConnection.Put<object>(ApiUrls.Notifications(), markAsReadRequest, cancellationToken);
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/notifications")]
        public Task MarkAsReadForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Put<object>(ApiUrls.Notifications(owner, name), new object(), cancellationToken);
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        [ManualRoute("PUT", "/repositories/{id}/notifications")]
        public Task MarkAsReadForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Put<object>(ApiUrls.Notifications(repositoryId), new object(), cancellationToken);
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/notifications")]
        public Task MarkAsReadForRepository(string owner, string name, MarkAsReadRequest markAsReadRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return ApiConnection.Put<object>(ApiUrls.Notifications(owner, name), markAsReadRequest, cancellationToken);
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        [ManualRoute("PUT", "/repositories/{id}/notifications")]
        public Task MarkAsReadForRepository(long repositoryId, MarkAsReadRequest markAsReadRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return ApiConnection.Put<object>(ApiUrls.Notifications(repositoryId), markAsReadRequest, cancellationToken);
        }

        /// <summary>
        /// Retrives a single <see cref="Notification"/> by Id.
        /// </summary>
        /// <param name="threadId">The Id of the notification to retrieve.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#view-a-single-thread</remarks>
        [ManualRoute("GET", "/notifications/threads/{thread_id}")]
        public Task<Notification> Get(int threadId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Get<Notification>(ApiUrls.Notification(threadId), null, cancellationToken);
        }

        /// <summary>
        /// Marks a single notification as read.
        /// </summary>
        /// <param name="threadId">The id of the notification.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-a-thread-as-read</remarks>
        [ManualRoute("PATCH", "/notifications/threads/{thread_id}")]
        public Task MarkAsRead(int threadId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Patch(ApiUrls.Notification(threadId), cancellationToken);
        }

        /// <summary>
        /// Retrives a <see cref="ThreadSubscription"/> for the provided thread id.
        /// </summary>
        /// <param name="threadId">The Id of the thread to retrieve subscription status.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#get-a-thread-subscription</remarks>
        [ManualRoute("GET", "/notifications/threads/{thread_id}/subscription")]
        public Task<ThreadSubscription> GetThreadSubscription(int threadId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Get<ThreadSubscription>(ApiUrls.NotificationSubscription(threadId), null, cancellationToken);
        }

        /// <summary>
        /// Sets the authenticated user's subscription settings for a given thread.
        /// </summary>
        /// <param name="threadId">The Id of the thread to update.</param>
        /// <param name="threadSubscription">The subscription parameters to set.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#set-a-thread-subscription</remarks>
        [ManualRoute("PUT", "/notifications/threads/{thread_id}/subscription")]
        public Task<ThreadSubscription> SetThreadSubscription(int threadId, NewThreadSubscription threadSubscription, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(threadSubscription, nameof(threadSubscription));

            return ApiConnection.Put<ThreadSubscription>(ApiUrls.NotificationSubscription(threadId), threadSubscription, cancellationToken);
        }

        /// <summary>
        /// Deletes the authenticated user's subscription to a given thread.
        /// </summary>
        /// <param name="threadId">The Id of the thread to delete subscription from.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#delete-a-thread-subscription</remarks>
        [ManualRoute("DELETE", "/notifications/threads/{thread_id}/subscription")]
        public Task DeleteThreadSubscription(int threadId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Delete(ApiUrls.NotificationSubscription(threadId), cancellationToken);
        }
    }
}
