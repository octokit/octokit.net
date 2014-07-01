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
    }
}
