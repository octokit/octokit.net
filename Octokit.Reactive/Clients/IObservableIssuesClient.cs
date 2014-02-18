using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
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
        /// Gets a single Issue by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#get-a-single-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<Issue> Get(string owner, string name, int number);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <returns></returns>
        IObservable<Issue> GetAllForCurrent();

        /// <summary>
        /// Gets all issues across all the authenticated user’s visible repositories including owned repositories, 
        /// member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <returns></returns>
        IObservable<Issue> GetAllForCurrent(IssueRequest request);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across owned and member repositories for the
        /// authenticated user.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <returns></returns>
        IObservable<Issue> GetAllForOwnedAndMemberRepositories();

        /// <summary>
        /// Gets all issues across owned and member repositories for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <returns></returns>
        IObservable<Issue> GetAllForOwnedAndMemberRepositories(IssueRequest request);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        IObservable<Issue> GetAllForOrganization(string organization);

        /// <summary>
        /// Gets all issues for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <returns></returns>
        IObservable<Issue> GetAllForOrganization(string organization, IssueRequest request);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        IObservable<Issue> GetForRepository(string owner, string name);

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <returns></returns>
        IObservable<Issue> GetForRepository(string owner, string name, RepositoryIssueRequest request);

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newIssue">A <see cref="NewIssue"/> instance describing the new issue to create</param>
        /// <returns></returns>
        IObservable<Issue> Create(string owner, string name, NewIssue newIssue);

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="issueUpdate">An <see cref="IssueUpdate"/> instance describing the changes to make to the issue
        /// </param>
        /// <returns></returns>
        IObservable<Issue> Update(string owner, string name, int number, IssueUpdate issueUpdate);
    }
}
