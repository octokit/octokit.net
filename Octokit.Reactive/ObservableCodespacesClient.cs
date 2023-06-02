namespace Octokit.Reactive
{
    public class ObservableCodespacesClient : IObservableCodespacesClient
    {
        private IGitHubClient githubClient;

        public ObservableCodespacesClient(IGitHubClient githubClient)
        {
            this.githubClient = githubClient;
        }
    }
}