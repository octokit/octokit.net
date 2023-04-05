using System;
using System.Collections.Generic;

namespace Octokit.Reactive
{
  /// <summary>
  /// A client for GitHub's Actions Self-hosted runners API.
  /// </summary>
  /// <remarks>
  /// See the <a href="https://developer.github.com/v3/actions/self-hosted-runners/">Actions Self-hosted runners API documentation</a> for more information.
  /// </remarks>
  public interface IObservableActionsSelfHostedRunnersClient
  {
    /// <summary>
    /// Gets a list of all self-hosted runners for a repository.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    IObservable<RunnerResponse> List(string owner, string name);

    /// <summary>
    /// Gets a list of all self-hosted runners for a repository.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/actions/self-hosted-runners/#list-self-hosted-runners-for-a-repository
    /// </remarks>
    /// <param name="owner">The owner of the repository.</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="options">Options to change the API response.</param>
    IObservable<RunnerResponse> List(string owner, string name, ApiOptions options);
  }
}
