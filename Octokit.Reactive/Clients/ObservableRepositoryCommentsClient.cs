using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableRepositoryCommentsClient : IObservableRepositoryCommentsClient
    {
        readonly IRepositoryCommentsClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryCommentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.RepositoryComments;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets a single Repository Comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <returns></returns>
        public IObservable<CommitComment> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<CommitComment> GetForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<CommitComment>(ApiUrls.CommitComments(owner, name));
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <returns></returns>
        public IObservable<CommitComment> GetForCommit(string owner, string name, string sha)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            return _connection.GetAndFlattenAllPages<CommitComment>(ApiUrls.CommitComments(owner, name, sha));
        }

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <returns></returns>
        public IObservable<CommitComment> Create(string owner, string name, string sha, NewCommitComment newCommitComment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");
            Ensure.ArgumentNotNull(newCommitComment, "newCommitComment");

            return _client.Create(owner, name, sha, newCommitComment).ToObservable();
        }

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <returns></returns>
        public IObservable<CommitComment> Update(string owner, string name, int number, string commentUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(commentUpdate, "commentUpdate");

            return _client.Update(owner, name, number, commentUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Delete(owner, name, number).ToObservable();
        }
    }
}
