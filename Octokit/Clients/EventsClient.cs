using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/events/">Activity Events API documentation</a> for more information
    /// </remarks>
    public class EventsClient : ApiClient, IEventsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Issue Events API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public EventsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all the public events
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events
        /// </remarks>
        [ManualRoute("GET", "/events")]
        public Task<IReadOnlyList<Activity>> GetAll()
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
        /// <returns>All the public <see cref="Activity"/>s for the particular user.</returns>
        [ManualRoute("GET", "/events")]
        public Task<IReadOnlyList<Activity>> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.Events(), options);
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/activity/events/#list-repository-events
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/events")]
        public Task<IReadOnlyList<Activity>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/events")]
        public Task<IReadOnlyList<Activity>> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/activity/events/#list-repository-events
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/events")]
        public Task<IReadOnlyList<Activity>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.Events(owner, name), options);
        }

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/events")]
        public Task<IReadOnlyList<Activity>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.Events(repositoryId), options);
        }

        /// <summary>
        /// Gets all the event issues for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllIssuesForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllIssuesForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the issue events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllIssuesForRepository(long repositoryId)
        {
            return GetAllIssuesForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the event issues for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllIssuesForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(owner, name), options);
        }

        /// <summary>
        /// Gets all the issue events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllIssuesForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(repositoryId), options);
        }

        /// <summary>
        /// Gets all the events for a given repository network
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-a-network-of-repositories
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/networks/{owner}/{name}/events")]
        public Task<IReadOnlyList<Activity>> GetAllForRepositoryNetwork(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

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
        [ManualRoute("GET", "/networks/{owner}/{name}/events")]
        public Task<IReadOnlyList<Activity>> GetAllForRepositoryNetwork(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.NetworkEvents(owner, name), options);
        }

        /// <summary>
        /// Gets all the events for a given organization
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-an-organization
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        [ManualRoute("GET", "/orgs/{org}/events")]
        public Task<IReadOnlyList<Activity>> GetAllForOrganization(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

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
        [ManualRoute("GET", "/orgs/{org}/events")]
        public Task<IReadOnlyList<Activity>> GetAllForOrganization(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.OrganizationEvents(organization), options);
        }

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        [ManualRoute("GET", "/users/{username}/received_events")]
        public Task<IReadOnlyList<Activity>> GetAllUserReceived(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

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
        [ManualRoute("GET", "/users/{username}/received_events")]
        public Task<IReadOnlyList<Activity>> GetAllUserReceived(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.ReceivedEvents(user), options);
        }

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        [ManualRoute("GET", "/users/{username}/received_events/public")]
        public Task<IReadOnlyList<Activity>> GetAllUserReceivedPublic(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

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
        [ManualRoute("GET", "/users/{username}/received_events/public")]
        public Task<IReadOnlyList<Activity>> GetAllUserReceivedPublic(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.ReceivedEvents(user, true), options);
        }

        /// <summary>
        /// Gets all the events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        [ManualRoute("GET", "/users/{username}/events")]
        public Task<IReadOnlyList<Activity>> GetAllUserPerformed(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

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
        [ManualRoute("GET", "/users/{username}/events")]
        public Task<IReadOnlyList<Activity>> GetAllUserPerformed(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.PerformedEvents(user), options);
        }

        /// <summary>
        /// Gets all the public events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        [ManualRoute("GET", "/users/{username}/events/public")]
        public Task<IReadOnlyList<Activity>> GetAllUserPerformedPublic(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

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
        [ManualRoute("GET", "/users/{username}/events/public")]
        public Task<IReadOnlyList<Activity>> GetAllUserPerformedPublic(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.PerformedEvents(user, true), options);
        }

        /// <summary>
        /// Gets all the events that are associated with an organization.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-for-an-organization
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="organization">The name of the organization</param>
        [ManualRoute("GET", "/users/{username}/events/orgs/{org}")]
        public Task<IReadOnlyList<Activity>> GetAllForAnOrganization(string user, string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

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
        [ManualRoute("GET", "/users/{username}/events/orgs/{org}")]
        public Task<IReadOnlyList<Activity>> GetAllForAnOrganization(string user, string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Activity>(ApiUrls.OrganizationEvents(user, organization), options);
        }
    }
}
