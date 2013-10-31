using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableIssuesClient : IObservableIssuesClient
    {
        readonly IIssuesClient _client;

        public IObservableAssigneesClient Assignee { get; private set; }

        public ObservableIssuesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Issue;
            Assignee = new ObservableAssigneesClient(client);
        }

        /// <summary>
        /// Gets a single Issue by number./// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#get-a-single-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public IObservable<Issue> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across all the authenticated user’s visible
        /// repositories including owned repositories, member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <returns></returns>
        public IObservable<IReadOnlyList<Issue>> GetAllForCurrent()
        {
            return GetAllForCurrent(new IssueRequest());
        }

        /// <summary>
        /// Gets all issues across all the authenticated user’s visible repositories including owned repositories, 
        /// member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <returns></returns>
        public IObservable<IReadOnlyList<Issue>> GetAllForCurrent(IssueRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return _client.GetAllForCurrent(request).ToObservable();
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user across owned and member repositories for the
        /// authenticated user.
        /// </summary>
        /// <remarks>
        /// Issues are sorted by the create date descending.
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <returns></returns>
        public IObservable<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories()
        {
            return GetAllForOwnedAndMemberRepositories(new IssueRequest());
        }

        /// <summary>
        /// Gets all issues across owned and member repositories for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <returns></returns>
        public IObservable<IReadOnlyList<Issue>> GetAllForOwnedAndMemberRepositories(IssueRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return _client.GetAllForOwnedAndMemberRepositories(request).ToObservable();
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <returns></returns>
        public IObservable<IReadOnlyList<Issue>> GetAllForOrganization(string organization)
        {
            return GetAllForOrganization(organization, new IssueRequest());
        }

        /// <summary>
        /// Gets all issues for a given organization for the authenticated user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter and sort the list of issues returned</param>
        /// <returns></returns>
        public IObservable<IReadOnlyList<Issue>> GetAllForOrganization(string organization, IssueRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNull(request, "request");

            return _client.GetAllForOrganization(organization, request).ToObservable();
        }

        /// <summary>
        /// Gets all open issues assigned to the authenticated user for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/#list-issues-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<IReadOnlyList<Issue>> GetForRepository(string owner, string name)
        {
            return GetForRepository(owner, name, new RepositoryIssueRequest());
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
        /// <returns></returns>
        public IObservable<IReadOnlyList<Issue>> GetForRepository(string owner, string name, RepositoryIssueRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return _client.GetForRepository(owner, name, request).ToObservable();
        }

        /// <summary>
        /// Creates an issue for the specified repository. Any user with pull access to a repository can create an
        /// issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/#create-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newIssue">A <see cref="NewIssue"/> instance describing the new issue to create</param>
        /// <returns></returns>
        public IObservable<Issue> Create(string owner, string name, NewIssue newIssue)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newIssue, "newIssue");

            return _client.Create(owner, name, newIssue).ToObservable();
        }

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
        public IObservable<Issue> Update(string owner, string name, int number, IssueUpdate issueUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(issueUpdate, "issueUpdate");

            return _client.Update(owner, name, number, issueUpdate).ToObservable();
        }
    }
}
