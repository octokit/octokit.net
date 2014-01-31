using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableMiscellaneousClient : IObservableMiscellaneousClient
    {
        readonly IMiscellaneousClient _client;

        public ObservableMiscellaneousClient(IMiscellaneousClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        public IObservable<Emoji> GetEmojis()
        {
            return _client.GetEmojis().ToObservable().SelectMany(e => e);
        }

        public IObservable<string> RenderRawMarkdown(string markdown)
        {
            return _client.RenderRawMarkdown(markdown).ToObservable();
        }
    }
}
