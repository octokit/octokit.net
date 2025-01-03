using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Provides methods used in the OAuth web flow.
    /// </summary>
    /// <inheritdoc />
    public class OauthClient : IOauthClient
    {
        readonly IConnection connection;
        readonly Uri hostAddress;

        /// <summary>
        /// Create an instance of the OauthClient
        /// </summary>
        /// <param name="connection">The underlying connection to use</param>
        public OauthClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, nameof(connection));

            this.connection = connection;
            var baseAddress = connection.BaseAddress ?? GitHubClient.GitHubDotComUrl;

            // The Oauth login stuff uses the main website and not the API URLs
            // For https://api.github.com we use https://github.com 
            // For any other address (presumably a GitHub Enterprise address) we need to strip any relative Uri such as /api/v3
            hostAddress = baseAddress.Host.Equals("api.github.com")
                ? new Uri("https://github.com")
                : baseAddress.StripRelativeUri();
        }

        /// <summary>
        /// Gets the URL used in the first step of the web flow. The Web application should redirect to this URL.
        /// </summary>
        /// <param name="request">Parameters to the Oauth web flow login url</param>
        /// <returns></returns>
        [DotNetSpecificRoute]
        public Uri GetGitHubLoginUrl(OauthLoginRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return new Uri(hostAddress, ApiUrls.OauthAuthorize())
                .ApplyParameters(request.ToParametersDictionary());
        }

        [ManualRoute("POST", "/login/oauth/access_token")]
        public async Task<OauthToken> CreateAccessToken(OauthTokenRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var endPoint = ApiUrls.OauthAccessToken();

            var body = new FormUrlEncodedContent(request.ToParametersDictionary());

            var response = await connection.Post<OauthToken>(endPoint, body, "application/json", null, hostAddress, cancellationToken).ConfigureAwait(false);
            return response.Body;
        }

        [ManualRoute("POST", "/login/device/code")]
        public async Task<OauthDeviceFlowResponse> InitiateDeviceFlow(OauthDeviceFlowRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var endPoint = ApiUrls.OauthDeviceCode();

            var body = new FormUrlEncodedContent(request.ToParametersDictionary());

            var response = await connection.Post<OauthDeviceFlowResponse>(endPoint, body, "application/json", null, hostAddress, cancellationToken).ConfigureAwait(false);
            return response.Body;
        }

        [ManualRoute("POST", "/login/oauth/access_token")]
        public async Task<OauthToken> CreateAccessTokenForDeviceFlow(string clientId, OauthDeviceFlowResponse deviceFlowResponse, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNull(deviceFlowResponse, nameof(deviceFlowResponse));

            var endPoint = ApiUrls.OauthAccessToken();

            int pollingDelay = deviceFlowResponse.Interval;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var request = new OauthTokenRequestForDeviceFlow(clientId, deviceFlowResponse.DeviceCode);
                var body = new FormUrlEncodedContent(request.ToParametersDictionary());
                var response = await connection.Post<OauthToken>(endPoint, body, "application/json", null, hostAddress, cancellationToken).ConfigureAwait(false);

                if (response.Body.Error != null)
                {
                    switch (response.Body.Error)
                    {
                        case "authorization_pending":
                            break;
                        case "slow_down":
                            pollingDelay += 5;
                            break;
                        case "expired_token":
                        default:
                            throw new ApiException(string.Format(CultureInfo.InvariantCulture, "{0}: {1}\n{2}", response.Body.Error, response.Body.ErrorDescription, response.Body.ErrorUri), null);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(pollingDelay), cancellationToken);
                }
                else
                {
                    return response.Body;
                }
            }
        }

        [ManualRoute("POST", "/login/oauth/access_token")]
        public async Task<OauthToken> CreateAccessTokenFromRenewalToken(OauthTokenRenewalRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var endPoint = ApiUrls.OauthAccessToken();
            var body = new FormUrlEncodedContent(request.ToParametersDictionary());

            var response = await connection.Post<OauthToken>(endPoint, body, "application/json", null, hostAddress, cancellationToken).ConfigureAwait(false);
            return response.Body;
        }
    }
}
