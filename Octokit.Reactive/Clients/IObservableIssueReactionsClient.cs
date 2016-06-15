using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Reactions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/reactions/">Reactions API documentation</a> for more information.
    /// </remarks>
    public interface IObservableIssueReactionsClient
    {
        /// <summary>
        /// Creates a reaction for a specified Issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue id</param>
        /// <param name="reaction">The reaction to create </param>
        /// <returns>An <see cref="IObservable{T}"/> representing created <see cref="Reaction"/> for a specified issue.</returns>
        IObservable<Reaction> Create(string owner, string name, int number, NewReaction reaction);

        /// <summary>
        /// List reactions for a specified Issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue id</param>        
        /// <returns>An <see cref="IObservable{T}"/> representing <see cref="Reaction"/>s for a specified issue.</returns>
        IObservable<Reaction> GetAll(string owner, string name, int number);
    }
}
