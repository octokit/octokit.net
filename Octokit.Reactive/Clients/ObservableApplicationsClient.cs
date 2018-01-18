using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableApplicationsClient : IObservableApplicationsClient
    {
        private IApplicationsClient _client;

        public ObservableApplicationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Application;
        }

        public IObservable<Application> Create()
        {
            return _client.Create().ToObservable();
        }
    }
}
