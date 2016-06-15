using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Reactions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/reactions">Reactions API documentation</a> for more information.
    /// </remarks>
    public interface IObservableCommitCommentReactionsClient
    {
        /// <summary>
        /// Creates a reaction for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create </param>
        /// <returns>An <see cref="IObservable{Reaction}"/> of <see cref="Reaction"/> representing created reaction for specified comment id.</returns>
        IObservable<Reaction> Create(string owner, string name, int number, NewReaction reaction);

        /// <summary>
        /// Creates a reaction for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create </param>
        /// <returns>An <see cref="IObservable{Reaction}"/> of <see cref="Reaction"/> representing created reaction for specified comment id.</returns>
        IObservable<Reaction> Create(int repositoryId, int number, NewReaction reaction);

        /// <summary>
        /// List reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns>An <see cref="IObservable{Reaction}"/> of <see cref="Reaction"/>s representing all reactions for specified comment id.</returns>
        IObservable<Reaction> GetAll(string owner, string name, int number);
        
        /// <summary>
        /// List reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns>An <see cref="IObservable{Reaction}"/> of <see cref="Reaction"/>s representing all reactions for specified comment id.</returns>
        IObservable<Reaction> GetAll(int repositoryId, int number);
    }
}
