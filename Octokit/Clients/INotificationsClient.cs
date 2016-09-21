using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity Notifications API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/notifications/">Activity Notifications API documentation</a> for more information.
    /// </remarks>
    public interface INotificationsClient
    {
        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Notification>> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Notification>> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Notification>> GetAllForCurrent(NotificationsRequest request);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Notification>> GetAllForCurrent(NotificationsRequest request, ApiOptions options);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, ApiOptions options);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, NotificationsRequest request);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, NotificationsRequest request);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name, NotificationsRequest request, ApiOptions options);

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="request">Specifies the parameters to filter notifications by</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        Task<IReadOnlyList<Notification>> GetAllForRepository(long repositoryId, NotificationsRequest request, ApiOptions options);

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        Task MarkAsRead();

        /// <summary>
        /// Marks all notifications as read.
        /// </summary>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-as-read</remarks>
        Task MarkAsRead(MarkAsReadRequest markAsReadRequest);

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        Task MarkAsReadForRepository(string owner, string name);

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        Task MarkAsReadForRepository(long repositoryId);

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        Task MarkAsReadForRepository(string owner, string name, MarkAsReadRequest markAsReadRequest);

        /// <summary>
        /// Marks the notifications for a given repository as read.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="markAsReadRequest">The <see cref="MarkAsReadRequest"/> parameter which specifies which notifications to mark.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-notifications-as-read-in-a-repository</remarks>
        Task MarkAsReadForRepository(long repositoryId, MarkAsReadRequest markAsReadRequest);

        /// <summary>
        /// Retrives a single <see cref="Notification"/> by Id.
        /// </summary>
        /// <param name="id">The Id of the notification to retrieve.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#view-a-single-thread</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Notification> Get(int id);

        /// <summary>
        /// Marks a single notification as read.
        /// </summary>
        /// <param name="id">The id of the notification.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#mark-a-thread-as-read</remarks>
        Task MarkAsRead(int id);

        /// <summary>
        /// Retrives a <see cref="ThreadSubscription"/> for the provided thread id.
        /// </summary>
        /// <param name="id">The Id of the thread to retrieve subscription status.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#get-a-thread-subscription</remarks>
        Task<ThreadSubscription> GetThreadSubscription(int id);

        /// <summary>
        /// Sets the authenticated user's subscription settings for a given thread.
        /// </summary>
        /// <param name="id">The Id of the thread to update.</param>
        /// <param name="threadSubscription">The subscription parameters to set.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#set-a-thread-subscription</remarks>
        Task<ThreadSubscription> SetThreadSubscription(int id, NewThreadSubscription threadSubscription);

        /// <summary>
        /// Deletes the authenticated user's subscription to a given thread.
        /// </summary>
        /// <param name="id">The Id of the thread to delete subscription from.</param>
        /// <remarks>http://developer.github.com/v3/activity/notifications/#delete-a-thread-subscription</remarks>
        Task DeleteThreadSubscription(int id);
    }
}
