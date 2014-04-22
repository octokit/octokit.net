using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableOauthClient : IObservableOauthClient
    {
        readonly IGitHubClient _client;

        public ObservableOauthClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        public IObservable<Uri> GetGitHubLoginUrl(OauthLoginRequest request)
        {
            return Observable.Return(_client.Oauth.GetGitHubLoginUrl(request));
        }

        public IObservable<OauthToken> CreateAccessToken(OauthTokenRequest request)
        {
            return _client.Oauth.CreateAccessToken(request).ToObservable();
        }
    }
}
