namespace Octokit.Reactive
{
    public interface IObservableGitHubClient
    {
        IObservableAuthorizationsClient Authorization { get; }
        IObservableMiscellaneousClient Miscellaneous { get; }
        IObservableOrganizationsClient Organization { get; }
        IObservableRepositoriesClient Repository { get; }
        IObservableSshKeysClient SshKey { get; }
        IObservableUsersClient User { get; }
    }
}