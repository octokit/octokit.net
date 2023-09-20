using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// Wrapper around <see cref="IOauthClient"/> for use with <see cref="IObservable{T}"/>
    /// </summary>
    /// <inheritdoc />
    public class ObservableOauthClient : IObservableOauthClient
    {
        readonly IGitHubClient _client;

        public ObservableOauthClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client;
        }

        public Uri GetGitHubLoginUrl(OauthLoginRequest request)
        {
            return _client.Oauth.GetGitHubLoginUrl(request);
        }

        public IObservable<OauthToken> CreateAccessToken(OauthTokenRequest request)
        {
            return _client.Oauth.CreateAccessToken(request).ToObservable();
        }

        public IObservable<OauthDeviceFlowResponse> InitiateDeviceFlow(OauthDeviceFlowRequest request)
        {
            return _client.Oauth.InitiateDeviceFlow(request).ToObservable();
        }

        public IObservable<OauthToken> CreateAccessTokenForDeviceFlow(string clientId, OauthDeviceFlowResponse deviceFlowResponse)
        {
            return _client.Oauth.CreateAccessTokenForDeviceFlow(clientId, deviceFlowResponse).ToObservable();
        }

        public IObservable<OauthToken> CreateAccessTokenFromRenewalToken(OauthTokenRenewalRequest request)
        {
            return _client.Oauth.CreateAccessTokenFromRenewalToken(request)
                .ToObservable();
        }
    }
}
