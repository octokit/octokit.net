using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Reactions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/reactions/">Reactions API documentation</a> for more information.
    /// </remarks>
    public class ObservableIssueReactionsClient : IObservableIssueReactionsClient
    {
        readonly IIssueReactionsClient _client;
        readonly IConnection _connection;

        public ObservableIssueReactionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Reaction.Issue;
            _connection = client.Connection;
        }

        /// <summary>
        /// List reactions for a specified Issue
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        public IObservable<Reaction> GetAll(string owner, string name, long issueNumber)
        {
            return GetAll(owner, name, issueNumber, ApiOptions.None);
        }

        /// <summary>
        /// List reactions for a specified Issue
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Reaction> GetAll(string owner, string name, long issueNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Reaction>(ApiUrls.IssueReactions(owner, name, issueNumber), null, options);
        }

        /// <summary>
        /// List reactions for a specified Issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        public IObservable<Reaction> GetAll(long repositoryId, long issueNumber)
        {
            return GetAll(repositoryId, issueNumber, ApiOptions.None);
        }

        /// <summary>
        /// List reactions for a specified Issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Reaction> GetAll(long repositoryId, long issueNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Reaction>(ApiUrls.IssueReactions(repositoryId, issueNumber), null, options);
        }

        /// <summary>
        /// Creates a reaction for a specified Issue
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="reaction">The reaction to create</param>
        public IObservable<Reaction> Create(string owner, string name, long issueNumber, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return _client.Create(owner, name, issueNumber, reaction).ToObservable();
        }

        /// <summary>
        /// Creates a reaction for a specified Issue.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="reaction">The reaction to create </param>
        public IObservable<Reaction> Create(long repositoryId, long issueNumber, NewReaction reaction)
        {
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return _client.Create(repositoryId, issueNumber, reaction).ToObservable();
        }

        /// <summary>
        /// Deletes a reaction for a specified Issue
        /// </summary>
        /// <remarks>https://docs.github.com/rest/reactions#delete-an-issue-reaction</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="reactionId">The reaction id</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string name, long issueNumber, long reactionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reactionId, nameof(reactionId));

            return _client.Delete(owner, name, issueNumber, reactionId).ToObservable();
        }

        /// <summary>
        /// Deletes a reaction for a specified Issue
        /// </summary>
        /// <remarks>https://docs.github.com/rest/reactions#delete-an-issue-reaction</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="reactionId">The reaction id</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(long repositoryId, long issueNumber, long reactionId)
        {
            Ensure.ArgumentNotNull(reactionId, nameof(reactionId));

            return _client.Delete(repositoryId, issueNumber, reactionId).ToObservable();
        }
    }
}
