﻿using System;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableReactionsClient
    {
        /// <summary>
        /// Access GitHub's Reactions API for Commit Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        IObservableCommitCommentReactionsClient CommitComment { get; }

        /// <summary>
        /// Access GitHub's Reactions API for Issues.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        IObservableIssueReactionsClient Issue { get; }

        /// <summary>
        /// Access GitHub's Reactions API for Issue Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        IObservableIssueCommentReactionsClient IssueComment { get; }

        /// <summary>
        /// Access GitHub's Reactions API for Pull Request Review Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        IObservablePullRequestReviewCommentReactionsClient PullRequestReviewComment { get; }

        /// <summary>
        /// Delete a reaction.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#delete-a-reaction</remarks>        
        /// <param name="number">The reaction id</param>        
        /// <returns></returns>
        IObservable<Unit> Delete(int number);
    }
}
