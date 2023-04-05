using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
  public class ObservableActionsSelfHostedRunnersClient : IObservableActionsSelfHostedRunnersClient
  {
    readonly IActionsSelfHostedRunnersClient _client;

    /// <summary>
    /// Instantiate a new GitHub Actions Self-hosted runners API client.
    /// </summary>
    /// <param name="client">A GitHub client.</param>
    public ObservableActionsSelfHostedRunnersClient(IGitHubClient client)
    {
      Ensure.ArgumentNotNull(client, nameof(client));

      _client = client.Actions.SelfHostedRunners;
    }

    public IObservable<RunnerResponse> List(string owner, string name)
    {
      return List(owner, name, ApiOptions.None);
    }

    public IObservable<RunnerResponse> List(string owner, string name, ApiOptions options)
    {
      Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
      Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
      Ensure.ArgumentNotNull(options, nameof(options));

      return _client.List(owner, name, options).ToObservable();
    }
  }
}
