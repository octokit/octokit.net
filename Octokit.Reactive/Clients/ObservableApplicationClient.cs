using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableApplicationClient : IObservableApplicationClient
    {
        private IApplicationClient _client;

        public ObservableApplicationClient(IGitHubClient client)
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
