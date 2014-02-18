using System;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableEventsClient : IObservableEventsClient
    {
        readonly IConnection _connection;

        public ObservableEventsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all the public events
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events
        /// </remarks>
        /// <returns>All the public <see cref="Activity"/>s for the particular user.</returns>
        public IObservable<Activity> GetAll()
        {
            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.Events());
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="Activity"/>s for the particular repository.</returns>
        public IObservable<Activity> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.IssuesEvents(owner, name));
        }

        /// <summary>
        /// Gets all the events for a given repository network
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-a-network-of-repositories
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="Activity"/>s for the particular repository network.</returns>
        public IObservable<Activity> GetAllForRepositoryNetwork(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.NetworkEvents(owner, name));
        }

        /// <summary>
        /// Gets all the events for a given organization
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-an-organization
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <returns>All the <see cref="Activity"/>s for the particular organization.</returns>
        public IObservable<Activity> GetAllForOrganization(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.OrganizationEvents(organization));
        }

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has received.</returns>
        public IObservable<Activity> GetUserReceived(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.ReceivedEvents(user));
        }

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has received.</returns>
        public IObservable<Activity> GetUserReceivedPublic(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.ReceivedEvents(user, true));
        }

        /// <summary>
        /// Gets all the events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has performed.</returns>
        public IObservable<Activity> GetUserPerformed(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.PerformedEvents(user));
        }

        /// <summary>
        /// Gets all the public events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the public <see cref="Activity"/>s that a particular user has performed.</returns>
        public IObservable<Activity> GetUserPerformedPublic(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.PerformedEvents(user, true));
        }

        /// <summary>
        /// Gets all the events that are associated with an organization.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-for-an-organization
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="organization">The name of the organization</param>
        /// <returns>All the public <see cref="Activity"/>s that are associated with an organization.</returns>
        public IObservable<Activity> GetForAnOrganization(string user, string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.OrganizationEvents(user, organization));
        }
    }
}
