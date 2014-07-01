using System;
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

        public OauthClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            this.connection = connection;
            var baseAddress = connection.BaseAddress ?? GitHubClient.GitHubDotComUrl;

            // The Oauth login stuff uses https://github.com and not the https://api.github.com URLs.
            hostAddress = baseAddress.Host.Equals("api.github.com")
                ? new Uri("https://github.com")
                : baseAddress;
        }

        /// <summary>
        /// Gets the URL used in the first step of the web flow. The Web application should redirect to this URL.
        /// </summary>
        /// <param name="request">Parameters to the Oauth web flow login url</param>
        /// <returns></returns>
        public Uri GetGitHubLoginUrl(OauthLoginRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

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
        public async Task<OauthToken> CreateAccessToken(OauthTokenRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var endPoint = ApiUrls.OauthAccessToken();

            var body = new FormUrlEncodedContent(request.ToParametersDictionary());

            var response = await connection.Post<OauthToken>(endPoint, body, "application/json", null, hostAddress);
            return response.BodyAsObject;
        }
    }
}
