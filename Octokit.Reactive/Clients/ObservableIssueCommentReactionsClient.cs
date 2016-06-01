using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableIssueCommentReactionsClient : IObservableIssueCommentReactionsClient
    {
        readonly ICommitCommentReactionsClient _client;
        readonly IConnection _connection;

        public ObservableIssueCommentReactionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Reaction.CommitComment;
            _connection = client.Connection;
        }

        /// <summary>
        /// Creates a reaction for an specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create </param>
        /// <returns></returns>
        public IObservable<Reaction> Create(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reaction, "reaction");

            return _client.Create(owner, name, number, reaction).ToObservable();
        }

        /// <summary>
        /// List reactions for an specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns></returns>
        public IObservable<Reaction> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<Reaction>(ApiUrls.IssueCommentReactions(owner, name, number), null, AcceptHeaders.ReactionsPreview);
        }
    }
}
