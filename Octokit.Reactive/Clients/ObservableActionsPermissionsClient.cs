namespace Octokit.Reactive
{
    public class ObservableActionsPermissionsClient : IObservableActionsPermissionsClient
    {
        readonly IActionsPermissionsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Permissions API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsPermissionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Permissions;
        }
    }
}
