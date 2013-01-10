using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Nocto.Helpers;

namespace Nocto.Http
{
    public class ApiInfoParser : Middleware
    {
        static readonly RegexOptions regexOptions =
#if NETFX_CORE
            RegexOptions.IgnoreCase;
#else
            RegexOptions.Compiled | RegexOptions.IgnoreCase;
#endif

        readonly Regex linkRelRegex = new Regex("rel=\"(next|prev|first|last)\"", regexOptions);
        readonly Regex linkUriRegex = new Regex("<(.+)>", regexOptions);

        public ApiInfoParser(IApplication app)
            : base(app)
        {
        }

        protected override void Before<T>(Environment<T> environment)
        {
            Ensure.ArgumentNotNull(environment, "env");
            if (environment.Response is GitHubResponse<T>) return;

            environment.Response = new GitHubResponse<T>();
        }

        protected override void After<T>(Environment<T> environment)
        {
            Ensure.ArgumentNotNull(environment, "env");

            if (environment.Response is GitHubResponse<T>)
            {
                ((GitHubResponse<T>)environment.Response).ApiInfo = ParseHeaders(environment);
            }
        }

        ApiInfo ParseHeaders<T>(Environment<T> environment)
        {
            var httpLinks = new Dictionary<string, Uri>();
            var oauthScopes = new List<string>();
            var acceptedOauthScopes = new List<string>();
            int rateLimit = 0;
            int rateLimitRemaining = 0;
            string etag = null;

            if (environment.Response.Headers.ContainsKey("X-Accepted-OAuth-Scopes"))
            {
                acceptedOauthScopes.AddRange(environment.Response.Headers["X-Accepted-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (environment.Response.Headers.ContainsKey("X-OAuth-Scopes"))
            {
                oauthScopes.AddRange(environment.Response.Headers["X-OAuth-Scopes"]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()));
            }

            if (environment.Response.Headers.ContainsKey("X-RateLimit-Limit"))
            {
                rateLimit = Convert.ToInt32(environment.Response.Headers["X-RateLimit-Limit"], CultureInfo.InvariantCulture);
            }

            if (environment.Response.Headers.ContainsKey("X-RateLimit-Remaining"))
            {
                rateLimitRemaining = Convert.ToInt32(environment.Response.Headers["X-RateLimit-Remaining"], CultureInfo.InvariantCulture);
            }

            if (environment.Response.Headers.ContainsKey("ETag"))
            {
                etag = environment.Response.Headers["ETag"];
            }

            if (environment.Response.Headers.ContainsKey("Link"))
            {
                var links = environment.Response.Headers["Link"].Split(',');
                foreach (var link in links)
                {
                    var relMatch = linkRelRegex.Match(link);
                    if (!relMatch.Success || relMatch.Groups.Count != 2) break;

                    var uriMatch = linkUriRegex.Match(link);
                    if (!uriMatch.Success || uriMatch.Groups.Count != 2) break;

                    httpLinks.Add(relMatch.Groups[1].Value, new Uri(uriMatch.Groups[1].Value));
                }
            }

            return new ApiInfo(httpLinks, oauthScopes, acceptedOauthScopes, etag, rateLimit, rateLimitRemaining);
        }
    }
}
