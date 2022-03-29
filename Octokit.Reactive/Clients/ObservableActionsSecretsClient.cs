namespace Octokit.Reactive
{
    public class ObservableActionsSecretsClient : IObservableActionsSecretsClient
    {
        readonly IActionsSecretsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Secrets API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsSecretsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Secrets;
        }
    }
}
