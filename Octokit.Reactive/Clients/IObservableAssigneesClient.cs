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
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s representing assignees of specified repository.</returns>
        IObservable<User> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s representing assignees of specified repository.</returns>
        IObservable<User> GetAllForRepository(int repositoryId);

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">The options to change API's behaviour.</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s representing assignees of specified repository.</returns>
        IObservable<User> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="options">The options to change API's behaviour.</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s representing assignees of specified repository.</returns>
        IObservable<User> GetAllForRepository(int repositoryId, ApiOptions options);

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="assignee">Username of the prospective assignee</param>
        /// <returns>A <see cref="IObservable{Bool}"/> of <see cref="bool"/> representing is a user is an assignee for a repository.</returns>
        IObservable<bool> CheckAssignee(string owner, string name, string assignee);

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="assignee">Username of the prospective assignee</param>
        /// <returns>A <see cref="IObservable{Bool}"/> of <see cref="bool"/> representing is a user is an assignee for a repository.</returns>
        IObservable<bool> CheckAssignee(int repositoryId, string assignee);
    }
}
