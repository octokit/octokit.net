using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableIssueCommentsClient
    {
        /// <summary>
        /// Gets a single Issue Comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue comment number</param>
        /// <returns>The <see cref="IssueComment"/>s for the specified Issue Comment.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<IssueComment> Get(string owner, string name, int number);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The list of <see cref="IssueComment"/>s for the specified Repository.</returns>
        IObservable<IssueComment> GetForRepository(string owner, string name);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>The list of <see cref="IssueComment"/>s for the specified Issue.</returns>
        IObservable<IssueComment> GetForIssue(string owner, string name, int number);

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="newComment">The text of the new comment</param>
        /// <returns>The <see cref="IssueComment"/> that was just created.</returns>
        IObservable<IssueComment> Create(string owner, string name, int number, string newComment);

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <returns>The <see cref="IssueComment"/> that was just updated.</returns>
        IObservable<IssueComment> Update(string owner, string name, int number, string commentUpdate);

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <returns></returns>
        IObservable<Unit> Delete(string owner, string name, int number);
    }
}
