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
        [ManualRoute("GET", "/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForCurrent(NotificationsRequest request)
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
        [ManualRoute("GET", "/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForCurrent(NotificationsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name)
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
        [ManualRoute("GET", "/repositories/{id}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId)
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(owner, name), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(repositoryId), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, NotificationsRequest request)
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
        [ManualRoute("GET", "/repositories/{id}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, NotificationsRequest request)
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, NotificationsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/notifications")]
        public Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, NotificationsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Notification>(ApiUrls.Notifications(repositoryId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        [ManualRoute("PUT", "/notifications")]
        public Task MarkAsRead()
        {
            return ApiConnection.Put<object>(ApiUrls.Notifications(), new object());
        }

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        [ManualRoute("PUT", "/notifications")]
        public Task MarkAsRead(MarkAsReadRequest markAsReadRequest)
        {
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return ApiConnection.Put<object>(ApiUrls.Notifications(), markAsReadRequest);
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/notifications")]
        public Task MarkAsReadForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Put<object>(ApiUrls.Notifications(owner, name), new object());
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        [ManualRoute("PUT", "/repositories/{id}/notifications")]
        public Task MarkAsReadForRepository(long repositoryId)
        {
            return ApiConnection.Put<object>(ApiUrls.Notifications(repositoryId), new object());
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/notifications")]
        public Task MarkAsReadForRepository(string owner, string name, MarkAsReadRequest markAsReadRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return ApiConnection.Put<object>(ApiUrls.Notifications(owner, name), markAsReadRequest);
        }

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        [ManualRoute("PUT", "/repositories/{id}/notifications")]
        public Task MarkAsReadForRepository(long repositoryId, MarkAsReadRequest markAsReadRequest)
        {
            Ensure.ArgumentNotNull(markAsReadRequest, nameof(markAsReadRequest));

            return ApiConnection.Put<object>(ApiUrls.Notifications(repositoryId), markAsReadRequest);
        }

        /// <summary>
        /// Retrives a single <see cref="Notification"/> by Id.
        /// </summary>
        /// <param name="id">The Id of the notification to retrieve.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#view-a-single-thread</remarks>
        [ManualRoute("GET", "/notifications/threads/{thread_id}")]
        public Task<Notification> Get(int id)
        {
            return ApiConnection.Get<Notification>(ApiUrls.Notification(id));
        }

        /// <summary>
        /// Marks a single notification as read.
        /// </summary>
        /// <param name="id">The id of the notification.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-a-thread-as-read</remarks>
        [ManualRoute("PATCH", "/notifications/threads/{thread_id}")]
        public Task MarkAsRead(int id)
        {
            return ApiConnection.Patch(ApiUrls.Notification(id));
        }

        /// <summary>
        /// Retrives a <see cref="ThreadSubscription"/> for the provided thread id.
        /// </summary>
        /// <param name="id">The Id of the thread to retrieve subscription status.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#get-a-thread-subscription</remarks>
        [ManualRoute("GET", "/notifications/threads/{thread_id}/subscription")]
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
        [ManualRoute("PUT", "/notifications/threads/{thread_id}/subscription")]
        public Task<ThreadSubscription> SetThreadSubscription(int id, NewThreadSubscription threadSubscription)
        {
            Ensure.ArgumentNotNull(threadSubscription, nameof(threadSubscription));

            return ApiConnection.Put<ThreadSubscription>(ApiUrls.NotificationSubscription(id), threadSubscription);
        }

        /// <summary>
        /// Deletes the authenticated user's subscription to a given thread.
        /// </summary>
        /// <param name="id">The Id of the thread to delete subscription from.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#delete-a-thread-subscription</remarks>
        [ManualRoute("DELETE", "/notifications/threads/{thread_id}/subscription")]
        public Task DeleteThreadSubscription(int id)
        {
            return ApiConnection.Delete(ApiUrls.NotificationSubscription(id));
        }
    }
}
