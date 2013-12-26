﻿using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// A Client for the GitHub API v3. You can read more about the api here: http://developer.github.com.
    /// </summary>
    public interface IGitHubClient
    {
        IConnection Connection { get; }

        IAuthorizationsClient Authorization { get; }
        IActivitiesClient Activity { get; }
        IBlobsClient Blob { get; }
        IIssuesClient Issue { get; }
        IMiscellaneousClient Miscellaneous { get; }
        IOrganizationsClient Organization { get; }
        IRepositoriesClient Repository { get; }
        IGistsClient Gist { get; }
        IReleasesClient Release { get; }
        ISshKeysClient SshKey { get; }
        IUsersClient User { get; }
        INotificationsClient Notification { get; }
        IGitDatabaseClient GitDatabase { get; }
        ITreesClient Tree { get; }
        ISearchClient Search { get; }
    }
}
