using System;
using System.Collections.Generic;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableRepoCollaboratorsClient
    {
        /// <summary>
        /// Gets all the available collaborators on this repo.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <returns></returns>
        IObservable<User> GetAll(string owner, string repo);

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        IObservable<bool> IsCollaborator(string owner, string repo, string user);

        /// <summary>
        /// Adds a user as a collaborator to a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        IObservable<Unit> Add(string owner, string repo, string user);

        /// <summary>
        /// Removes a user as a collaborator for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        IObservable<Unit> Delete(string owner, string repo, string user);
    }
}
