using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableGistsClient : IObservableGistsClient 
    {
        readonly IGistsClient _client;

        public ObservableGistsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Gist;
            Comment = new ObservableGistCommentsClient(client);
        }

        public IObservableGistCommentsClient Comment { get; set; }

        public IObservable<Gist> Get(string id)
        {
            Ensure.ArgumentNotNullOrEmptyString(id, "id");

            return _client.Get(id).ToObservable();
        }
    }
}