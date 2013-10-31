using System;
using System.Collections.Generic;

namespace Octokit.Reactive
{
    public interface IObservableAssigneesClient
    {
        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        IObservable<User> GetForRepository(string owner, string name);

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="assignee">Username of the prospective assignee</param>
        /// <returns></returns>
        IObservable<bool> CheckAssignee(string owner, string name, string assignee);
    }
}
