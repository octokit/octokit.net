using System;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Activity Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/events/">Activity Events API documentation</a> for more information
    /// </remarks>
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
        public IObservable<Activity> GetAll()
        {
            return GetAll(ApiOptions.None);
        }

        /// <summary>
        /// Gets all the public events
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.Events(), options);
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<Activity> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<Activity> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.Events(owner, name), options);
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.Events(repositoryId), options);
        }

        /// <summary>
        /// Gets all the issue events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<IssueEvent> GetAllIssuesForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllIssuesForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the issue events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<IssueEvent> GetAllIssuesForRepository(long repositoryId)
        {
            return GetAllIssuesForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the issue events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueEvent> GetAllIssuesForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<IssueEvent>(ApiUrls.IssuesEvents(owner, name), options);
        }

        /// <summary>
        /// Gets all the issue events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueEvent> GetAllIssuesForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<IssueEvent>(ApiUrls.IssuesEvents(repositoryId), options);
        }

        /// <summary>
        /// Gets all the events for a given repository network
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-a-network-of-repositories
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<Activity> GetAllForRepositoryNetwork(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepositoryNetwork(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events for a given repository network
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-a-network-of-repositories
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllForRepositoryNetwork(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.NetworkEvents(owner, name), options);
        }

        /// <summary>
        /// Gets all the events for a given organization
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-an-organization
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        public IObservable<Activity> GetAllForOrganization(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return GetAllForOrganization(organization, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events for a given organization
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-an-organization
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllForOrganization(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.OrganizationEvents(organization), options);
        }

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        public IObservable<Activity> GetAllUserReceived(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return GetAllUserReceived(user, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllUserReceived(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.ReceivedEvents(user), options);
        }

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        public IObservable<Activity> GetAllUserReceivedPublic(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return GetAllUserReceivedPublic(user, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllUserReceivedPublic(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.ReceivedEvents(user, true), options);
        }

        /// <summary>
        /// Gets all the events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        public IObservable<Activity> GetAllUserPerformed(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return GetAllUserPerformed(user, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllUserPerformed(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.PerformedEvents(user), options);
        }

        /// <summary>
        /// Gets all the public events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        public IObservable<Activity> GetAllUserPerformedPublic(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return GetAllUserPerformedPublic(user, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the public events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllUserPerformedPublic(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.PerformedEvents(user, true), options);
        }

        /// <summary>
        /// Gets all the events that are associated with an organization.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-for-an-organization
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="organization">The name of the organization</param>
        public IObservable<Activity> GetAllForAnOrganization(string user, string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return GetAllForAnOrganization(user, organization, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events that are associated with an organization.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-for-an-organization
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="organization">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Activity> GetAllForAnOrganization(string user, string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Activity>(ApiUrls.OrganizationEvents(user, organization),options);            
        }
    }
}
