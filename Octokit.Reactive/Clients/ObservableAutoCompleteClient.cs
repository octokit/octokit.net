using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableAutoCompleteClient : IObservableAutoCompleteClient
    {
        readonly IAutoCompleteClient _client;

        public ObservableAutoCompleteClient(IAutoCompleteClient client)
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
