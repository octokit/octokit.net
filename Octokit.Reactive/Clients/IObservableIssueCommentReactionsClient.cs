using System;

namespace Octokit.Reactive
{
    public interface IObservableIssueCommentReactionsClient
    {
        /// <summary>
        /// Creates a reaction for an specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create </param>
        /// <returns></returns>
        IObservable<Reaction> Create(string owner, string name, int number, NewReaction reaction);

        /// <summary>
        /// List reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns></returns>
        IObservable<Reaction> GetAll(string owner, string name, int number);
    }
}
