﻿using System;
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
    public class ObservablePullRequestReviewCommentReactionsClient : IObservablePullRequestReviewCommentReactionsClient
    {
        readonly IPullRequestReviewCommentReactionsClient _client;
        readonly IConnection _connection;

        public ObservablePullRequestReviewCommentReactionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Reaction.PullRequestReviewComment;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get all reactions for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-pull-request-review-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        public IObservable<Reaction> GetAll(string owner, string name, int number)
        {
            return GetAll(owner, name, number, ApiOptions.None);
        }

        public IObservable<Reaction> GetAll(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Reaction>(ApiUrls.PullRequestReviewCommentReactions(owner, name, number), null, options);
        }

        /// <summary>
        /// Get all reactions for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-pull-request-review-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        public IObservable<Reaction> GetAll(long repositoryId, int number)
        {
            return GetAll(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Get all reactions for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-pull-request-review-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Reaction> GetAll(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Reaction>(ApiUrls.PullRequestReviewCommentReactions(repositoryId, number), null, options);
        }

        /// <summary>
        /// Creates a reaction for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-pull-request-review-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        public IObservable<Reaction> Create(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return _client.Create(owner, name, number, reaction).ToObservable();
        }

        /// <summary>
        /// Creates a reaction for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-pull-request-review-comment</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        public IObservable<Reaction> Create(long repositoryId, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return _client.Create(repositoryId, number, reaction).ToObservable();
        }

        /// <summary>
        /// Deletes a reaction for a specified Pull Request comment
        /// </summary>
        /// <remarks>https://docs.github.com/rest/reactions#delete-a-pull-request-comment-reaction</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reactionId">The reaction id</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string name, int commentId, int reactionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reactionId, nameof(reactionId));

            return _client.Delete(owner, name, commentId, reactionId).ToObservable();
        }

        /// <summary>
        /// Deletes a reaction for a specified Pull Request comment
        /// </summary>
        /// <remarks>https://docs.github.com/rest/reactions#delete-a-pull-request-comment-reaction</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reactionId">The reaction id</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(long repositoryId, int commentId, int reactionId)
        {
            Ensure.ArgumentNotNull(reactionId, nameof(reactionId));

            return _client.Delete(repositoryId, commentId, reactionId).ToObservable();
        }
    }
}
