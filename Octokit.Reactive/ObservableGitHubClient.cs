using System;

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
            Ensure.ArgumentNotNull(gitHubClient, nameof(gitHubClient));

            _gitHubClient = gitHubClient;
            Authorization = new ObservableAuthorizationsClient(gitHubClient);
            Activity = new ObservableActivitiesClient(gitHubClient);
            GitHubApps = new ObservableGitHubAppsClient(gitHubClient);
            Issue = new ObservableIssuesClient(gitHubClient);
            Miscellaneous = new ObservableMiscellaneousClient(gitHubClient);
            Oauth = new ObservableOauthClient(gitHubClient);
            Organization = new ObservableOrganizationsClient(gitHubClient);
            PullRequest = new ObservablePullRequestsClient(gitHubClient);
            PullRequestReview = new ObservablePullRequestReviewsClient(gitHubClient);
            Repository = new ObservableRepositoriesClient(gitHubClient);
            User = new ObservableUsersClient(gitHubClient);
            Git = new ObservableGitDatabaseClient(gitHubClient);
            Gist = new ObservableGistsClient(gitHubClient);
            Search = new ObservableSearchClient(gitHubClient);
            Enterprise = new ObservableEnterpriseClient(gitHubClient);
            Migration = new ObservableMigrationClient(gitHubClient);
            Reaction = new ObservableReactionsClient(gitHubClient);
            Check = new ObservableChecksClient(gitHubClient);
            Packages = new ObservablePackagesClient(gitHubClient);
            Emojis = new ObservableEmojisClient(gitHubClient);
            Markdown = new ObservableMarkdownClient(gitHubClient);
            GitIgnore = new ObservableGitIgnoreClient(gitHubClient);
            Licenses = new ObservableLicensesClient(gitHubClient);
            RateLimit = new ObservableRateLimitClient(gitHubClient);
            Meta = new ObservableMetaClient(gitHubClient);
            Actions = new ObservableActionsClient(gitHubClient);
        }

        public IConnection Connection
        {
            get { return _gitHubClient.Connection; }
        }

        /// <summary>
        /// Sets the timeout for the connection between the client and the server.
        /// GitHub will terminate the request if it takes more than 10 seconds to process the request
        /// Useful to set a specific timeout for lengthy operations, such as uploading release assets
        /// </summary>
        /// <remarks>
        /// See more information here: https://technet.microsoft.com/library/system.net.http.httpclient.timeout(v=vs.110).aspx
        /// </remarks>
        /// <param name="timeout">The Timeout value</param>
        public void SetRequestTimeout(TimeSpan timeout)
        {
            _gitHubClient.SetRequestTimeout(timeout);
        }

        public IObservableAuthorizationsClient Authorization { get; private set; }
        public IObservableActivitiesClient Activity { get; private set; }
        public IObservableGitHubAppsClient GitHubApps { get; private set; }
        public IObservableIssuesClient Issue { get; private set; }
        public IObservableMiscellaneousClient Miscellaneous { get; private set; }
        public IObservableOauthClient Oauth { get; private set; }
        public IObservableOrganizationsClient Organization { get; private set; }
        public IObservablePullRequestsClient PullRequest { get; private set; }
        public IObservablePullRequestReviewsClient PullRequestReview { get; private set; }
        public IObservableRepositoriesClient Repository { get; private set; }
        public IObservableGistsClient Gist { get; private set; }
        public IObservableUsersClient User { get; private set; }
        public IObservableGitDatabaseClient Git { get; private set; }
        public IObservableSearchClient Search { get; private set; }
        public IObservableEnterpriseClient Enterprise { get; private set; }
        public IObservableMigrationClient Migration { get; private set; }
        public IObservableReactionsClient Reaction { get; private set; }
        public IObservableChecksClient Check { get; private set; }
        public IObservablePackagesClient Packages { get; private set; }
        public IObservableEmojisClient Emojis { get; private set; }
        public IObservableMarkdownClient Markdown { get; private set; }
        public IObservableGitIgnoreClient GitIgnore { get; private set; }
        public IObservableLicensesClient Licenses { get; private set; }
        public IObservableRateLimitClient RateLimit { get; private set; }
        public IObservableMetaClient Meta { get; private set; }
        public IObservableActionsClient Actions { get; private set; }

        /// <summary>
        /// Gets the latest API Info - this will be null if no API calls have been made
        /// </summary>
        /// <returns><seealso cref="ApiInfo"/> representing the information returned as part of an Api call</returns>
        public ApiInfo GetLastApiInfo()
        {
            return _gitHubClient.Connection.GetLastApiInfo();
        }
    }
}
