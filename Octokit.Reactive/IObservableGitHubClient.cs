﻿namespace Octokit.Reactive
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
        IObservableReleasesClient Release { get; }
        IObservableSshKeysClient SshKey { get; }
        IObservableUsersClient User { get; }
        IObservableNotificationsClient Notification { get; }
        IObservableGitDatabaseClient GitDatabase { get; }
        IObservableSearchClient Search { get; }
    }
}