using System;

namespace Octokit
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public interface IGitHubClient : IApiInfoProvider
    {
        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        IConnection Connection { get; }

        /// <summary>
        /// Access GitHub's Authorization API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/oauth_authorizations/
        /// </remarks>
        IAuthorizationsClient Authorization { get; }

        /// <summary>
        /// Access GitHub's Activity API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/activity/
        /// </remarks>
        IActivitiesClient Activity { get; }

        /// <summary>
        /// Access GitHub's Issue API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/issues/
        /// </remarks>
        IIssuesClient Issue { get; }

        /// <summary>
        /// Access GitHub's Miscellaneous API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/misc/
        /// </remarks>
        IMiscellaneousClient Miscellaneous { get; }

        /// <summary>
        /// Access GitHub's OAuth API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/oauth/
        /// </remarks>
        IOauthClient Oauth { get; }

        /// <summary>
        /// Access GitHub's Organizations API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/orgs/
        /// </remarks>
        IOrganizationsClient Organization { get; }

        /// <summary>
        /// Access GitHub's Pull Requests API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/pulls/
        /// </remarks>
        IPullRequestsClient PullRequest { get; }

        /// <summary>
        /// Access GitHub's Repositories API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/repos/
        /// </remarks>
        IRepositoriesClient Repository { get; }

        /// <summary>
        /// Access GitHub's Gists API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/gists/
        /// </remarks>
        IGistsClient Gist { get; }

        // TODO: this should be under Repositories to align with the API docs
        /// <summary>
        /// Access GitHub's Releases API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/repos/releases/
        /// </remarks>
        [Obsolete("Use Repository.Release instead")]
        IReleasesClient Release { get; }

        // TODO: this should be under Users to align with the API docs
        // TODO: this should be named PublicKeys to align with the API docs
        /// <summary>
        /// Access GitHub's Public Keys API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/users/keys/
        /// </remarks>
        ISshKeysClient SshKey { get; }

        /// <summary>
        /// Access GitHub's Users API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/users/
        /// </remarks>
        IUsersClient User { get; }

        // TODO: this should be under Activities to align with the API docs
        /// <summary>
        /// Access GitHub's Notifications API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/activity/notifications/
        /// </remarks>
        [System.Obsolete("Notifications are now available under the Activities client. This will be removed in a future update.")]
        INotificationsClient Notification { get; }

        /// <summary>
        /// Access GitHub's Git Data API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/git/
        /// </remarks>
        [Obsolete("Use Git instead")]
        IGitDatabaseClient GitDatabase { get; }

        /// <summary>
        /// Access GitHub's Git Data API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/git/
        /// </remarks>
        IGitDatabaseClient Git { get; }

        /// <summary>
        /// Access GitHub's Search API.
        /// </summary>
        /// <remarks>
        /// Refer to the API docmentation for more information: https://developer.github.com/v3/search/
        /// </remarks>
        ISearchClient Search { get; }

        /// <summary>
        /// Access GitHub's Enterprise API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/enterprise/
        /// </remarks>
        IEnterpriseClient Enterprise { get; }
    }
}
