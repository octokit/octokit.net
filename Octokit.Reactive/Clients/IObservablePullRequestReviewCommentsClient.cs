using System;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservablePullRequestReviewCommentsClient
    {
        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The list of <see cref="PullRequestReviewComment"/>s for the specified pull request</returns>
        IObservable<PullRequestReviewComment> GetAll(string owner, string name, int number);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The list of <see cref="PullRequestReviewComment"/>s for the specified repository</returns>
        IObservable<PullRequestReviewComment> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <returns>The list of <see cref="PullRequestReviewComment"/>s for the specified repository</returns>
        IObservable<PullRequestReviewComment> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request);

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <returns>The <see cref="PullRequestReviewComment"/></returns>
        IObservable<PullRequestReviewComment> GetComment(string owner, string name, int number);

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="comment">The comment</param>
        /// <returns>The created <see cref="PullRequestReviewComment"/></returns>
        IObservable<PullRequestReviewComment> Create(string owner, string name, int number, PullRequestReviewCommentCreate comment);

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="comment">The comment</param>
        /// <returns>The created <see cref="PullRequestReviewComment"/></returns>
        IObservable<PullRequestReviewComment> CreateReply(string owner, string name, int number, PullRequestReviewCommentReplyCreate comment);

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="comment">The edited comment</param>
        /// <returns>The edited <see cref="PullRequestReviewComment"/></returns>
        IObservable<PullRequestReviewComment> Edit(string owner, string name, int number, PullRequestReviewCommentEdit comment);

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <returns></returns>
        IObservable<Unit> Delete(string owner, string name, int number);
    }
}
