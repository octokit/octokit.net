namespace Octokit.Reactive
{
    public interface IObservableGitHubClient
    {
        IConnection Connection { get; }

        IObservableAuthorizationsClient Authorization { get; }
        IObservableActivitiesClient Activity { get; }
        IObservableBlobClient Blob { get; }
        IObservableMiscellaneousClient Miscellaneous { get; }
        IObservableOrganizationsClient Organization { get; }
        IObservableRepositoriesClient Repository { get; }
        IObservableSshKeysClient SshKey { get; }
        IObservableUsersClient User { get; }
        IObservableGitDatabaseClient GitDatabase { get; }
        IObservableTreesClient Tree { get; }
        IObservableGistsClient Gist { get; }
    }
}