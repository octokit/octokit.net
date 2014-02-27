using System;
namespace Octokit.Reactive
{
    public interface IObservableGitHubClient: IDisposable
    {
        IConnection Connection { get; }

        IObservableAuthorizationsClient Authorization { get; }
        IObservableActivitiesClient Activity { get; }
        IObservableIssuesClient Issue { get; }
        IObservableMiscellaneousClient Miscellaneous { get; }
        IObservableOrganizationsClient Organization { get; }
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