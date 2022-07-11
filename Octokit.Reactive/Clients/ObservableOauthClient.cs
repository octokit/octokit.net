﻿using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableOauthClient : IObservableOauthClient
    {
        readonly IGitHubClient _client;

        public ObservableOauthClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client;
        }

        /// <summary>
        /// Gets the URL used in the first step of the web flow. The Web application should redirect to this URL.
        /// </summary>
        /// <param name="request">Parameters to the Oauth web flow login url</param>
        /// <returns></returns>
        public Uri GetGitHubLoginUrl(OauthLoginRequest request)
        {
            return _client.Oauth.GetGitHubLoginUrl(request);
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
        public IObservable<OauthToken> CreateAccessToken(OauthTokenRequest request)
        {
            return _client.Oauth.CreateAccessToken(request).ToObservable();
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
        public IObservable<OauthDeviceFlowResponse> InitiateDeviceFlow(OauthDeviceFlowRequest request)
        {
            return _client.Oauth.InitiateDeviceFlow(request).ToObservable();
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
        public IObservable<OauthToken> CreateAccessTokenForDeviceFlow(string clientId, OauthDeviceFlowResponse deviceFlowResponse)
        {
            return _client.Oauth.CreateAccessTokenForDeviceFlow(clientId, deviceFlowResponse).ToObservable();
        }
    }
}
