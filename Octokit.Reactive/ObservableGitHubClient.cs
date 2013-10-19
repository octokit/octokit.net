using Octokit.Internal;

namespace Octokit.Reactive
{
    public class ObservableGitHubClient : IObservableGitHubClient
    {
        readonly IGitHubClient _gitHubClient;

        public ObservableGitHubClient(IGitHubClient gitHubClient)
        {
            Ensure.ArgumentNotNull(gitHubClient, "githubClient");

            _gitHubClient = gitHubClient;
            Authorization = new ObservableAuthorizationsClient(gitHubClient);
            Miscellaneous = new ObservableMiscellaneousClient(gitHubClient.Miscellaneous);
            Organization = new ObservableOrganizationsClient(gitHubClient);
            Repository = new ObservableRepositoriesClient(gitHubClient);
            SshKey = new ObservableSshKeysClient(gitHubClient);
            User = new ObservableUsersClient(gitHubClient);
            Release = new ObservableReleasesClient(gitHubClient);
        }

        public IConnection Connection
        {
            get { return _gitHubClient.Connection; }
        }

        public IObservableAuthorizationsClient Authorization { get; private set; }
        public IObservableMiscellaneousClient Miscellaneous { get; private set; }
        public IObservableOrganizationsClient Organization { get; private set; }
        public IObservableRepositoriesClient Repository { get; private set; }
        public IObservableReleasesClient Release { get; private set; }
        public IObservableSshKeysClient SshKey { get; private set; }
        public IObservableUsersClient User { get; private set; }
    }
}
