using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Provides methods used in the OAuth web flow.
    /// </summary>
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

        /// <summary>
        /// Makes a request to get an access token using the code returned when GitHub.com redirects back from the URL
        /// <see cref="GetGitHubLoginUrl">GitHub login url</see> to the application.
        /// </summary>
        /// <remarks>
        /// If the user accepts your request, GitHub redirects back to your site with a temporary code in a code
        /// parameter as well as the state you provided in the previous step in a state parameter. If the states don’t
        /// match, the request has been created by a third party and the process should be aborted. Exchange this for
        /// an access token using this method.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [ManualRoute("POST", "/login/oauth/access_token")]
        public async Task<OauthToken> CreateAccessToken(OauthTokenRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var endPoint = ApiUrls.OauthAccessToken();

            var body = new FormUrlEncodedContent(request.ToParametersDictionary());

            var response = await connection.Post<OauthToken>(endPoint, body, "application/json", null, hostAddress).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Makes a request to initiate the device flow authentication.
        /// </summary>
        /// <remarks>
        /// Returns a user verification code and verification URL that the you will use to prompt the user to authenticate.
        /// This request also returns a device verification code that you must use to receive an access token to check the status of user authentication.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [ManualRoute("POST", "/login/device/code")]
        public async Task<OauthDeviceFlowResponse> InitiateDeviceFlow(OauthDeviceFlowRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var endPoint = ApiUrls.OauthDeviceCode();

            var body = new FormUrlEncodedContent(request.ToParametersDictionary());

            var response = await connection.Post<OauthDeviceFlowResponse>(endPoint, body, "application/json", null, hostAddress).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Makes a request to get an access token using the response from <see cref="InitiateDeviceFlow(OauthDeviceFlowRequest)"/>.
        /// </summary>
        /// <remarks>
        /// Will poll the access token endpoint, until the device and user codes expire or the user has successfully authorized the app with a valid user code.
        /// </remarks>
        /// <param name="clientId">The client Id you received from GitHub when you registered the application.</param>
        /// <param name="deviceFlowResponse">The response you received from <see cref="InitiateDeviceFlow(OauthDeviceFlowRequest)"/></param>
        /// <returns></returns>
        [ManualRoute("POST", "/login/oauth/access_token")]
        public async Task<OauthToken> CreateAccessTokenForDeviceFlow(string clientId, OauthDeviceFlowResponse deviceFlowResponse)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNull(deviceFlowResponse, nameof(deviceFlowResponse));

            var endPoint = ApiUrls.OauthAccessToken();

            int pollingDelay = deviceFlowResponse.Interval;

            while (true)
            {
                var request = new OauthTokenRequestForDeviceFlow(clientId, deviceFlowResponse.DeviceCode);
                var body = new FormUrlEncodedContent(request.ToParametersDictionary());
                var response = await connection.Post<OauthToken>(endPoint, body, "application/json", null, hostAddress).ConfigureAwait(false);

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

                    await Task.Delay(TimeSpan.FromSeconds(pollingDelay));
                }
                else
                {
                    return response.Body;
                }
            }
        }
    }
}
