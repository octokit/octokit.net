using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Assignees API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/assignees/">Issue Assignees API documentation</a> for more information.
    /// </remarks>
    public interface IObservableAssigneesClient
    {
        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<User> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<User> GetAllForRepository(long repositoryId);

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">The options to change API's behaviour.</param>
        IObservable<User> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">The options to change API's behaviour.</param>
        IObservable<User> GetAllForRepository(long repositoryId, ApiOptions options);

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="assignee">Username of the prospective assignee</param>
        IObservable<bool> CheckAssignee(string owner, string name, string assignee);

        /// <summary>
        /// Add assignees to a specified Issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="assignees">List of names of assignees to add</param>
        /// <returns></returns>
        IObservable<Issue> AddAssignees(string owner, string name, int issueNumber, AssigneesUpdate assignees);

        /// <summary>
        /// Remove assignees from a specified Issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="assignees">List of assignees to remove </param>
        /// <returns></returns>
        IObservable<Issue> RemoveAssignees(string owner, string name, int issueNumber, AssigneesUpdate assignees);

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="assignee">Username of the prospective assignee</param>
        IObservable<bool> CheckAssignee(long repositoryId, string assignee);
    }
}
