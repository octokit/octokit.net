using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableCommitCommentReactionClient : IObservableCommitCommentReactionClient
    {
        readonly ICommitCommentReactionClient _client;
        readonly IConnection _connection;

        public ObservableCommitCommentReactionClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Reaction.CommitComments;
            _connection = client.Connection;
        }

        /// <summary>
        /// Creates a reaction for a specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction for </param>
        /// <returns></returns>
        public IObservable<Reaction> CreateReaction(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reaction, "reaction");

            return _client.CreateReaction(owner, name, number, reaction).ToObservable();
        }

        /// <summary>
        /// List reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns></returns>
        public IObservable<Reaction> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<Reaction>(ApiUrls.CommitCommentReaction(owner, name, number));
        }
    }
}
