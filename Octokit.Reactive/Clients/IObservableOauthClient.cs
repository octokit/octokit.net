using System;
using System.Threading;

namespace Octokit.Reactive
{
    public interface IObservableOauthClient
    {
        /// <summary>
        /// Gets the URL used in the first step of the web flow. The Web application should redirect to this URL.
        /// </summary>
        /// <param name="request">Parameters to the Oauth web flow login url</param>
        /// <returns></returns>
        Uri GetGitHubLoginUrl(OauthLoginRequest request);

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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IObservable<OauthToken> CreateAccessToken(OauthTokenRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Makes a request to initiate the device flow authentication.
        /// </summary>
        /// <remarks>
        /// Returns a user verification code and verification URL that the you will use to prompt the user to authenticate.
        /// This request also returns a device verification code that you must use to receive an access token to check the status of user authentication.
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IObservable<OauthDeviceFlowResponse> InitiateDeviceFlow(OauthDeviceFlowRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Makes a request to get an access token using the response from <see cref="InitiateDeviceFlow(OauthDeviceFlowRequest, CancellationToken)"/>.
        /// </summary>
        /// <remarks>
        /// Will poll the access token endpoint, until the device and user codes expire or the user has successfully authorized the app with a valid user code.
        /// </remarks>
        /// <param name="clientId">The client Id you received from GitHub when you registered the application.</param>
        /// <param name="deviceFlowResponse">The response you received from <see cref="InitiateDeviceFlow(OauthDeviceFlowRequest, CancellationToken)"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IObservable<OauthToken> CreateAccessTokenForDeviceFlow(string clientId, OauthDeviceFlowResponse deviceFlowResponse, CancellationToken cancellationToken = default);

        /// <summary>
        /// Makes a request to get an access token using the refresh token returned in <see cref="CreateAccessToken(OauthTokenRequest, CancellationToken)"/>.
        /// </summary>
        /// <param name="request">Token renewal request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="OauthToken"/> with the new token set.</returns>
        IObservable<OauthToken> CreateAccessTokenFromRenewalToken(OauthTokenRenewalRequest request, CancellationToken cancellationToken = default);
    }
}
