using Octokit.Reactive.Clients;

namespace Octokit.Reactive
{
    public class ObservableGitHubClient : IObservableGitHubClient
    {
        public ObservableGitHubClient(IGitHubClient gitHubClient)
        {
            Ensure.ArgumentNotNull(gitHubClient, "githubClient");
            
            Authorization = new ObservableAuthorizationsClient(gitHubClient.Authorization);
            AutoComplete = new ObservableAutoCompleteClient(gitHubClient.AutoComplete);
            Organization = new ObservableOrganizationsClient(gitHubClient.Organization);
            Repository = new ObservableRepositoriesClient(gitHubClient.Repository);
            SshKey = new ObservableSshKeysClient(gitHubClient.SshKey);
            User = new ObservableUsersClient(gitHubClient.User);
        }

        public IObservableAuthorizationsClient Authorization { get; private set; }
        public IObservableAutoCompleteClient AutoComplete { get; private set; }
        public IObservableOrganizationsClient Organization { get; private set; }
        public IObservableRepositoriesClient Repository { get; private set; }
        public IObservableSshKeysClient SshKey { get; private set; }
        public IObservableUsersClient User { get; private set; }
    }
}
