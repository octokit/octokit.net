using System;

namespace Octokit.Reactive
{
    public interface IObservableEventsClient
    {
        /// <summary>
        /// Gets all the public events
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events
        /// </remarks>
        /// <returns>All the public <see cref="Activity"/>s for the particular user.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<Activity> GetAll();

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="Activity"/>s for the particular repository.</returns>
        IObservable<Activity> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets all the events for a given repository network
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-a-network-of-repositories
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="Activity"/>s for the particular repository network.</returns>
        IObservable<Activity> GetAllForRepositoryNetwork(string owner, string name);

        /// <summary>
        /// Gets all the events for a given organization
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-an-organization
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <returns>All the <see cref="Activity"/>s for the particular organization.</returns>
        IObservable<Activity> GetAllForOrganization(string organization);

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has received.</returns>
        IObservable<Activity> GetUserReceived(string user);

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has received.</returns>
        IObservable<Activity> GetUserReceivedPublic(string user);

        /// <summary>
        /// Gets all the events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has performed.</returns>
        IObservable<Activity> GetUserPerformed(string user);

        /// <summary>
        /// Gets all the public events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the public <see cref="Activity"/>s that a particular user has performed.</returns>
        IObservable<Activity> GetUserPerformedPublic(string user);

        /// <summary>
        /// Gets all the events that are associated with an organization.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-for-an-organization
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="organization">The name of the organization</param>
        /// <returns>All the public <see cref="Activity"/>s that are associated with an organization.</returns>
        IObservable<Activity> GetForAnOrganization(string user, string organization);
    }
}