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
    /// Refer to the API documentation for more information: https://docs.github.com/rest/reactions/
    /// </remarks>
    IObservableCommitCommentReactionsClient CommitComment { get; }

    /// <summary>
    /// Access GitHub's Reactions API for Issues.
    /// </summary>
    /// <remarks>
    /// Refer to the API documentation for more information: https://docs.github.com/rest/reactions/
    /// </remarks>
    IObservableIssueReactionsClient Issue { get; }

    /// <summary>
    /// Access GitHub's Reactions API for Issue Comments.
    /// </summary>
    /// <remarks>
    /// Refer to the API documentation for more information: https://docs.github.com/rest/reactions/
    /// </remarks>
    IObservableIssueCommentReactionsClient IssueComment { get; }

    /// <summary>
    /// Access GitHub's Reactions API for Pull Request Review Comments.
    /// </summary>
    /// <remarks>
    /// Refer to the API documentation for more information: https://docs.github.com/rest/reactions/
    /// </remarks>
    IObservablePullRequestReviewCommentReactionsClient PullRequestReviewComment { get; }
  }
}
