using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservablePullRequestReviewCommentsClient : IObservablePullRequestReviewCommentsClient
    {
        readonly IPullRequestReviewCommentsClient _client;
        readonly IConnection _connection;

        public ObservablePullRequestReviewCommentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.PullRequest.Comment;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>The list of <see cref="PullRequestReviewComment"/>s for the specified pull request</returns>
        public IObservable<PullRequestReviewComment> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(owner, name, number));
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The list of <see cref="PullRequestReviewComment"/>s for the specified repository</returns>
        public IObservable<PullRequestReviewComment> GetAllForRepository(string owner, string name)
        {
            return GetAllForRepository(owner, name, new PullRequestReviewCommentRequest());
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <returns>The list of <see cref="PullRequestReviewComment"/>s for the specified repository</returns>
        public IObservable<PullRequestReviewComment> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewCommentsRepository(owner, name), request.ToParametersDictionary());
        }

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <returns>The <see cref="PullRequestReviewComment"/></returns>
        public IObservable<PullRequestReviewComment> GetComment(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.GetComment(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="comment">The comment</param>
        /// <returns>The created <see cref="PullRequestReviewComment"/></returns>
        public IObservable<PullRequestReviewComment> Create(string owner, string name, int number, PullRequestReviewCommentCreate comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(comment, "comment");

            return _client.Create(owner, name, number, comment).ToObservable();
        }

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="comment">The comment</param>
        /// <returns>The created <see cref="PullRequestReviewComment"/></returns>
        public IObservable<PullRequestReviewComment> CreateReply(string owner, string name, int number, PullRequestReviewCommentReplyCreate comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(comment, "comment");

            return _client.CreateReply(owner, name, number, comment).ToObservable();
        }

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="comment">The edited comment</param>
        /// <returns>The edited <see cref="PullRequestReviewComment"/></returns>
        public IObservable<PullRequestReviewComment> Edit(string owner, string name, int number, PullRequestReviewCommentEdit comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(comment, "comment");

            return _client.Edit(owner, name, number, comment).ToObservable();
        }

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Delete(owner, name, number).ToObservable();
        }
    }
}
