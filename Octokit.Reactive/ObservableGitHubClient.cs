using System;
using System.Net.Http.Headers;
using Octokit.Reactive.Clients;

namespace Octokit.Reactive
{
    public class ObservableGitHubClient : IObservableGitHubClient
    {
        readonly IGitHubClient _gitHubClient;

        public ObservableGitHubClient(ProductHeaderValue productInformation)
            : this(new GitHubClient(productInformation))
        {
        }

        public ObservableGitHubClient(ProductHeaderValue productInformation, ICredentialStore credentialStore)
            : this(new GitHubClient(productInformation, credentialStore))
        {
        }

        public ObservableGitHubClient(ProductHeaderValue productInformation, Uri baseAddress)
            : this(new GitHubClient(productInformation, baseAddress))
        {
        }

        public ObservableGitHubClient(ProductHeaderValue productInformation, ICredentialStore credentialStore, Uri baseAddress)
            : this(new GitHubClient(productInformation, credentialStore, baseAddress))
        {
        }

        public ObservableGitHubClient(IGitHubClient gitHubClient)
        {
            Ensure.ArgumentNotNull(gitHubClient, "githubClient");

            _gitHubClient = gitHubClient;
            Authorization = new ObservableAuthorizationsClient(gitHubClient);
            Activity = new ObservableActivitiesClient(gitHubClient);
            Blob = new ObservableBlobClient(gitHubClient);
            Miscellaneous = new ObservableMiscellaneousClient(gitHubClient.Miscellaneous);
            Notification = new ObservableNotificationsClient(gitHubClient);
            Organization = new ObservableOrganizationsClient(gitHubClient);
            Repository = new ObservableRepositoriesClient(gitHubClient);
            SshKey = new ObservableSshKeysClient(gitHubClient);
            User = new ObservableUsersClient(gitHubClient);
            Release = new ObservableReleasesClient(gitHubClient);
            GitDatabase = new ObservableGitDatabaseClient(gitHubClient);
        }

        public IConnection Connection
        {
            get { return _gitHubClient.Connection; }
        }

        public IObservableAuthorizationsClient Authorization { get; private set; }
        public IObservableActivitiesClient Activity { get; private set; }
        public IObservableBlobClient Blob { get; private set; }
        public IObservableMiscellaneousClient Miscellaneous { get; private set; }
        public IObservableNotificationsClient Notification { get; private set; }
        public IObservableOrganizationsClient Organization { get; private set; }
        public IObservableRepositoriesClient Repository { get; private set; }
        public IObservableReleasesClient Release { get; private set; }
        public IObservableSshKeysClient SshKey { get; private set; }
        public IObservableUsersClient User { get; private set; }
        public IObservableGitDatabaseClient GitDatabase { get; private set; }
    }
}
