using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issues API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/">Issues API documentation</a> for more information.
    /// </remarks>
    public interface IObservableIssuesClient
    {
        /// <summary>
        /// Client for managing assignees.
        /// </summary>
        IObservableAssigneesClient Assignee { get; }

        /// <summary>
        /// Client for managing milestones.
        /// </summary>
        IObservableMilestonesClient Milestone { get; }

        /// <summary>
        /// Client for reading various event information associated with issues/pull requests.
        /// This is useful both for display on issue/pull request information pages and also to
        /// determine who should be notified of comments.
        /// </summary>
        IObservableIssuesEventsClient Events { get; }

        /// <summary>
        /// Client for managing labels.
        /// </summary>
        IObservableIssuesLabelsClient Labels { get; }

        /// <summary>
        /// Client for managing comments.
        /// </summary>
        IObservableIssueCommentsClient Comment { get; }

        /// <summary>
        /// Client for reading the timeline of events for an issue
        /// </summary>
        IObservableIssueTimelineClient Timeline { get; }

        /// <summary>
        /// Client for locking/unlocking conversation on an issue
        /// </summary>
        IObservableLockUnlockClient LockUnlock { get; }

        /// <summary>
        /// Gets a single Issue by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#get-a-single-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        IObservable<Issue> Get(string owner, string name, long issueNumber);

        /// <summary>
        /// Gets a single Issue by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#get-a-single-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        IObservable<Issue> Get(long repositoryId, long issueNumber);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        IObservable<Issue> GetAllForCurrent();

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        IObservable<Issue> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Gets all issues across all the authenticated user’s visible repositories including owned repositories,
        /// member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        IObservable<Issue> GetAllForCurrent(IssueRequest request);

        /// <summary>
        /// Gets all issues across all the authenticated user’s visible repositories including owned repositories,
        /// member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Issue> GetAllForCurrent(IssueRequest request, ApiOptions options);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across owned and member repositories for the
        /// authenticated user.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        IObservable<Issue> GetAllForOwnedAndMemberRepositories();

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across owned and member repositories for the
        /// authenticated user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        IObservable<Issue> GetAllForOwnedAndMemberRepositories(ApiOptions options);

        /// <summary>
        /// Gets all issues across owned and member repositories for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        IObservable<Issue> GetAllForOwnedAndMemberRepositories(IssueRequest request);

        /// <summary>
        /// Gets all issues across owned and member repositories for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Issue> GetAllForOwnedAndMemberRepositories(IssueRequest request, ApiOptions options);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        IObservable<Issue> GetAllForOrganization(string organization);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Issue> GetAllForOrganization(string organization, ApiOptions options);

        /// <summary>
        /// Gets all issues for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        IObservable<Issue> GetAllForOrganization(string organization, IssueRequest request);

        /// <summary>
        /// Gets all issues for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Issue> GetAllForOrganization(string organization, IssueRequest request, ApiOptions options);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<Issue> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<Issue> GetAllForRepository(long repositoryId);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Issue> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Issue> GetAllForRepository(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        IObservable<Issue> GetAllForRepository(string owner, string name, RepositoryIssueRequest request);

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        IObservable<Issue> GetAllForRepository(long repositoryId, RepositoryIssueRequest request);

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Issue> GetAllForRepository(string owner, string name, RepositoryIssueRequest request, ApiOptions options);

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Issue> GetAllForRepository(long repositoryId, RepositoryIssueRequest request, ApiOptions options);

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newIssue">A <see cref="NewIssue"/> instance describing the new issue to create</param>
        IObservable<Issue> Create(string owner, string name, NewIssue newIssue);

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newIssue">A <see cref="NewIssue"/> instance describing the new issue to create</param>
        IObservable<Issue> Create(long repositoryId, NewIssue newIssue);

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="issueUpdate">An <see cref="IssueUpdate"/> instance describing the changes to make to the issue
        /// </param>
        IObservable<Issue> Update(string owner, string name, long issueNumber, IssueUpdate issueUpdate);

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="issueUpdate">An <see cref="IssueUpdate"/> instance describing the changes to make to the issue
        /// </param>
        IObservable<Issue> Update(long repositoryId, long issueNumber, IssueUpdate issueUpdate);
    }
}
