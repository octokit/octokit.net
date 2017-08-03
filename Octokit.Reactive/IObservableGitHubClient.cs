using System;

namespace Octokit.Reactive
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
        IObservablePullRequestReviewClient PullRequestReview { get; }
        IObservableRepositoriesClient Repository { get; }
        IObservableGistsClient Gist { get; }
        IObservableUsersClient User { get; }
        IObservableGitDatabaseClient Git { get; }
        IObservableSearchClient Search { get; }
        IObservableEnterpriseClient Enterprise { get; }
        IObservableMigrationClient Migration { get; }
        IObservableReactionsClient Reaction { get; }
    }
}