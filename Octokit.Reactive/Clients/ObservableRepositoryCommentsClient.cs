using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/comments/">Repository Comments API documentation</a> for more information.
    /// </remarks>
    public class ObservableRepositoryCommentsClient : IObservableRepositoryCommentsClient
    {
        readonly IRepositoryCommentsClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryCommentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Comment;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets a single Repository Comment by number.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        public IObservable<CommitComment> Get(string owner, string name, long commentId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, commentId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single Repository Comment by number.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        public IObservable<CommitComment> Get(long repositoryId, long commentId, CancellationToken cancellationToken = default)
        {
            return _client.Get(repositoryId, commentId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        public IObservable<CommitComment> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        public IObservable<CommitComment> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        public IObservable<CommitComment> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CommitComment>(ApiUrls.CommitComments(owner, name), null, options, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        public IObservable<CommitComment> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CommitComment>(ApiUrls.CommitComments(repositoryId), null, options, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        public IObservable<CommitComment> GetAllForCommit(string owner, string name, string sha, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));

            return GetAllForCommit(owner, name, sha, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        public IObservable<CommitComment> GetAllForCommit(long repositoryId, string sha, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));

            return GetAllForCommit(repositoryId, sha, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        public IObservable<CommitComment> GetAllForCommit(string owner, string name, string sha, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CommitComment>(ApiUrls.CommitComments(owner, name, sha), null, options);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        public IObservable<CommitComment> GetAllForCommit(long repositoryId, string sha, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CommitComment>(ApiUrls.CommitComments(repositoryId, sha), null, options, cancellationToken);
        }

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        public IObservable<CommitComment> Create(string owner, string name, string sha, NewCommitComment newCommitComment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNull(newCommitComment, nameof(newCommitComment));

            return _client.Create(owner, name, sha, newCommitComment, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        public IObservable<CommitComment> Create(long repositoryId, string sha, NewCommitComment newCommitComment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNull(newCommitComment, nameof(newCommitComment));

            return _client.Create(repositoryId, sha, newCommitComment, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        public IObservable<CommitComment> Update(string owner, string name, long commentId, string commentUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(commentUpdate, nameof(commentUpdate));

            return _client.Update(owner, name, commentId, commentUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        public IObservable<CommitComment> Update(long repositoryId, long commentId, string commentUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(commentUpdate, nameof(commentUpdate));

            return _client.Update(repositoryId, commentId, commentUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        public IObservable<Unit> Delete(string owner, string name, long commentId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, commentId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        public IObservable<Unit> Delete(long repositoryId, long commentId, CancellationToken cancellationToken = default)
        {
            return _client.Delete(repositoryId, commentId, cancellationToken).ToObservable();
        }
    }
}
