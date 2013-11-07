using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableCommitsClient
    {
        /// <summary>
        /// Gets a commit for a given repository by sha reference
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#get-a-commit
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">Tha sha reference of the commit</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<Commit> Get(string owner, string name, string reference);

        /// <summary>
        /// Create a commit for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#create-a-commit
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commit">The commit to create</param>
        /// <returns></returns>
        IObservable<Commit> Create(string owner, string name, NewCommit commit);            
    }
}