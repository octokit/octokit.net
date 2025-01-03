using System;
using System.Reactive.Threading.Tasks;
using System.Threading;

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

        public IObservable<OauthToken> CreateAccessToken(OauthTokenRequest request, CancellationToken cancellationToken = default)
        {
            return _client.Oauth.CreateAccessToken(request, cancellationToken).ToObservable();
        }

        public IObservable<OauthDeviceFlowResponse> InitiateDeviceFlow(OauthDeviceFlowRequest request, CancellationToken cancellationToken = default)
        {
            return _client.Oauth.InitiateDeviceFlow(request, cancellationToken).ToObservable();
        }

        public IObservable<OauthToken> CreateAccessTokenForDeviceFlow(string clientId, OauthDeviceFlowResponse deviceFlowResponse, CancellationToken cancellationToken = default)
        {
            return _client.Oauth.CreateAccessTokenForDeviceFlow(clientId, deviceFlowResponse, cancellationToken).ToObservable();
        }

        public IObservable<OauthToken> CreateAccessTokenFromRenewalToken(OauthTokenRenewalRequest request, CancellationToken cancellationToken = default)
        {
            return _client.Oauth.CreateAccessTokenFromRenewalToken(request, cancellationToken)
                .ToObservable();
        }
    }
}
