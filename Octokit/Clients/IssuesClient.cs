using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issues API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/">Issues API documentation</a> for more information.
    /// </remarks>
    public class IssuesClient : ApiClient, IIssuesClient
    {
        /// <summary>
        /// Instantiates a new GitHub Issues API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public IssuesClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Assignee = new AssigneesClient(apiConnection);
            Events = new IssuesEventsClient(apiConnection);
            Labels = new IssuesLabelsClient(apiConnection);
            Milestone = new MilestonesClient(apiConnection);
            Comment = new IssueCommentsClient(apiConnection);
            Timeline = new IssueTimelineClient(apiConnection);
            LockUnlock = new LockUnlockClient(apiConnection);
        }

        /// <summary>
        /// Client for managing assignees.
        /// </summary>
        public IAssigneesClient Assignee { get; private set; }

        /// <summary>
        /// Client for reading various event information associated with issues/pull requests.
        /// This is useful both for display on issue/pull request information pages and also to
        /// determine who should be notified of comments.
        /// </summary>
        public IIssuesEventsClient Events { get; private set; }

        /// <summary>
        /// Client for managing labels.
        /// </summary>
        public IIssuesLabelsClient Labels { get; private set; }

        /// <summary>
        /// Client for managing milestones.
        /// </summary>
        public IMilestonesClient Milestone { get; private set; }

        /// <summary>
        /// Client for managing comments.
        /// </summary>
        public IIssueCommentsClient Comment { get; private set; }

        /// <summary>
        /// Client for reading the timeline of events for an issue
        /// </summary>
        public IIssueTimelineClient Timeline { get; private set; }

        /// <summary>
        /// Client for locking and unlocking a conversation on a Issue or Pull request
        /// </summary>
        public ILockUnlockClient LockUnlock { get; private set; }

        /// <summary>
        /// Gets a single Issue by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#get-a-single-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{issue_number}")]
        public Task<Issue> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<Issue>(ApiUrls.Issue(owner, name, number), null);
        }

        /// <summary>
        /// Gets a single Issue by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#get-a-single-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}")]
        public Task<Issue> Get(long repositoryId, int number)
        {
            return ApiConnection.Get<Issue>(ApiUrls.Issue(repositoryId, number), null);
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        [ManualRoute("GET", "/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        [ManualRoute("GET", "/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForCurrent(new IssueRequest(), options);
        }

        /// <summary>
        /// Gets all issues across all the authenticated user’s visible repositories including owned repositories,
        /// member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        [ManualRoute("GET", "/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForCurrent(IssueRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForCurrent(request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all issues across all the authenticated user’s visible repositories including owned repositories,
        /// member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForCurrent(IssueRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Issue>(ApiUrls.Issues(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across owned and member repositories for the
        /// authenticated user.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        [ManualRoute("GET", "/user/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories()
        {
            return GetAllForOwnedAndMemberRepositories(ApiOptions.None);
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across owned and member repositories for the
        /// authenticated user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        [ManualRoute("GET", "/user/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForOwnedAndMemberRepositories(new IssueRequest(), options);
        }

        /// <summary>
        /// Gets all issues across owned and member repositories for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        [ManualRoute("GET", "/user/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories(IssueRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForOwnedAndMemberRepositories(request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all issues across owned and member repositories for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/user/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories(IssueRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Issue>(ApiUrls.IssuesForOwnedAndMember(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        [ManualRoute("GET", "/orgs/{org}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForOrganization(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return GetAllForOrganization(organization, ApiOptions.None);
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/orgs/{org}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForOrganization(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForOrganization(organization, new IssueRequest(), options);
        }

        /// <summary>
        /// Gets all issues for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        [ManualRoute("GET", "/orgs/{org}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForOrganization(string organization, IssueRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForOrganization(organization, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all issues for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/orgs/{org}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForOrganization(string organization, IssueRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Issue>(ApiUrls.Issues(organization), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, new RepositoryIssueRequest());
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, new RepositoryIssueRequest());
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(owner, name, new RepositoryIssueRequest(), options);
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(repositoryId, new RepositoryIssueRequest(), options);
        }

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForRepository(string owner, string name, RepositoryIssueRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        [ManualRoute("GET", "/repositories/{id}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForRepository(long repositoryId, RepositoryIssueRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForRepository(string owner, string name, RepositoryIssueRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Issue>(ApiUrls.Issues(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets issues for a repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues")]
        public Task<IReadOnlyList<Issue>> GetAllForRepository(long repositoryId, RepositoryIssueRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Issue>(ApiUrls.Issues(repositoryId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newIssue">A <see cref="NewIssue"/> instance describing the new issue to create</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues")]
        public Task<Issue> Create(string owner, string name, NewIssue newIssue)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newIssue, nameof(newIssue));

            return ApiConnection.Post<Issue>(ApiUrls.Issues(owner, name), newIssue);
        }

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newIssue">A <see cref="NewIssue"/> instance describing the new issue to create</param>
        [ManualRoute("POST", "/repositories/{id}/issues")]
        public Task<Issue> Create(long repositoryId, NewIssue newIssue)
        {
            Ensure.ArgumentNotNull(newIssue, nameof(newIssue));

            return ApiConnection.Post<Issue>(ApiUrls.Issues(repositoryId), newIssue);
        }

        /// <summary>
        /// Updates an issue for the specified repository. Issue owners and users with push access can edit an issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/issues/#edit-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="issueUpdate">An <see cref="IssueUpdate"/> instance describing the changes to make to the issue
        /// </param>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/issues/{issue_number}")]
        public Task<Issue> Update(string owner, string name, int number, IssueUpdate issueUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(issueUpdate, nameof(issueUpdate));

            return ApiConnection.Patch<Issue>(ApiUrls.Issue(owner, name, number), issueUpdate);
        }

        /// <summary>
        /// Updates an issue for the specified repository. Any user with pull access to a repository can update an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#edit-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="issueUpdate">An <see cref="IssueUpdate"/> instance describing the changes to make to the issue
        /// </param>
        [ManualRoute("PATCH", "/repositories/{id}/issues/{number}")]
        public Task<Issue> Update(long repositoryId, int number, IssueUpdate issueUpdate)
        {
            Ensure.ArgumentNotNull(issueUpdate, nameof(issueUpdate));

            return ApiConnection.Patch<Issue>(ApiUrls.Issue(repositoryId, number), issueUpdate);
        }        
    }
}
