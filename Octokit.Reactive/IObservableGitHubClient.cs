namespace Octokit.Reactive
{
    public interface IObservableGitHubClient
    {
        IObservableAuthorizationsClient Authorization { get; }
        IObservableAutoCompleteClient AutoComplete { get; }
        IObservableOrganizationsClient Organization { get; }
        IObservableRepositoriesClient Repositories { get; }
        IObservableSshKeysClient SshKey { get; }
        IObservableUsersClient User { get; }
    }
}