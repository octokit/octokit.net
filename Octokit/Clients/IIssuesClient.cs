using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issues API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/">Issues API documentation</a> for more information.
    /// </remarks>
    public interface IIssuesClient
    {
        /// <summary>
        /// Client for managing assignees.
        /// </summary>
        IAssigneesClient Assignee { get; }

        /// <summary>
        /// Client for reading various event information associated with issues/pull requests.  
        /// This is useful both for display on issue/pull request information pages and also to 
        /// determine who should be notified of comments.
        /// </summary>
        IIssuesEventsClient Events { get; }

        /// <summary>
        /// Client for managing milestones.
        /// </summary>
        IMilestonesClient Milestone { get; }

        /// <summary>
        /// Client for managing labels.
        /// </summary>
        IIssuesLabelsClient Labels { get; }

        /// <summary>
        /// Client for managing comments.
        /// </summary>
        IIssueCommentsClient Comment { get; }

        IIssueTimelineClient Timeline { get; }

        ILockUnlockClient LockUnlock { get; }

        /// <summary>
        /// Gets a single Issue by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#get-a-single-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        Task<Issue> Get(string owner, string name, int number);

        /// <summary>
        /// Gets a single Issue by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#get-a-single-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        Task<Issue> Get(long repositoryId, int number);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        Task<IReadOnlyList<Issue>> GetAllForCurrent();

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        Task<IReadOnlyList<Issue>> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Gets all issues across all the authenticated user’s visible repositories including owned repositories, 
        /// member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        Task<IReadOnlyList<Issue>> GetAllForCurrent(IssueRequest request);

        /// <summary>
        /// Gets all issues across all the authenticated user’s visible repositories including owned repositories, 
        /// member repositories, and organization repositories.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        Task<IReadOnlyList<Issue>> GetAllForCurrent(IssueRequest request, ApiOptions options);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across owned and member repositories for the
        /// authenticated user.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        Task<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories();

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across owned and member repositories for the
        /// authenticated user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        Task<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories(ApiOptions options);

        /// <summary>
        /// Gets all issues across owned and member repositories for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        Task<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories(IssueRequest request);

        /// <summary>
        /// Gets all issues across owned and member repositories for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories(IssueRequest request, ApiOptions options);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        Task<IReadOnlyList<Issue>> GetAllForOrganization(string organization);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Issue>> GetAllForOrganization(string organization, ApiOptions options);

        /// <summary>
        /// Gets all issues for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        Task<IReadOnlyList<Issue>> GetAllForOrganization(string organization, IssueRequest request);

        /// <summary>
        /// Gets all issues for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Issue>> GetAllForOrganization(string organization, IssueRequest request, ApiOptions options);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<IReadOnlyList<Issue>> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<IReadOnlyList<Issue>> GetAllForRepository(long repositoryId);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Issue>> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Issue>> GetAllForRepository(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        Task<IReadOnlyList<Issue>> GetAllForRepository(string owner, string name, RepositoryIssueRequest request);

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        Task<IReadOnlyList<Issue>> GetAllForRepository(long repositoryId, RepositoryIssueRequest request);

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
        Task<IReadOnlyList<Issue>> GetAllForRepository(string owner, string name, RepositoryIssueRequest request, ApiOptions options);

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Issue>> GetAllForRepository(long repositoryId, RepositoryIssueRequest request, ApiOptions options);

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newIssue">A <see cref="NewIssue"/> instance describing the new issue to create</param>
        Task<Issue> Create(string owner, string name, NewIssue newIssue);

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newIssue">A <see cref="NewIssue"/> instance describing the new issue to create</param>
        Task<Issue> Create(long repositoryId, NewIssue newIssue);

        /// <summary>
        /// Updates an issue for the specified repository. Any user with pull access to a repository can update an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#edit-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="issueUpdate">An <see cref="IssueUpdate"/> instance describing the changes to make to the issue
        /// </param>
        Task<Issue> Update(string owner, string name, int number, IssueUpdate issueUpdate);

        /// <summary>
        /// Updates an issue for the specified repository. Any user with pull access to a repository can update an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#edit-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="issueUpdate">An <see cref="IssueUpdate"/> instance describing the changes to make to the issue
        /// </param>
        Task<Issue> Update(long repositoryId, int number, IssueUpdate issueUpdate);

    }
}