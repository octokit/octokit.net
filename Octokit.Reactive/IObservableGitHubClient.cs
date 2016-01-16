using System;

namespace Octokit.Reactive
{
    public interface IObservableGitHubClient : IApiInfoProvider
    {
        IConnection Connection { get; }

        IObservableAuthorizationsClient Authorization { get; }
        IObservableActivitiesClient Activity { get; }
        IObservableIssuesClient Issue { get; }
        IObservableMiscellaneousClient Miscellaneous { get; }
        IObservableOauthClient Oauth { get; }
        IObservableOrganizationsClient Organization { get; }
        IObservablePullRequestsClient PullRequest { get; }
        IObservableRepositoriesClient Repository { get; }
        IObservableGistsClient Gist { get; }
        [Obsolete("Use Repository.Release instead")]
        IObservableReleasesClient Release { get; }
        IObservableSshKeysClient SshKey { get; }
        IObservableUsersClient User { get; }
        [System.Obsolete("Notifications are now available under the Activities client. This will be removed in a future update.")]
        IObservableNotificationsClient Notification { get; }
        IObservableGitDatabaseClient Git { get; }
        [Obsolete("Use Git instead")]
        IObservableGitDatabaseClient GitDatabase { get; }
        IObservableSearchClient Search { get; }
        IObservableEnterpriseClient Enterprise { get; }
    }
}