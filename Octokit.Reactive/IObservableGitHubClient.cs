using System;

namespace Octokit.Reactive
{
    public interface IObservableGitHubClient : IApiInfoProvider
    {
        IConnection Connection { get; }

        /// <summary>
        /// Sets the timeout for the connection between the client and the server.
        /// GitHub will terminate the request if it takes more than 10 seconds to process the request
        /// /// Useful to set a specific timeout for lengthy operations, such as uploading release assets
        /// </summary>
        /// <remarks>
        /// See more information here: https://technet.microsoft.com/library/system.net.http.httpclient.timeout(v=vs.110).aspx
        /// </remarks>
        /// <param name="timeout">The Timeout value</param>
        void SetRequestTimeout(TimeSpan timeout);

        IObservableAuthorizationsClient Authorization { get; }
        IObservableActivitiesClient Activity { get; }
        IObservableGitHubAppsClient GitHubApps { get; }
        IObservableIssuesClient Issue { get; }
        IObservableMiscellaneousClient Miscellaneous { get; }
        IObservableOauthClient Oauth { get; }
        IObservableOrganizationsClient Organization { get; }
        IObservablePullRequestsClient PullRequest { get; }
        IObservableRepositoriesClient Repository { get; }
        IObservableGistsClient Gist { get; }
        IObservableUsersClient User { get; }
        IObservableGitDatabaseClient Git { get; }
        IObservableSearchClient Search { get; }
        IObservableEnterpriseClient Enterprise { get; }
        IObservableMigrationClient Migration { get; }
        IObservableReactionsClient Reaction { get; }
        IObservableChecksClient Check { get; }
        IObservablePackagesClient Packages{ get; }
        IObservableEmojisClient Emojis { get; }
        IObservableMarkdownClient Markdown { get; }
        IObservableGitIgnoreClient GitIgnore { get; }
        IObservableLicensesClient Licenses { get; }
        IObservableRateLimitClient RateLimit { get; }
        IObservableMetaClient Meta { get; }
        IObservableActionsClient Actions { get; }
    }
}
