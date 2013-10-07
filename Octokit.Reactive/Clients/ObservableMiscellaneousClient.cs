using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableMiscellaneousClient : IObservableMiscellaneousClient
    {
        readonly IMiscellaneousClient _client;

        public ObservableMiscellaneousClient(IMiscellaneousClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        public IObservable<IReadOnlyDictionary<string, Uri>> GetEmojis()
        {
            return _client.GetEmojis().ToObservable();
        }
    }
}
