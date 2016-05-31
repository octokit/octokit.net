using System;

namespace Octokit.Reactive
{
    public interface IObservableIssuesReactionsClient
    {
        /// <summary>
        /// Creates a reaction for an specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-reaction-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue id</param>
        /// <param name="reaction">The reaction to create </param>
        /// <returns></returns>
        IObservable<Reaction> CreateReaction(string owner, string name, int number, NewReaction reaction);

        /// <summary>
        /// List reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-reactions-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue id</param>        
        /// <returns></returns>
        IObservable<Reaction> GetAll(string owner, string name, int number);
    }
}
